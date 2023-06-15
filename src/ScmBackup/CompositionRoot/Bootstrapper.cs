using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Http;
using ScmBackup.Loggers;
using ScmBackup.Scm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    public class Bootstrapper
    {
        /// <summary>
        /// Registers IoC dependencies and returns the initialized container
        /// </summary>
        /// <returns></returns>
        public static Container BuildContainer()
        {
            var thisAssembly = new[] { typeof(ScmBackup).GetTypeInfo().Assembly };

            var container = new Container();
            container.Register<IScmBackup, ScmBackup>();
            container.RegisterDecorator<IScmBackup, LoggingScmBackup>();
            container.RegisterDecorator<IScmBackup, ErrorHandlingScmBackup>();

            // auto-register loggers
            // https://github.com/simpleinjector/SimpleInjector/issues/208#issuecomment-201423660
            var subLoggers = container.GetTypesToRegister(typeof(ILogger), thisAssembly)
                .Except(new List<Type> { typeof(CompositeLogger) });
            var subLoggerRegistrations = (
                from type in subLoggers
                select Lifestyle.Singleton.CreateRegistration(type, container)
                ).ToArray();

            container.Collection.Register<ILogger>(subLoggerRegistrations);
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);

            container.Register<IFileSystemHelper, FileSystemHelper>();

            container.RegisterSingleton<IContext, Context>();
            container.Register<IConfigBackupMaker, ConfigBackupMaker>();

            container.Register<IConfigReader, ConfigReader>(Lifestyle.Singleton);
            container.RegisterDecorator<IConfigReader, EnvironmentVariableConfigReader>(Lifestyle.Singleton); 
            container.RegisterDecorator<IConfigReader, ValidatingConfigReader>(Lifestyle.Singleton);

            container.Register<IHttpRequest, HttpRequest>();
            container.RegisterDecorator<IHttpRequest, LoggingHttpRequest>();
            container.RegisterSingleton<IEmailSender, MailKitEmailSender>();
            container.Register<IUrlHelper, UrlHelper>();

            container.Register<IApiCaller, ApiCaller>();
            container.Register<IScmValidator, ScmValidator>();
            container.Register<IBackupMaker, BackupMaker>();
            container.Register<IDeletedRepoHandler, DeletedRepoHandler>();

            // auto-register validators
            var validators = container.GetTypesToRegister(typeof(IConfigSourceValidator), thisAssembly);
            foreach (var validator in validators)
            {
                string hosterName = HosterNameHelper.GetHosterName(validator, "configsourcevalidator");

                container.RegisterConditional(typeof(IConfigSourceValidator), validator, 
                    c => HosterNameHelper.GetHosterName(c.Consumer.ImplementationType, "hoster") == hosterName && !c.Handled);
            }

            // auto-register hoster APIs
            var apis = container.GetTypesToRegister(typeof(IHosterApi), thisAssembly);
            foreach (var api in apis)
            {
                string hosterName = HosterNameHelper.GetHosterName(api, "api");

                container.RegisterConditional(typeof(IHosterApi), api,
                    c => HosterNameHelper.GetHosterName(c.Consumer.ImplementationType, "hoster") == hosterName && !c.Handled);
            }

            // auto-register hoster backuppers
            var backups = container.GetTypesToRegister(typeof(IBackup), thisAssembly);
            foreach (var backup in backups)
            {
                string hosterName = HosterNameHelper.GetHosterName(backup, "backup");

                container.RegisterConditional(typeof(IBackup), backup,
                c => HosterNameHelper.GetHosterName(c.Consumer.ImplementationType, "hoster") == hosterName && !c.Handled);
            }

            // auto-register hosters
            var hosterFactory = new HosterFactory(container);
            var hosters = container.GetTypesToRegister(typeof(IHoster), thisAssembly);
            foreach (var hoster in hosters)
            {
                hosterFactory.Register(hoster);
            }

            // auto-register SCMs
            var scmFactory = new ScmFactory(container);
            var scms = container.GetTypesToRegister(typeof(IScm), thisAssembly);
            foreach (var scm in scms)
            {
                scmFactory.Register(scm);
            }

            container.RegisterInstance<IHosterValidator>(new HosterValidator(hosterFactory));

            container.RegisterInstance<IHosterApiCaller>(new HosterApiCaller(hosterFactory));
            container.RegisterDecorator<IHosterApiCaller, IgnoringHosterApiCaller>();
            container.RegisterDecorator<IHosterApiCaller, IncludingHosterApiCaller>();
            container.RegisterDecorator<IHosterApiCaller, LoggingHosterApiCaller>();

            container.RegisterInstance<IHosterBackupMaker>(new HosterBackupMaker(hosterFactory));
            container.RegisterInstance<IHosterFactory>(hosterFactory); // only needed for integration tests!
            container.RegisterInstance<IScmFactory>(scmFactory);
            container.Verify();

            return container;
        }
    }
}
