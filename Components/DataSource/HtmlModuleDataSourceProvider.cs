
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Satrabel.OpenBlocks.DataSource;
using DotNetNuke.Modules.Html;
using DotNetNuke.Common.Utilities;


namespace Satrabel.OpenBlocks.DataSource
{

    public class HtmlModuleDataSourceProvider : DataSourceProvider
    {

        public static HtmlModel Html(int moduleId)
        {
            HtmlTextController hc = new HtmlTextController();
            var html = hc.GetTopHtmlText(moduleId, true, Null.NullInteger);
            if (html != null)
                return new HtmlModel { Content = HttpUtility.HtmlDecode(html.Content), Summary = html.Summary };
            else
                return null;

        }
    }

    public class HtmlModel 
    {
        
        public string Content { get; set; }
        public string Summary { get; set; }

    }
}