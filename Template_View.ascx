<%@ Control Language="C#" AutoEventWireup="true" Inherits="SatraBel.OpenBlocks.Template_View" CodeBehind="Template_View.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<%-- Custom CSS Registration --%>
<dnn:DnnCssInclude ID="DnnCssInclude3" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/display/fullscreen.css" />
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/lib/codemirror.css" />
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/show-hint.css" />
<%-- Custom JavaScript Registration --%>
<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/lib/codemirror.js"
    Priority="101" />

<dnn:DnnJsInclude ID="DnnJsInclude17" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/edit/closebrackets.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude9" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/show-hint.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude7" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/css-hint.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude10" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/xml-hint.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude11" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/html-hint.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude12" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/hint/javascript-hint.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude13" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/addon/display/fullscreen.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/clike/clike.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude4" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/vb/vb.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude5" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/xml/xml.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude6" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/javascript/javascript.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude2" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/css/css.js"
    Priority="102" />
<dnn:DnnJsInclude ID="DnnJsInclude8" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror/mode/htmlmixed/htmlmixed.js"
    Priority="102" />

<dnn:DnnCssInclude ID="DnnCssInclude4" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-extension/addon/hint/show-hint-eclipse.css" />

<dnn:DnnJsInclude ID="DnnJsInclude14" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-extension/addon/hint/show-context-info.js"
    Priority="103" />
<dnn:DnnCssInclude ID="DnnCssInclude5" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-extension/addon/hint/show-context-info.css" />
<dnn:DnnCssInclude ID="DnnCssInclude6" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-extension/addon/hint/templates-hint.css" />

<dnn:DnnJsInclude ID="DnnJsInclude15" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-extension/addon/hint/templates-hint.js"
    Priority="103" />

<dnn:DnnJsInclude ID="DnnJsInclude16" runat="server" FilePath="~/DesktopModules/OpenBlocks/js/CodeMirror-dnn/addon/hint/dnn-templates.js"
    Priority="104" />


