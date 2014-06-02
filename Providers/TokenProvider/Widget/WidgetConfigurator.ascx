<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token.WidgetConfigurator" Codebehind="WidgetConfigurator.ascx.cs" %>

<div class="dnnFormItem">
    <asp:label id="lTemplate" runat="server" text="Template" helptext="Enter a value" controlname="txtField" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
        <asp:ListItem Value="" resourceKey="selectTemplate" Text="--select--"></asp:ListItem>
    </asp:DropDownList>
</div>
<div class="dnnFormItem">
    <asp:label id="lFile" runat="server" text="File" helptext="Enter a value" controlname="txtField" CssClass="dnnLabel" />
    <asp:DropDownList ID="ddlFile" runat="server" AutoPostBack="True">
        <asp:ListItem Value="" resourceKey="selectFile" Text="--select--"></asp:ListItem>
    </asp:DropDownList>
</div>

 <div class="dnnFormItem">
    <asp:label id="plField" runat="server" text="DataSource" helptext="Enter a value" controlname="ddlDataSource" CssClass="dnnLabel" />           
    <asp:DropDownList ID="ddlDataSource" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDataSource_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:PlaceHolder ID="phConfigurator" runat="server"></asp:PlaceHolder>
</div>