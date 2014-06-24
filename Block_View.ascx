<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Block_View.ascx.cs" Inherits="Satrabel.OpenBlocks.Block_View" %>

<div class="dnnForm" id="form-blocks">
    <asp:GridView ID="gvBlocks" runat="server" CssClass="dnnGrid" AutoGenerateColumns="False" OnRowCommand="gvBlocks_RowCommand" OnRowDataBound="gvBlocks_RowDataBound" HeaderStyle-CssClass="dnnGridHeader" RowStyle-CssClass="dnnGridItem" Width="100%">
        <Columns>
           
            <asp:TemplateField ItemStyle-Width="20px">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkEdit" runat="server" ResourceKey="EditItem.Text" Visible="false" ImageUrl="~/icons/sigma/Edit_16X16_Standard.png" />
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20px">
                <ItemTemplate>
                    <asp:ImageButton ID="lnkDelete" runat="server" ResourceKey="DeleteItem.Text" Visible="false" CommandName="Delete" ImageUrl="~/icons/sigma/Delete_16X16_Standard.png" />
                </ItemTemplate>

            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
             <asp:TemplateField HeaderText="Type">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# BlockType(Eval("BlockType")) %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Scope">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="lblCulture" runat="server" Text='<%# Scope(Eval("CultureCode")) %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Token">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="lblToken" runat="server" Text='<%# Token(Eval("Name")) %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <HeaderStyle CssClass="dnnGridHeader"></HeaderStyle>

        <RowStyle CssClass="dnnGridItem"></RowStyle>

    </asp:GridView>

    <ul class="dnnActions dnnClear">
      
        <li>
            <asp:HyperLink runat="server" CssClass="dnnPrimaryAction"  ResourceKey="EditModule" ID="hlAdd" /></li>
    </ul>

</div>
