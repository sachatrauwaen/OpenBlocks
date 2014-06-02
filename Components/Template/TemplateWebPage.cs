using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using DotNetNuke.Web.Razor;

namespace Satrabel.Providers.TemplateEngine
{

    public abstract class TemplateWebPage : DotNetNukeWebPage
    {
        #region Helpers

        protected internal TemplateHelper Template { get; internal set; }

        #endregion

        #region BaseClass Overrides

        protected override void ConfigurePage(WebPageBase parentPage)
        {
            base.ConfigurePage(parentPage);

            //Child pages need to get their context from the Parent
            Context = parentPage.Context;
        }

        #endregion
    }
    
    public abstract class TemplateWebPage<T> : TemplateWebPage
    {
        public T Model { get; set; }
    }
    
}