<asp:Panel ID="ScopeWrapper" runat="server" CssClass="">
    <div class="dnnForm" id="templateEditor">
        <fieldset>
            <div class="dnnFormItem" style="width: 100%;">
                 
                <div style="display: inline-block">
                    <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label><br />
                    <asp:DropDownList ID="ddlModule" runat="server" DataTextField="FriendlyName"
                        DataValueField="FolderName" AutoPostBack="true" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" Width="230">
                        <asp:ListItem Value="0" resourceKey="selectModule"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div style="display: inline-block">
                    <asp:Label ID="lblTemplate" runat="server" Text="Template"></asp:Label><br />
                    <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" Width="230">
                        <asp:ListItem Value="" resourceKey="selectTemplate"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="display: inline-block">
                    <asp:CheckBox ID="cbFullScreen" runat="server" Text="Full Responsive" ClientIDMode="Static" OnCheckedChanged="cbFullScreen_CheckedChanged" AutoPostBack="true" />
                </div>
                <div style="display: inline-block;float:right;padding-top:25px;padding-left:0px;">
                    <asp:HyperLink ID="hlSettings" runat="server" ImageUrl="~/DesktopModules/OpenBlocks/Images/settings_32x32.png" ToolTip="Settings" />
                </div>
               <div style="display: inline-block;float:right;padding-top:25px;padding-left:0px;">
                    <asp:HyperLink ID="hlHelp" runat="server" ImageUrl="~/DesktopModules/OpenBlocks/Images/help-32.png" ToolTip="Help" NavigateUrl="https://openblocks.codeplex.com/documentation" Target="_blank" />
                </div>
                <div style="display: inline-block;float:right;padding-top:23px;padding-left:10px;">
                    <asp:HyperLink ID="hlHome" runat="server" ImageUrl="~/DesktopModules/OpenBlocks/Images/Home-32.png" ToolTip="Home page" />
                </div>
                 <div style="display: inline-block;float:right;padding-top:30px;">
                    <span style="font-size:30px;font-weight:bold">Template Studio</span>
                </div>
            </div>
            <div class="editorContainer">
                <div class="col1">
                    <dnn:DnnFileExplorer runat="server" ID="dfeTree" ExplorerMode="FileTree" Width="99%" Configuration-MaxUploadFileSize="10000000"
                        RenderMode="Classic" EnableCopy="True" EnableOpenFile="false" OnClientFileOpen="OnClientItemSelected"  
                        OnClientLoad="OnClientLoad" Height="100%" >
                    </dnn:DnnFileExplorer>
                </div>
                <div class="col2">
                    <div class="dnnFormItem" style="background-color: #aaa; color: #fff;border: 1px solid #aaa;height:18px">
                        <asp:Label ID="lFileName" runat="server"></asp:Label>
                    </div>
                    <div class="dnnFormItem" id="divEditor">
                        <asp:TextBox ID="tbxEdit" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="dnnFormItem" style="text-align: center; height: 482px; overflow: auto; width: 100%; display: none" id="divImg">
                        <asp:Image ID="imgEdit" runat="server" CssClass="imgEdit" EnableViewState="False" />
                    </div>
                    <div class="dnnFormItem" style="height: 482px; overflow: auto; width: 100%; display: none" id="divRun">
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <ul class="dnnActions dnnClear">
            <li>
                <asp:HyperLink ID="lbCreate" resourceKey="lkbCreate" runat="server" CssClass="dnnSecondaryAction"
                    EnableViewState="False" />
            </li>
            <li>
                <asp:LinkButton ID="btnZip" runat="server" OnClick="btnZip_Click" resourcekey="btnZip"
                    Visible="false" CssClass="dnnSecondaryAction" /></li>
            <li>
                <asp:LinkButton ID="lkbDelete" resourceKey="lkbDelete" runat="server" CssClass="dnnSecondaryAction"
                    Visible="false" EnableViewState="False" OnClick="lkbDelete_Click" />
            </li>
            <asp:PlaceHolder ID="phToolbar" runat="server" Visible="true">
                <li>
                    <div style="border-left: 1px solid #000; padding-right: 5px; margin-left: 5px; height: 30px;"></div>
                </li>
                <li>
                    <asp:HyperLink ID="lkbSave" resourcekey="lkbSave" runat="server" CssClass="dnnPrimaryAction" EnableViewState="False" />
                </li>
                <li>
                    <asp:HyperLink ID="hlDataSource" runat="server" resourcekey="lkbDataSource" CssClass="dnnSecondaryAction" EnableViewState="False" />
                </li>
                <li>
                    <asp:HyperLink ID="hlPreview" resourcekey="hlPreview" runat="server" CssClass="dnnSecondaryAction" EnableViewState="False" />
                </li>
                <li>
                    <asp:HyperLink ID="lkbData" runat="server" resourcekey="lkbData" CssClass="dnnSecondaryAction" EnableViewState="False" />
                </li>
                <li>
                    <asp:HyperLink ID="hlWidget" runat="server" resourcekey="hlWidget" CssClass="dnnSecondaryAction" EnableViewState="False" />
                </li>
                 <li>
                    <div style="border-left: 1px solid #000; padding-right: 5px; margin-left: 5px; height: 30px;"></div>
                </li>
                 <li style="float:right;padding-top:10px;">
                    F11 : Fullscreen | CTRL-S : Save | CTRL-SPACE : autocomplete
                </li>
                 <li>
              
            </li>
            </asp:PlaceHolder>
        </ul>
        
    </div>
</asp:Panel>
<asp:Label ID="lblMsg" runat="server" Style="width: 25%; text-align: center" CssClass="dnnFormMessage dnnFormSuccess"
    Visible="false" EnableViewState="False"></asp:Label>
