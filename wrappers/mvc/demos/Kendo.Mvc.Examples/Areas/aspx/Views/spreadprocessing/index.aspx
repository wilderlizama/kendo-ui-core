﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="example">
    <div class="demo-section k-header custom-width">
        <% using (Html.BeginForm("Download_Document", "SpreadProcessing"))
        { %>

            <div class="document-options-wrapper">
                <div class="column">
                    <%= Html.Kendo().Upload()
                        .Name("customDocument")
                        .Multiple(false)
                        .ShowFileList(false)
                        .Events(events => events.Select("onSelect"))
                    %>

                    <%= Html.Kendo().Button()
                        .Name("uploadFileBtn")
                        .Content("Load Custom Document")
                        .HtmlAttributes(new { type = "button" })
                        .Events(events => events.Click("uploadFile"))
                    %>
                    <div class="imageContainer  custom-doc">
                        <img src="/Content/web/spreadprocessing/CustomDocument_file.png" />
                    </div>
                </div>
                <div class="column or-elm">- OR -</div>
                <div class="column">
                    <%= Html.Kendo().Button()
                        .Name("loadTempDocBtn")
                        .Content("Load Template Document")
                        .HtmlAttributes(new { type = "button" })
                        .Events(events => events.Click("loadTemplate"))
                    %>
                    <div class="imageContainer">
                        <img src="/Content/web/spreadprocessing/SampleDocumentImage.png" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="file-options-wrapper">
                <div class="column float-right">
                    <%= Html.Label("fileExtension", "Convert to: ") %>
                    <%= Html.Kendo().ComboBox()
                        .Name("convertTo")
                        .Filter("contains")
                        .Suggest(true)
                        .SelectedIndex(0)
                        .Items(items =>
                            {
                                items.Add().Text("XLSX").Value("xlsx");
                                items.Add().Text("CSV").Value("csv");
                                items.Add().Text("TXT").Value("txt");
                                items.Add().Text("PDF").Value("pdf");
                            }
                        )
                    %>

                </div>
                <div class="column"><span>File: </span><span id="fileName"></span></div>
            </div>

            <hr class="clear" />
            <input type="submit" class="k-button k-primary wide-btn" value="Download" />
        <%}%>

    </div>
</div>

<script>
    function loadTemplate(ev) {
        clearHighlights();
        $("#fileName").text("SampleDocument.xlsx");
        ev.sender.element.siblings(".imageContainer").addClass("highlighted");
    }

    function uploadFile(ev) {
        $("#customDocument").click();
    }

    function onSelect(ev) {
        var file = ev.files[0];
        clearHighlights();

        if (!/.xlsx|.csv|.txt/.test(file.extension)) {
            alert("Only documents with *.xlsx, *.csv or *.txt extensions are accepted!");
            ev.preventDefault();
        } else {
            var fileExt = file.extension.substr(1);
            $(".imageContainer.custom-doc img").attr("src", "/Content/web/spreadprocessing/" + fileExt + "_file.png");
            $(".imageContainer.custom-doc").addClass("highlighted");
            $("#fileName").text(file.name);
        }
    }

    function clearHighlights() {
        $("#fileName").text("");
        $(".imageContainer").removeClass("highlighted");
        $(".imageContainer.custom-doc img").attr("src", "/Content/web/spreadprocessing/CustomDocument_file.png");

        // clear uploaded files
        var upload = $("#customDocument").data("kendoUpload");
        var fileInput = upload.wrapper.find("input").first();
        if (!fileInput.is("#customDocument")) {
            fileInput.remove();
        }
    }
</script>

<style>
    .demo-section.custom-width {
        max-width: 472px;
    }

    .column {
        vertical-align: middle;
        display: inline-block;
    }

    .document-options-wrapper button {
        margin-bottom: 10px;
    }

    .document-options-wrapper .column:not(:first-child) {
        margin-left: 10px;
    }

    .document-options-wrapper .column.or-elm {
        margin-top: 35px;
    }

    .file-options-wrapper {
        line-height: 30px;
    }

    .imageContainer {
        border: 1px solid #c9c9c9;
        height: 171px;
        width: 200px;
        position: relative;
    }

        .imageContainer.highlighted {
            border-color: #36B4CB;
        }

        .imageContainer img {
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            max-height: 100%;
            max-width: 100%;
            width: auto;
            height: auto;
        }

    .column #fileName {
        display: inline-block;
        overflow: hidden;
        text-overflow: ellipsis;
        vertical-align: middle;
        white-space: nowrap;
        width: 179px;
    }

    input.wide-btn {
        width: 100%;
    }

    .k-upload {
        display: none;
    }

    .float-right {
        float: right;
    }

    .clear {
        clear: both;
    }

    hr {
        margin: 15px 0;
        border-width: 0 0 1px 0;
        border-color: #c9c9c9;
        border-style: solid;
    }
</style>
</asp:Content>
