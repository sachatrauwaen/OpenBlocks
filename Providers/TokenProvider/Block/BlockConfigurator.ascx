<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token.BlockConfigurator" Codebehind="BlockConfigurator.ascx.cs" %>


<div class="dnnFormItem">
    <asp:label id="lblField" runat="server" text="Block" helptext="Enter a value"
        controlname="ddlBlock" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true">
    </asp:DropDownList>
</div>
