using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.Html;
using DotNetNuke.Common.Utilities;

namespace Satrabel.OpenBlocks.Token
{
    public class HtmlModuleTokenProvider : TokenProvider
    {

        public override string Execute(Dictionary<string, string> parameters)
        {
            int ModuleId = Null.NullInteger;
            if (int.TryParse(parameters["moduleid"], out ModuleId))
            {
                return HttpUtility.HtmlDecode(GetHtmlText(ModuleId));
            }

            return "";
        }

        public static string GetHtmlText(int ModuleId)
        {
            HtmlTextController hc = new HtmlTextController();
            var html = hc.GetTopHtmlText(ModuleId, true, Null.NullInteger);
            if (html != null)
                return html.Content;
            

            return "";
        }

    }}