<%@ Control Language="C#" AutoEventWireup="true" Inherits="SatraBel.OpenBlocks.TokenSettings" Codebehind="TokenSettings.ascx.cs" %>
<div class="dnnForm dnnSettings dnnClear" id="dnnSettings">
    <fieldset>
        <div class="dnnFormItem">
            <asp:label id="plField" runat="server" text="Token provider" helptext="Enter a value" controlname="ddlTokens" CssClass="dnnLabel" />           
            <asp:DropDownList ID="ddlTokens" runat="server" AutoPostBack="True" onselectedindexchanged="ddlTokens_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:PlaceHolder ID="phConfigurator" runat="server"></asp:PlaceHolder>
        </div>
   </fieldset>
</div>
<script type="text/javascript">
    function EditorClose() {
        <%= PostBackStr %>
    }    
</script>
<%= CloseDialogStr %>