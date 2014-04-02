using Nop.Core.Infrastructure;
using Nop.Services.Configuration;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.HtmlOptimiser.Code
{
    public class HtmlFilterAttribute : ActionFilterAttribute
    {
        private readonly ISettingService _settingService;

        private HtmlOptimiserSettings settings;

        public HtmlFilterAttribute()
        {
            _settingService = EngineContext.Current.Resolve<ISettingService>();

            settings = _settingService.LoadSetting<HtmlOptimiserSettings>();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (settings != null)
            {
                if (settings.RemoveHeaders != null)
                {
                    // remove any unneeded HTTP headers
                    try
                    {
                        foreach (var header in settings.RemoveHeaders)
                        {
                            filterContext.HttpContext.Response.Headers.Remove(header);
                        }
                    }
                    catch (PlatformNotSupportedException)
                    {
                        // ignore when using IIS Express
                    }
                }

                if (settings.AddHeaders != null)
                {
                    // add any specified HTTP headers
                    try
                    {
                        foreach (var header in settings.AddHeaders)
                        {
                            if (!filterContext.HttpContext.Response.Headers.AllKeys.Contains(header.Name))
                            {
                                filterContext.HttpContext.Response.Headers.Add(header.Name, header.Value);
                            }
                        }
                    }
                    catch (PlatformNotSupportedException)
                    {
                        // ignore when using IIS Express
                    }
                }

                if (settings.MinificationEnabled)
                {
                    // apply HTML minification
                    if (!filterContext.IsChildAction &&
                        !filterContext.HttpContext.Response.IsRequestBeingRedirected &&
                        filterContext.Exception == null &&
                        !(filterContext.Result is FileResult) &&
                        !(filterContext.Result is HttpUnauthorizedResult))
                    {
                        filterContext.HttpContext.Response.Filter =
                            new WhitespaceFilter(filterContext.HttpContext.Response.Filter);

                        try
                        {
                            // get session ID to prevent errors during initialization
                            string sessionId = filterContext.HttpContext.Session.SessionID;

                            // flush to prevent issues with other filters
                            filterContext.HttpContext.Response.Flush();
                        }
                        catch (HttpException)
                        {
                            // ignore when connection is already closed
                        }
                    }
                }
            }

            base.OnResultExecuted(filterContext);
        }
    }
}
