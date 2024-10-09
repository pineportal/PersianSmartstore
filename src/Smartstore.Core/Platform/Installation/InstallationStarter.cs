﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Smartstore.Engine.Builders;

namespace Smartstore.Core.Installation
{
    internal sealed class InstallationStarter : StarterBase
    {
        const string InstallControllerName = "install";

        public override bool Matches(IApplicationContext appContext)
            => !appContext.IsInstalled;

        public override void ConfigureContainer(ContainerBuilder builder, IApplicationContext appContext)
        {
            builder.RegisterType<InstallationService>().As<IInstallationService>().InstancePerLifetimeScope();

            // Register app languages for installation
            builder.RegisterType<FaIRSeedData>()
                .As<InvariantSeedData>()
                .WithMetadata<InstallationAppLanguageMetadata>(m =>
                {
                    m.For(em => em.Culture, "fa-IR");
                    m.For(em => em.Name, "Persian");
                    m.For(em => em.UniqueSeoCode, "fa");
                    m.For(em => em.FlagImageFileName, "ir.png");
                    m.For(em => em.Rtl, true);
                })
                .InstancePerRequest();
            builder.RegisterType<EnUSSeedData>()
                .As<InvariantSeedData>()
                .WithMetadata<InstallationAppLanguageMetadata>(m =>
                {
                    m.For(em => em.Culture, "en-US");
                    m.For(em => em.Name, "English");
                    m.For(em => em.UniqueSeoCode, "en");
                    m.For(em => em.FlagImageFileName, "us.png");
                    m.For(em => em.Rtl, false);
                })
                .InstancePerLifetimeScope();
            builder.RegisterType<DeDESeedData>()
                .As<InvariantSeedData>()
                .WithMetadata<InstallationAppLanguageMetadata>(m =>
                {
                    m.For(em => em.Culture, "de-DE");
                    m.For(em => em.Name, "Deutsch");
                    m.For(em => em.UniqueSeoCode, "de");
                    m.For(em => em.FlagImageFileName, "de.png");
                    m.For(em => em.Rtl, false);
                })
                .InstancePerLifetimeScope();

        }

        public override void BuildPipeline(RequestPipelineBuilder builder)
        {
            builder.Configure(StarterOrdering.EarlyMiddleware, app =>
            {
                app.Use(async (context, next) =>
                {
                    var routeValues = context.GetRouteData().Values;

                    if (!routeValues.GetControllerName().EqualsNoCase(InstallControllerName))
                    {
                        context.Response.Redirect(context.Request.PathBase.Value + "/" + InstallControllerName);
                        return;
                    }

                    await next();
                });
            });
        }
    }
}
