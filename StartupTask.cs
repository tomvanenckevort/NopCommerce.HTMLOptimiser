using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.HtmlOptimiser.Code;
using Nop.Services.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.HtmlOptimiser
{
    public class StartupTask : IStartupTask
    {
        private bool Installed
        {
            get
            {
                var pluginFinder = EngineContext.Current.Resolve<IPluginFinder>();

                return (pluginFinder != null && 
                        pluginFinder.GetPluginDescriptorBySystemName("Misc.HtmlOptimiser", true) != null);
            }
        }

        private IEnumerable<string> RemoveHeaders
        {
            get
            {
                var settingService = EngineContext.Current.Resolve<ISettingService>();

                var settings = settingService.LoadSetting<HtmlOptimiserSettings>();

                if (settings != null && settings.RemoveHeaders != null)
                {
                    return settings.RemoveHeaders;
                }
                else
                {
                    return Enumerable.Empty<string>();
                }
            }
        }

        public void Execute()
        {
            if (Installed)
            {
                // register type converter
                TypeDescriptor.AddAttributes(typeof(List<AddHeader>), new TypeConverterAttribute(typeof(AddHeaderTypeConverter)));

                // check if web.config needs updating (i.e. after an upgrade)
                WebConfigUpdater.UpdateWebConfig(RemoveHeaders);

                // remove old filter before adding new one
                var currentFilter = GlobalFilters.Filters.FirstOrDefault(f => f.Instance.GetType() == typeof(HtmlFilterAttribute));

                if (currentFilter != null)
                {
                    GlobalFilters.Filters.Remove(currentFilter.Instance);
                }

                GlobalFilters.Filters.Add(new HtmlFilterAttribute() { Order = 100 });
            }
        }

        public int Order
        {
            get
            {
                return 100;
            }
        }
    }
}
