<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Template_settings.ascx.cs" Inherits="Satrabel.OpenBlocks.Template_settings" %>
<ul class="dnnActions dnnClear">
    <li>
        <asp:LinkButton ID="lbCKEditor" resourceKey="lbCKEditor" runat="server" CssClass="dnnSecondaryAction"
            EnableViewState="False" OnClick="lbCKEditor_Click" />
    </li>
   <li>
        <asp:LinkButton ID="lbBlockPages" resourceKey="lbBlockPages" runat="server" CssClass="dnnSecondaryAction"
            EnableViewState="False" OnClick="lbBlockPages_Click" />
    </li>
</ul>
