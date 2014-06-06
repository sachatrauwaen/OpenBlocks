<%@ Control Language="C#" AutoEventWireup="true" Inherits="Satrabel.OpenBlocks.Token_View" CodeBehind="Token_View.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>


<asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <asp:TextBox ID="txtField" runat="server" Width="99%" BorderStyle="Dashed"  BorderWidth="3" BorderColor="Gray" />
</asp:PlaceHolder>
<asp:Literal ID="lContent" runat="server"></asp:Literal>
