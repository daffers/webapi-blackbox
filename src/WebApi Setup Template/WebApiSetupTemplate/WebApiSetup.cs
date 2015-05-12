using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace WebApiSetupTemplate
{
    public abstract class WebApiSetup
    {
        protected readonly IDependencyResolver Resolver;

        protected WebApiSetup(IDependencyResolver resolver)
        {
            Resolver = resolver;
        }

        public void ConfigureSite(HttpConfiguration configuration)
        {
            configuration.DependencyResolver = Resolver;
            ConfigureFilters(configuration.Filters);
            ConfigureFormatters(configuration.Formatters);
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy;
            configuration.Initializer = Initializer;
            ConfigureMessageHandlers(configuration.MessageHandlers);
            ConfigureParameterBindingRules(configuration.ParameterBindingRules);
            ConfigureProperties(configuration.Properties);
            ConfigureRoutes(configuration.Routes);
            ConfigureServices(configuration.Services);
            ExecuteCustomConfigurationSteps(configuration);
        }

        protected virtual void ExecuteCustomConfigurationSteps(HttpConfiguration configuration) { }
        protected virtual void ConfigureServices(ServicesContainer services) { }
        protected virtual void ConfigureRoutes(HttpRouteCollection routes) { }
        protected virtual void ConfigureProperties(ConcurrentDictionary<object, object> properties) { }
        protected virtual void ConfigureParameterBindingRules(ParameterBindingRulesCollection parameterBindingRules) { }
        protected virtual void ConfigureMessageHandlers(Collection<DelegatingHandler> messageHandlers) { }
        protected virtual void Initializer(HttpConfiguration httpConfiguration) { }
        public abstract IncludeErrorDetailPolicy IncludeErrorDetailPolicy { get; }
        protected virtual void ConfigureFormatters(MediaTypeFormatterCollection mediaTypeFormatterCollection) { }
        protected virtual void ConfigureFilters(HttpFilterCollection filters) { }
    }
}