<script type="text/javascript">
    $(function () {
        InitCodeMirror();
    });
    var cm;
    var cmchanged = false;
    function InitCodeMirror() {
        if ($("textarea[id$='tbxEdit']").length > 0) {

            CodeMirror.registerHelper("hint", "templates", templateHint);

            function myHint(cm) {
                return CodeMirror.showHint(cm, CodeMirror.ternHint, { async: true });
            }

            function templateHint(cm, options) {
                var cur = cm.getCursor(), token = cm.getTokenAt(cur);
                var completions = [];
                if (token && token.string.slice(0, 1) == "@") {

                    CodeMirror.templatesHint.getCompletions(cm, completions, token.string);

                    completions2 = getCompletions(completions, cur, token, CodeMirror.htmlHint);
                    return completions2;
                }
                else {
                    var completions1 = CodeMirror.hint.auto(cm, options);
                    return completions1;
                    if (completions1 && completions1.list.length > 0) {

                    }
                }

                /*
                for (var j = 0; j < completions1.list.length; j++) {
                    if (completions2.list.indexOf(completions1.list[j]) === -1) {
                        completions2.list.push(completions1.list[j]);
                    }
                }
                */


            }
            var Pos = CodeMirror.Pos;
            function getCompletions(completions, cur, token, options, showHint) {
                var sortedCompletions = completions.sort(function (a, b) {
                    var s1 = a.text; // getKeyWord(a);
                    var s2 = b.text; // getKeyWord(b);
                    var nameA = s1.toLowerCase(), nameB = s2.toLowerCase()
                    if (nameA < nameB) // sort string ascending
                        return -1
                    if (nameA > nameB)
                        return 1
                    return 0 // default return value (no sorting)
                });
                var data = {
                    list: sortedCompletions,
                    from: Pos(cur.line, token.start),
                    to: Pos(cur.line, token.end)
                };
                if (CodeMirror.attachContextInfo) {
                    // if context info is available, attach it
                    CodeMirror.attachContextInfo(data);
                }
                if (options && options.async) {
                    showHint(data);
                } else {
                    return data;
                }
            }

            CodeMirror.commands.autocomplete = function (cm) {
                CodeMirror.showHint(cm, templateHint, { async: false });
            }

            var mimeType = dnn.getVar('mimeType') || "text/html";
            cm = CodeMirror.fromTextArea($("textarea[id$='tbxEdit']")[0], {
                lineNumbers: true,
                matchBrackets: true,
                lineWrapping: true,
                mode: mimeType,
                autoCloseBrackets: true,
                extraKeys: {
                    "Ctrl-Space": "autocomplete",
                    "F11": function (cm) {
                        cm.setOption("fullScreen", !cm.getOption("fullScreen"));
                    },
                    "Esc": function (cm) {
                        if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
                    }
                }
            });

            cm.on("change", function () {
                cmchanged = true;
            });


        }
    }
</script>

<script type="text/javascript">
    function OnClientItemSelected(sender, args) {
        var filename = args.get_path();

        if (endsWith(filename, ".jpg") || endsWith(filename, ".png") || endsWith(filename, ".gif")) {
            $("#divImg").show();
            $("#divEditor").hide();
            $("#divImg img").attr("src", filename);
        } else {
            $("#divImg").hide();
            $("#divEditor").show();
            moduleService.openFile(filename);
        }
    }

    function OnTreeContextMenuItemClicked(oTreeMenu, args) {
        var menuItemValue = args.get_menuItem().get_value();
        var pathToItem = args.get_node().get_attributes().getAttribute('Path');
        if (menuItemValue == "newfile") {
            //var dirPath = oExplorer.get_currentDirectory();
            var filename = prompt("File Name", "newfile.cshtml");
            if (filename != "" && filename != null) {
                moduleService.newFile(pathToItem + '/' + filename);
            }
        }
    }
</script>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    prm.add_initializeRequest(InitRequestHandler);

    function BeginRequestHandler(sender, args) {

    }

    function EndRequestHandler(sender, args) {
       
    }

    function InitRequestHandler(s, e) {
       
    }
</script>


