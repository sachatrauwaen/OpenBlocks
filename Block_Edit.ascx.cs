/*
' Copyright (c) 2014  Satrabel.be
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
using DotNetNuke.Entities.Users;
using Satrabel.OpenBlocks.Block;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Services.Localization;
using System.Web.UI.WebControls;

namespace Satrabel.OpenBlocks
{
    public partial class Block_Edit : OpenBlocksModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module
                if (!Page.IsPostBack)
                {
                    //get a list of users to assign the user to the Object
                    /*
                    ddlAssignedUser.DataSource = UserController.GetUsers(PortalId);
                    ddlAssignedUser.DataTextField = "Username";
                    ddlAssignedUser.DataValueField = "UserId";
                    ddlAssignedUser.DataBind();
                    */
                    ddlCultureCode.DataSource = LocaleController.Instance.GetLocales(PortalId).Values;
                    ddlCultureCode.DataTextField = "NativeName";
                    ddlCultureCode.DataValueField = "Code";
                    ddlCultureCode.DataBind();
                    //check if we have an ID passed in via a querystring parameter, if so, load that item to edit.
                    //ItemId is defined in the ItemModuleBase.cs file
                    if (ItemId > 0)
                    {
                        var tc = new BlockController();
                        var t = tc.GetBlock(ItemId, PortalId);
                        if (t != null)
                        {
                            rblType.SelectedValue = t.BlockType.ToString();
                            txtName.Text = t.Name;
                            txtContentTxt.Text = t.Content;
                            ((TextEditor)txtContentHtml).Text = t.Content;
                            ListItem li =  ddlCultureCode.Items.FindByValue(t.CultureCode);
                            if (li != null)
                            {
                                li.Selected = true;
                            }
                            txtContentTxt.Visible = t.BlockType == 1;
                            txtContentHtml.Visible = t.BlockType == 2;
                        }
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var t = new Satrabel.OpenBlocks.Block.Block();
            var tc = new BlockController();
            if (ItemId > 0)
            {
                t = tc.GetBlock(ItemId, PortalId);                              
                t.LastModifiedByUserId = UserId;
                t.LastModifiedOnDate = DateTime.Now;
                //t.AssignedUserId = Convert.ToInt32(ddlAssignedUser.SelectedValue);                
            }
            else
            {
                t = new Satrabel.OpenBlocks.Block.Block()
                {
                    //AssignedUserId = Convert.ToInt32(ddlAssignedUser.SelectedValue),
                    CreatedByUserId = UserId,
                    CreatedOnDate = DateTime.Now,                   
                };
            }
            t.Name = txtName.Text.Trim();
            t.CultureCode = ddlCultureCode.SelectedIndex > 0 ? ddlCultureCode.SelectedValue : "";
            t.BlockType = int.Parse(rblType.SelectedValue);
            if (t.BlockType == 1)
                t.Content = txtContentTxt.Text.Trim();
            else if (t.BlockType == 2)
                t.Content = ((TextEditor)txtContentHtml).Text;
            t.LastModifiedOnDate = DateTime.Now;
            t.LastModifiedByUserId = UserId;
            t.PortalId = PortalId;

            if (t.BlockId > 0)
            {
                tc.UpdateBlock(t);
            }
            else
            {
                tc.CreateBlock(t);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContentTxt.Visible = rblType.SelectedValue == "1";
            txtContentHtml.Visible = rblType.SelectedValue == "2";
        }
    }
}