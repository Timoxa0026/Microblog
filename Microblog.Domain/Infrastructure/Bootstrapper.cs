namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentValidation;
    using FluentValidation.Mvc;
    using Incoding.Block.IoC;
    using Incoding.Block.Logging;
    using Incoding.CQRS;
    using Incoding.Data;
    using Incoding.Extensions;
    using Incoding.MvcContrib;
    using NHibernate.Tool.hbm2ddl;
    using StructureMap.Graph;

    #endregion

    public static class Bootstrapper
    {
        public static void Start()
        {
            IoCFactory.Instance.Initialize(init => init.WithProvider(new StructureMapIoCProvider(registry =>
                                                                                                 {
                                                                                                     registry.For<IDispatcher>().Use<DefaultDispatcher>();
                                                                                                     registry.For<ITemplateFactory>().Singleton().Use<TemplateHandlebarsFactory>();

                                                                                                     var configure = Fluently
                                                                                                             .Configure()
                                                                                                             .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
                                                                                                             .Mappings(configuration => configuration.FluentMappings.AddFromAssembly(typeof(Bootstrapper).Assembly))
                                                                                                             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
                                                                                                     ;
                                                                                                     registry.For<INhibernateSessionFactory>().Singleton().Use(() => new NhibernateSessionFactory(configure));
                                                                                                     registry.For<IUnitOfWorkFactory>().Use<NhibernateUnitOfWorkFactory>();

                                                                                                     registry.Scan(r =>
                                                                                                                   {
                                                                                                                       r.TheCallingAssembly();
                                                                                                                       r.WithDefaultConventions();

                                                                                                                       r.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                                                                                                                       r.AddAllTypesOf<ISetUp>();
                                                                                                                   });
                                                                                                 })));

            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new IncValidatorFactory()));
            FluentValidationModelValidatorProvider.Configure();

            foreach (var setUp in IoCFactory.Instance.ResolveAll<ISetUp>().OrderBy(r => r.GetOrder()))
                setUp.Execute();

            TemplateHandlebarsFactory.GetVersion = () => Guid.NewGuid().ToString();

            var ajaxDef = JqueryAjaxOptions.Default;
            ajaxDef.Cache = false;
        }
    }
}