<script type="text/javascript">
    var SplitMode = false;
    var moduleService;

    $(document).ready(function () {
        var moduleScope = $('#<%=ScopeWrapper.ClientID %>'),
        self = moduleScope,
        sf = $.ServicesFramework(<%=ModuleId %>);
        moduleService = moduleScope;
        // Save button
        $("#<%= lkbSave.ClientID %>", moduleScope).click(function () {
            var filename = $("#<%= lFileName.ClientID %>", moduleScope).html();
            self.updateFile();
            return false;
        });
        // DataSource button
        $("#<%= hlDataSource.ClientID %>", moduleScope).click(function () {
            var DataSourceUrl = "<%= DataSourceUrl %>";
            var filename = $("#<%= lFileName.ClientID %>", moduleScope).html();
            if(filename != "")
                dnnModal.show(DataSourceUrl + "&template=" + escape(filename) , false, 550, 950, true);
             return false;
         });
        // Data Button
        $("#<%= lkbData.ClientID %>", moduleScope).click(function () {
            var PreviewUrl = "<%= PreviewUrl %>";
            var filename = $("#<%= lFileName.ClientID %>", moduleScope).html();
            if (filename != "")
                dnnModal.show(PreviewUrl + "&template=" + escape(filename) + "&mode=data", false, 550, 950, false);
            return false;
        });
        // Widget button
        $("#<%= hlWidget.ClientID %>", moduleScope).click(function () {
            //self.widgetFile();
            var PreviewUrl = "<%= PreviewUrl %>";
            var filename = $("#<%= lFileName.ClientID %>", moduleScope).html();
            if (filename != "")
                dnnModal.show(PreviewUrl + "&template=" + escape(filename) + "&mode=widget", false, 550, 950, false);
            return false;
        });
        // Preview button
        $("#<%= hlPreview.ClientID %>", moduleScope).click(function () {
            var PreviewUrl = "<%= PreviewUrl %>";
            var filename = $("#<%= lFileName.ClientID %>", moduleScope).html();
            if (filename != "")
                dnnModal.show(PreviewUrl + "&template=" + escape(filename), false, 550, 950, false);
            return false;
        });
        // CTRL-S Save
        document.addEventListener("keydown", function (e) {
            if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
                e.preventDefault();
                if (SplitMode) {
                    self.runFile();
                }
                else {
                    self.updateFile();
                }
            }
        }, false);
        // Auto save
        setInterval(function () {
            if (cmchanged) {
                self.updateFile();
            }
        }, 10 * 1000);
        // New file
        self.newFile = function (filename) {
            var postData = { Filename: filename };
            var action = "NewFile";
            $("#<%= lFileName.ClientID %>", moduleScope).html("");
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cm.setOption('mode', mimeType(filename));
                    cm.setValue(data);
                    cmchanged = false;
                    notify('File ' + filename + ' created !', 'succes');
                    $("#<%= lFileName.ClientID %>", moduleScope).html(filename);
                    document.cookie = "lkbRefresh=" + filename + ";path=/";
                    var oExplorer = $find("<%= dfeTree.ClientID %>");
                    oExplorer.refresh();
                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
        // Open file
        self.openFile = function (sourceFile) {
            if (sourceFile == undefined) {
                sourceFile = $("#<%= lFileName.ClientID %>", moduleScope).html();
            }
            $("#<%= lFileName.ClientID %>", moduleScope).html("");
            var postData = { Filename: sourceFile };
            var action = "OpenFile";
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cm.setOption('mode', mimeType(sourceFile));
                    cm.setValue(data);
                    cmchanged = false;
                    notify('File ' + sourceFile + ' open !', 'information');
                    $("#<%= lFileName.ClientID %>", moduleScope).html(sourceFile);
                    document.cookie = "lkbRefresh=" + sourceFile+";path=/";
                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
        // winget - NOT USED
        self.widgetFile = function (sourceFile) {
            if (sourceFile == undefined) {
                sourceFile = $("#<%= lFileName.ClientID %>", moduleScope).html();
            }
            var postData = { Filename: sourceFile };
            var action = "WidgetFile";
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cmchanged = false;
                    notify('File ' + sourceFile + ' open !', 'information');
                    //displayMessage('File '+ sourceFile +' open !', 'information');
                    $("#divEditor").hide();

                    $("#divRun").html(data);
                    $("#divRun").show();

                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
        // Save file
        self.updateFile = function () {
            var sourceFile = $("#<%= lFileName.ClientID %>", moduleScope).html();
            if (sourceFile == "") {
                otify('No file to save', 'error');
                return;
            }
            var postData = { Filename: sourceFile, Content: cm.getValue() };
            var action = "UpdateFile";
            notify('File save ...', 'information');
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cmchanged = false;
                    notify('File saved !', 'success');
                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
        // run file - NOT USED
        self.runFile = function () {
            var postData = { Filename: $("#<%= lFileName.ClientID %>", moduleScope).html(), Content: cm.getValue() };
            var action = "RunFile";
            notify('File save ...', 'information');
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cmchanged = false;
                    if (!SplitMode) {
                        $("#divEditor").hide();
                    }
                    $('<iframe id="someId"/>').appendTo('#divRun');
                    $('#someId').contents().find('body').append(data);
                    $("#divRun").show();
                    notify('File saved & Run !', 'success');
                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
        // Data file - NOT USED
        self.dataFile = function () {
            var postData = { Filename: $("#<%= lFileName.ClientID %>", moduleScope).html(), Content: cm.getValue() };
            var action = "DataFile";
            notify('File save ...', 'information');
            $.ajax({
                type: "POST",
                url: sf.getServiceRoot('OpenBlocks') + "TemplateEditor/" + action,
                data: postData,
                beforeSend: sf.setModuleHeaders
            }).done(function (data) {
                if (data !== undefined && data != null) {
                    cmchanged = false;
                    $("#divEditor").hide();
                    $("#divRun").html(data);
                    $("#divRun").show();

                    notify('File saved & show data !', 'success');
                }
            }).fail(function (xhr, result, status) {
                notify("Uh-oh, something broke : " + status, 'error');
            });
        };
    });
</script>
<script type="text/javascript">

    function displayMessage(message, cssclass) {
        var messageNode = $("<div/>").addClass('dnnFormMessage ' + cssclass).text(message);
        $('#<%=ScopeWrapper.ClientID %>').prepend(messageNode);
        messageNode.fadeOut(3000, 'easeInExpo', function () { messageNode.remove(); });
    };

    function notify(message, type, timeout) {
        // default values
        message = typeof message !== 'undefined' ? message : 'Hello!';
        type = typeof type !== 'undefined' ? type : 'success';
        timeout = typeof timeout !== 'undefined' ? timeout : 3000;

        // append markup if it doesn't already exist
        if ($('#notification').length < 1) {
            markup = '<div id="notification" class="information"><span>Hello!</span><a class="close" href="#">x</a></div>';
            $('body').append(markup);
        }
        // elements
        $notification = $('#notification');
        $notificationSpan = $('#notification span');
        $notificationClose = $('#notification a.close');
        // set the message
        $notificationSpan.text(message);
        // setup click event
        $notificationClose.click(function (e) {
            e.preventDefault();
            $notification.css('top', '-50px');
        });
        // for ie6, scroll to the top first
        if ($.browser.msie && $.browser.version < 7) {
            $('html').scrollTop(0);
        }
        // hide old notification, then show the new notification
        $notification.css('top', '-50px').stop().removeClass().addClass(type).animate({
            top: 0
        }, 500, function () {
            $notification.delay(timeout).animate({
                top: '-50px'
            }, 500);
        });
    }

    function mimeType(filename) {
        var mimeType = "text/html";
        if (endsWith(filename, ".vb")) {
            mimeType = "text/x-vb";
        }
        else if (endsWith(filename, ".cs")) {
            mimeType = "text/x-csharp"
        }
        else if (endsWith(filename, ".css")) {
            mimeType = "text/css";
        }
        else if (endsWith(filename, ".js")) {
            mimeType = "text/javascript";
        }
        else if (endsWith(filename, ".xml") || endsWith(filename, ".xslt")) {
            mimeType = "application/xml";
        }
        else if (endsWith(filename, ".sql") || endsWith(filename, ".sqldataprovider")) {
            mimeType = "text/x-sql";
        }
        return mimeType;
    }
    function endsWith(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }
    $(document).ready(function () {
        updateContainer();
        $(window).resize(function () {
            updateContainer();
        });
    });
    function updateContainer() {
        var containerHeight = $(window).height();

        $('.editorContainer').height(containerHeight - $('.editorContainer').offset().top -110 );
        //$('.editorContainer').height(containerHeight - 250);
        $('#templateEditor .CodeMirror').height($('.editorContainer .col1').height() - 18);
        ResizeExplorer();
        cm.refresh();
    }
    function OnClientLoad(sender, args) {
       
        setTimeout(function () { ResizeExplorer(); }, 0);
    }
    var resized = false;
    function ResizeExplorer() { // The name of the div the RadFileExplorer is in: 
        var height = $(".col1").height() - 20; 
        var width = ($(".col1").width()) -5;
        // The name of the RadFileExplorer goes here 
        var explorer = $find("<%= dfeTree.ClientID %>");
        var domSplitter = $("div[ID$='splitter']").attr("id");  
        if (explorer) { 
            resized = true; 
            //var grid = explorer.get_grid(); 
            var div = explorer.get_element(); 
            var toolbar = explorer.get_toolbar();  
            var splitter = $find(domSplitter); 
            //resize explorer container div 
            //div.style.height = height + "px"; 
            //div.style.width = width + "px"; 
            //div.style.border = "0px";  
            //resize the splitter  
            splitter.resize(width, height - toolbar.get_element().offsetHeight);   
            //resize the grid height  
            //grid.get_element().style.height = (height - toolbar.get_element().offsetHeight) + "px"; grid.repaint();
        }
    }
</script>
