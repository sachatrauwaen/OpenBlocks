/*
' Copyright (c) 2014  Satrabel
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Satrabel.OpenBlocks.Block;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace Satrabel.OpenBlocks
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Block_View : OpenBlocksModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    hlAdd.NavigateUrl = EditUrl("Edit");
                    var tc = new BlockController();


                    gvBlocks.DataSource = tc.GetBlocks(PortalId);
                    gvBlocks.DataBind();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void gvBlocks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;
                var lnkDelete = e.Row.FindControl("lnkDelete") as ImageButton;


                var t = (Satrabel.OpenBlocks.Block.Block)e.Row.DataItem;

                if (lnkDelete != null && lnkEdit != null)
                {

                    lnkDelete.CommandArgument = t.BlockId.ToString();
                    lnkDelete.Enabled = lnkDelete.Visible = lnkEdit.Visible = true;

                    lnkEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Edit", "id=" + t.BlockId);

                    ClientAPI.AddButtonConfirm(lnkDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
                }

            }
        }

        protected void gvBlocks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(EditUrl(string.Empty, string.Empty, "Edit", "id=" + e.CommandArgument));
            }

            if (e.CommandName == "Delete")
            {
                var tc = new BlockController();
                tc.DeleteBlock(Convert.ToInt32(e.CommandArgument), ModuleId);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected string Scope(object oCultureCode)
        {
            string CultureCode = (string)oCultureCode;
            if (!string.IsNullOrEmpty(CultureCode))
            {
                return "Language : " + CultureCode;
            }
            return "";
        }
        protected string Token(object oName)
        {            
            return "{{block name=\""+oName+"\"}}";
        }
        protected string BlockType(object oBlockType)
        {
            int BlockType = (int)oBlockType;
            return GetBlockTypeDic()[BlockType];
        }
        private Dictionary<int, string> GetBlockTypeDic() { 
            Dictionary<int, string> BlockTypeDic = new Dictionary<int, string>();
            BlockTypeDic.Add(1, "Text");
            BlockTypeDic.Add(2, "Html");
            
            return BlockTypeDic;
        }
    }
}