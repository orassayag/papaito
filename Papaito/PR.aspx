<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="PR.aspx.cs" Inherits="_PR" %>

<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">
    <div class="prClass">
        <div class="classLeft">
            <div id="prImage">
            <img style="width:400px; height:400px;" id="PrPic" runat="server" alt=""/>
            </div>
        </div>
        <div class="classRight">
            <div class="space11">
            </div>
            <h1 id="About" runat="server">
                </h1>
            <div class="space4">
            </div>
            <p id="PrText" runat="server">
                </p>
            <div class="space5">
            </div>
        </div>
    </div>
</asp:Content>
