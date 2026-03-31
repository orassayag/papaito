<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="ContentAndAbout.aspx.cs" Inherits="_ContentAndAbout" %>
<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">

    <div class="contactClass" style="margin-top:60px">
    <div class="contactSpace"></div>
        <div class="contactClassLeft">
            <ul class="contact">
                <li>
                <img id="papaMapa" alt="" src="OrImage/papaMapa.bmp" />
                </li>
            </ul>
            <p class="space16">
            </p>
        </div>
        <div class="contactClassRight">
            <h1 id="About" runat="server">
                </h1>
            <p id="textAbout" runat="server">
            </p>
            <h1 id="ContactUs" runat="server">
                </h1>
            <p id="MainContact" runat="server">
            </p>
        </div>
    </div>
</asp:Content>
