<%@ Control Language="C#" AutoEventWireup="True" Inherits="SatraBel.OpenBlocks.Template_Edit" CodeBehind="Template_Edit.ascx.cs" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm" id="templateEditor">
    <fieldset>
        <div class="dnnFormItem">
            <asp:RadioButtonList ID="rblMode" runat="server" Style="margin-left: 25%; margin-bottom: 3%"
                RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMode_SelectedIndexChanged"
                AutoPostBack="True">
                <asp:ListItem Value="0" resourceKey="emptyTemplate" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" resourceKey="copyTemplate"></asp:ListItem>
                <asp:ListItem Value="2" resourceKey="unzipTemplate"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="dnnFormItem">

            <div style="display: inline-block; width: 30%">
                <asp:Label ID="Label1" runat="server" Text="Module"></asp:Label><br />
                <asp:DropDownList ID="ddlModule" runat="server" DataTextField="FriendlyName" Width="100%"
                    DataValueField="FolderName" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                    
                </asp:DropDownList>
            </div>
            <div style="display: inline-block; width: 30%">
                <asp:Label ID="Label2" runat="server" Text="Storage"></asp:Label><br />
                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="100%"
                    AutoPostBack="True">
                    <asp:ListItem Value="1" resourceKey="selectSyst"></asp:ListItem>
                    <asp:ListItem Value="2" resourceKey="selectHost"></asp:ListItem>
                    <asp:ListItem Value="3" resourceKey="selectSite" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="display: inline-block; width: 30%">
                <asp:Label ID="Label3" runat="server" Text="Template"></asp:Label><br />
                <asp:TextBox ID="tbxNewTemplate" runat="server" placeholder="Name of the new template" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div style="height: 40px">
            <asp:RequiredFieldValidator ID="rfvNewTemplate" runat="server" Style="margin-left: 650px;"
                ControlToValidate="tbxNewTemplate" resourceKey="rfvNewTemplate" CssClass="dnnFormMessage dnnFormError"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="dnnFormItem widthauto">
            <dnn:Label ID="lblMode" runat="server" CssClass="dnnForm" Visible="false"></dnn:Label>
            <div style="white-space: nowrap">
                <div style="display: inline-block; width: 30%">
                    <asp:Label ID="lblTypeCopy" runat="server" Text="Storage" Visible="false"></asp:Label><br />
                    <asp:DropDownList ID="ddlTypeCopy" runat="server" Width="100%" Visible="false" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTypeCopy_SelectedIndexChanged">
                        <asp:ListItem Value="" resourceKey="selectType"></asp:ListItem>
                        <asp:ListItem Value="1" resourceKey="selectSyst"></asp:ListItem>
                        <asp:ListItem Value="2" resourceKey="selectHost"></asp:ListItem>
                        <asp:ListItem Value="3" resourceKey="selectSite"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="display: inline-block; width: 30%">
                    <asp:Label ID="lblTemplate" runat="server" Text="Template" Visible="false"></asp:Label><br />
                    <asp:DropDownList ID="ddlTemplate" runat="server" Width="100%" AutoPostBack="True"
                        Visible="false" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
                        <asp:ListItem Value="" resourceKey="selectTemplate"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="display: inline-block; width: 30%">
                    <asp:Label ID="lblZip" runat="server" Text="Zip file" Visible="false"></asp:Label><br />
                    <asp:FileUpload ID="updZip" runat="server" Visible="false" />
                </div>
            </div>
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li>
            <asp:Button ID="btnValid" resourcekey="btnValid" runat="server" CssClass="dnnPrimaryAction"
                OnClick="btnValid_Click"  EnableViewState="False" /></li>
        <li>
            <asp:Button ID="btnCancel" resourcekey="btnCancel" runat="server" CssClass="dnnSecondaryAction"
                OnClick="btnCancel_Click" CausesValidation="False" /></li>
    </ul>
    <asp:Label ID="lblemptyFile" runat="server" CssClass="dnnFormMessage dnnFormError"
        Visible="false" Style="width: 20%" EnableViewState="False"></asp:Label>
</div>
