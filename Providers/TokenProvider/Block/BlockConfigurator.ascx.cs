
using Satrabel.Modules.OpenBlocks.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Satrabel.Token
{
    public partial class BlockConfigurator : TokenConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void LoadSettings(Hashtable settings)
        {
            BlockController bc = new BlockController();
            var blocks = bc.GetBlocks(PortalId);
            ddlBlock.DataSource = blocks.Select(b => b.Name).Distinct();
            ddlBlock.DataBind();
            try
            {
                ddlBlock.SelectedValue = (string)settings["Block_Name"];
            }
            catch (Exception)
            {

            }
        }

        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("Block_Name", ddlBlock.SelectedValue);
            return settings;
        }

        public override string getToken()
        {
            return "{{block name=\"" + ddlBlock.SelectedValue + "\" }}";
            
        }
    }
}