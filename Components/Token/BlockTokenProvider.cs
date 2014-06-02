using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using Satrabel.Modules.OpenBlocks.Components;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Localization;

namespace Satrabel.Token
{
    public class BlockTokenProvider : TokenProvider
    {
        public override string Execute(Dictionary<string, string> parameters)
        {
            string Name = parameters["name"];
            if (string.IsNullOrEmpty(Name))
                return string.Format("[TokenProvider : parameter {0} missing]", Name);
            int PortalId = PortalSettings.Current.PortalId;
            BlockController bc = new BlockController();
            var b = bc.GetBlock(Name, PortalId, LocaleController.Instance.GetCurrentLocale(PortalId).Code);
            if (b != null)
            {
                return string.IsNullOrEmpty(b.Content) ? "" : TokenReplaceUtils.Replace(HttpUtility.HtmlDecode(b.Content));
            }
            else
            { 
                return string.Format("[TokenProvider : no block with name {0} exist]", Name);
            }
        }

    }
}