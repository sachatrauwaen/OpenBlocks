<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token.HtmlModuleConfigurator" Codebehind="HtmlModuleConfigurator.ascx.cs" %>


<div class="dnnFormItem">
    <asp:label id="lblField" runat="server" text="Module" helptext="Enter a value"
        controlname="ddlModules" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="true">
    </asp:DropDownList>
</div>
