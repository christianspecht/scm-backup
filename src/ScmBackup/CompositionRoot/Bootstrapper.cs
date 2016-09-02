using ScmBackup.Hosters;
using ScmBackup.Http;
using ScmBackup.Resources;
using SimpleInjector;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    public class Bootstrapper
    {
        public static void SetupResources()
        {
            // TODO: determine current culture
            var culture = new CultureInfo("en-US");

            Resource.Initialize(new ResourceProvider(), culture);
        }

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

            container.RegisterCollection<ILogger>(new ConsoleLogger(), new NLogLogger());
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);

            container.RegisterSingleton<IConfigReader, ConfigReader>();
            container.RegisterDecorator<IConfigReader, ValidatingConfigReader>();

            container.Register<IHttpRequest, HttpRequest>();

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

            container.Verify();

            return container;
        }
    }
}
