<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="Production.aspx.cs" Inherits="_Production" %>
<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <div id="fadeInOut" style="margin-top:60px">
        <div class="production1_3_col" >
            <div class="mover">
                <h1 id="About" runat="server"></h1>
                <div class="production1_title_left">
                <div style="height:6px"></div>
                <img alt="" id="selectedPic" class="picBig" src="" />
                </div>
                <div class="space14Prod">
                </div>
                <div style="height:140px"></div>
                <p id="selectedText" class="textAbout">
                </p>
            </div>
        </div>
        <div id="main" class="production_3_col production_3_col_middle" runat="server">
            <h1 id="LatestRecords" runat="server">
                </h1>
            <div class="cleaner">
            </div>
            <div id="proGallery" class="production_gallery" runat="server">
            </div>
            <div class="production_more_2">
                <a id="HearMore" runat="server" href="AllArtists.aspx" class="production_more_2"></a></div>
        </div>
        <div class="production_3_col">
            <h1 id="LetsHearIt" runat="server" style="margin-top:10px">
                </h1>
            <div class="mover">
                <div id="music" runat="server">
                </div>
            </div>
        </div>
        <div class="cleaner">
        </div>
    </div>
</asp:Content>
