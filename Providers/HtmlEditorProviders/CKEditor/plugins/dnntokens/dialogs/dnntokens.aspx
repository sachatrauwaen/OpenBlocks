<%@ Page Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token.DnnTokens" Codebehind="dnntokens.aspx.cs" %>

<%@ Register Src="~/DesktopModules/OpenBlocks/TokenSettings.ascx" TagPrefix="uc1" TagName="TokenSettings" %>
<!DOCTYPE html>
<html lang="en">
  <head>
   <title>DNN Tokens</title>
   <style type="text/css">
        body { font: normal 12px Arial,Helvetica,Tahoma,Verdana,Sans-Serif; }
        .dnnForm fieldset {
            clear: none;
            position: relative;
            margin-bottom: 18px;
            text-align: left;
            border : none;
        }
        .dnnForm .dnnFormItem {
            clear: both;
            width: 100%;
            display: block;
            position: relative;
            text-align: left;
        }

        .dnnLabel {
            display: inline-block;
            float: left;
            position: relative;
            width: 32.075%;
            padding-right: 20px;
            margin-right: 18px;
            overflow: visible;
            text-align: right;
            margin-top: 5px;
        }
   </style>
      <link href="/portals/_default/default.css" type="text/css" rel="stylesheet"/>
      <link href="/portals/_default/skins/_default/webcontrolskin/default/combobox.default.css" type="text/css" rel="stylesheet"/>
  </head>
 <body>
     <form id="DialogForm" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <uc1:TokenSettings runat="server" id="TokenSettings" />
	</form>
  </body>
</html>
