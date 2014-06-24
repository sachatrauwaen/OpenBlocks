
using Satrabel.OpenBlocks.Block;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Satrabel.OpenBlocks.Token
{
    public partial class BlockConfigurator : TokenConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BlockController bc = new BlockController();
                var blocks = bc.GetBlocks(PortalId);
                ddlBlock.DataSource = blocks.Select(b => b.Name).Distinct();
                ddlBlock.DataBind();
            }
        }

        public override void LoadSettings(Hashtable settings)
        {
           
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