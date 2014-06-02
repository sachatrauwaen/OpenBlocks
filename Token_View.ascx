<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token_View"  Codebehind="Token_View.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">
    <fieldset>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" >
        <div class="dnnFormItem">
            <dnn:label id="plField" runat="server" Text="Token" helptext="Enter a value" controlname="txtField" />
            <asp:textbox id="txtField" runat="server" TextMode="MultiLine" />         
        </div>        
        </asp:PlaceHolder>
        <div class="dnnFormItem">
            <asp:Literal ID="lContent" runat="server" ></asp:Literal>
        </div>    
   </fieldset>
</div>
