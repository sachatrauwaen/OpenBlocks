<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Block_Edit.ascx.cs" Inherits="Satrabel.Modules.OpenBlocks.Block_Edit" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="texteditor" Src="~/controls/texteditor.ascx" %>

<div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings">

    <ul class="dnnAdminTabNav dnnClear">
        <li><a href="#rbDefinition"><%=LocalizeString("DefinitionSettings")%></a></li>
        <li><a href="#rbScope"><%=LocalizeString("ScopeSettings")%></a></li>
        <li><a href="#rbContent"><%=LocalizeString("BasicSettings")%></a></li>
    </ul>
    <div class="rbDefinition dnnClear" id="rbDefinition">
        <div class="rbmContent dnnClear">
            <fieldset>

                <div class="dnnFormItem">
                    <dnn:label ID="lblName" runat="server" />
                    <asp:TextBox ID="txtName" runat="server" CssClass="dnnFormRequired" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" 
                CssClass="dnnFormMessage dnnFormError" ResourceKey="Name.Required" />
                </div>
                <div class="dnnFormItem">
                    <dnn:label ID="lblType" runat="server"  />
                    <asp:RadioButtonList ID="rblType" runat="server" RepeatColumns="3" OnSelectedIndexChanged="rblType_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Text" Selected="True" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Html" Value="2"></asp:ListItem>

                    </asp:RadioButtonList>
                </div>

            </fieldset>
        </div>
    </div>
    <div class="rbScope dnnClear" id="rbScope">
        <div class="rbmContent dnnClear">
            <fieldset>
                <div class="dnnFormItem">
                    <dnn:label ID="lblCultureCode" runat="server" />
                    <asp:DropDownList ID="ddlCultureCode" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="rbContent dnnClear" id="rbContent">
        <div class="rbmContent dnnClear">
            <fieldset>

                <div class="dnnFormItem">

                    <dnn:label ID="lblDescription" runat="server" Visible="false" />
                    <asp:TextBox ID="txtContentTxt" runat="server" TextMode="MultiLine" Rows="15" Width="100%" />
                    <dnn:texteditor ID="txtContentHtml" runat="server" Visible="false"/>


                </div>
            </fieldset>
        </div>
    </div>
    <ul class="dnnActions dnnClear" style="padding:0">
        <li>
            <asp:LinkButton ID="btnSubmit" runat="server"
                OnClick="btnSubmit_Click" resourcekey="btnSubmit" CssClass="dnnPrimaryAction" /></li>
        <li>
            <asp:LinkButton ID="btnCancel" runat="server"
                OnClick="btnCancel_Click" resourcekey="btnCancel" CssClass="dnnSecondaryAction" /></li>
    </ul>
</div>
<script language="javascript" type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#dnnEditBasicSettings').dnnTabs();

            $('#dnnEditBasicSettings').dnnPanels();
            $('#dnnEditBasicSettings .dnnFormExpandContent a').dnnExpandAll({ expandText: '<%=Localization.GetString("ExpandAll", LocalResourceFile)%>', collapseText: '<%=Localization.GetString("CollapseAll", LocalResourceFile)%>', targetArea: '#dnnEditBasicSettings' });
            }

            $(document).ready(function () {
                dnnEditBasicSettings();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    dnnEditBasicSettings();
                });
            });

        }(jQuery, window.Sys));
</script>
