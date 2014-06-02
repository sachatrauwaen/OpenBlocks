<%@ Control Language="C#" AutoEventWireup="false" Inherits="SatraBel.OpenBlocks.Template_DataSource" CodeBehind="Template_DataSource.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm" id="templateEditorDataSource">
    <fieldset>
        <div class="dnnFormItem" >
            <asp:Label ID="plField" runat="server" Text="DataSource" helptext="Enter a value" controlname="ddlDataSource" CssClass="dnnLabel" />
            <asp:DropDownList ID="ddlDataSource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDataSource_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:PlaceHolder ID="phConfigurator" runat="server"></asp:PlaceHolder>
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li>
            <asp:Button ID="btnSave" resourcekey="btnSave" runat="server" CssClass="dnnPrimaryAction"
                OnClick="btnSave_Click"  EnableViewState="False" /></li>
        <li>
            <asp:Button ID="btnCancel" resourcekey="btnCancel" runat="server" CssClass="dnnSecondaryAction"
                OnClick="btnCancel_Click" CausesValidation="False" />

        </li>
    </ul>
</div>
