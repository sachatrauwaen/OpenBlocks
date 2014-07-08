<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Satrabel.OpenBlocks.DataSource.DnnConfigurator" Codebehind="DnnConfigurator.ascx.cs" %>

<div class="dnnFormItem">
    <asp:label id="Label2" runat="server" text="Data source" helptext="Enter a value"
        controlname="txtSource" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlSource" runat="server">
        <asp:ListItem Selected="True">Files</asp:ListItem>
    </asp:DropDownList>
</div>
<div class="dnnFormItem">
    <asp:label id="plField" runat="server" text="Url" helptext="Enter a value"
        controlname="txtUrl" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlFolder" runat="server"></asp:DropDownList>
</div>
<div class="dnnFormItem">
    <asp:label id="Label1" runat="server" text="Search patern" helptext="Enter a value"
        controlname="tbSearchPatern" CssClass="dnnLabel" />
    <asp:TextBox ID="tbSearchPatern" runat="server" Text="*.jpg"></asp:TextBox>
</div>
