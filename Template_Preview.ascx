<%@ Control Language="C#" AutoEventWireup="false" Inherits="SatraBel.TemplateEditor.Template_Preview" Codebehind="Template_Preview.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">

    <fieldset>

        <div class="dnnFormItem">
            <asp:Literal ID="lContent" runat="server"></asp:Literal>
        </div>
    
        
   </fieldset>
</div>

<asp:HyperLink ID="hlThis" runat="server" Visible="false" Target="_blank">HyperLink</asp:HyperLink>        
