<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token_Settings" Codebehind="Token_Settings.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnSettings dnnClear" id="dnnSettings">

    <fieldset>

        <div class="dnnFormItem">
            <dnn:label id="plField" runat="server" text="Token provider" helptext="Enter a value" controlname="ddlTokens" />           
            <asp:DropDownList ID="ddlTokens" runat="server" AutoPostBack="True" onselectedindexchanged="ddlTokens_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:PlaceHolder ID="phConfigurator" runat="server"></asp:PlaceHolder>
        </div>

   </fieldset>

</div>
<asp:HiddenField ID="hfToken" runat="server" />
