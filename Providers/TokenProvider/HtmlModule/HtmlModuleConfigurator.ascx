<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.Token.HtmlModuleConfigurator" Codebehind="HtmlModuleConfigurator.ascx.cs" %>


<div class="dnnFormItem">
    <asp:label id="lblField" runat="server" text="Block" helptext="Enter a value"
        controlname="ddlModules" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="true">
    </asp:DropDownList>
</div>
