#region Copyright

// 
// Copyright (c) 2014
// by SatraBel
// 

#endregion

#region Using Statements

using System;

using DotNetNuke.Entities.Modules;
using System.Web;
using Satrabel.OpenBlocks.Token;

#endregion

namespace Satrabel.OpenBlocks
{

    public partial class Token_View : PortalModuleBase
	{
		#region Event Handlers
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);			
			if (!Page.IsPostBack)
			{
                PlaceHolder1.Visible = IsEditable;
                txtField.Text = (string)Settings["Token"];
                lContent.Text = TokenReplaceUtils.Replace(txtField.Text);
                //lContent.Text = (string)Settings["Token"];
                //lContent.Text = Server.HtmlEncode(lContent.Text);
                //txtField.Text = lContent.Text;
			}
		}		
		#endregion
	}
}

