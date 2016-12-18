using ScmBackup.Hosters;
using ScmBackup.Http;
using SimpleInjector;
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
            container.RegisterDecorator<IScmBackup, ErrorHandlingScmBackup>();
            container.RegisterDecorator<IScmBackup, LoggingScmBackup>();

            container.RegisterCollection<ILogger>(new ConsoleLogger(), new NLogLogger());
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);

            container.RegisterSingleton<IContext, Context>();

            container.Register<IConfigReader, ConfigReader>(Lifestyle.Singleton);
            container.RegisterDecorator<IConfigReader, ValidatingConfigReader>(Lifestyle.Singleton);

            container.Register<IHttpRequest, HttpRequest>();
            container.RegisterDecorator<IHttpRequest, LoggingHttpRequest>();

            container.Register<IApiCaller, ApiCaller>();

            // auto-register validators
            var validators = container.GetTypesToRegister(typeof(IConfigSourceValidator), thisAssembly);
            foreach (var validator in validators)
            {
                var validatorInterface = validator.GetInterfaces().Except(new[] { (typeof(IConfigSourceValidator)) }).First();
                container.Register(validatorInterface, validator);
            }

            // auto-register hoster APIs
            var apis = container.GetTypesToRegister(typeof(IHosterApi), thisAssembly);
            foreach (var api in apis)
            {
                var apiInterface = api.GetInterfaces().Except(new[] { typeof(IHosterApi) }).First();
                container.Register(apiInterface, api);
            }

            // auto-register hosters
            var hosterFactory = new HosterFactory(container);
            var hosters = container.GetTypesToRegister(typeof(IHoster), thisAssembly);
            foreach (var hoster in hosters)
            {
                hosterFactory.Register(hoster);
            }

            container.RegisterSingleton<IHosterValidator>(new HosterValidator(hosterFactory));
            container.RegisterSingleton<IHosterApiCaller>(new HosterApiCaller(hosterFactory));
            container.Verify();

            return container;
        }
    }
}
