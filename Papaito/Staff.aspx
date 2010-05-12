<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="Staff.aspx.cs" Inherits="_Staff" %>
    <%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">
    <div class="fadeInOutSec">
        
        <div id="staff4eSec" class="htmltooltip" runat="server">
        <h1 id="DuduTitle" runat="server" class="hebrew"></h1>
        <p id="DuduText" runat="server"></p>
        <br />
        <a id="closeBut4" runat="server" href="#fadeInOut" class="showDetailsR"></a>
        </div>
        
        <div id="staff3eSec" class="htmltooltip" runat="server">
        <h1 id="PerryTitle" runat="server" class="hebrew"></h1>
        <p id="PerryText" runat="server"></p>
        <br />
        <a id="closeBut3" runat="server" href="#fadeInOut" class="showDetailsR"></a>
        </div>
        
        <div id="staff2eSec" class="htmltooltip" runat="server">
        <h1 id="NapoTitle" runat="server" class="hebrew"></h1>
        <p id="NapoText" runat="server"></p>
        <br />
        <a id="closeBut2" runat="server" href="#fadeInOut" class="showDetailsR"></a>
        </div>
        
        <div id="staff1eSec" class="htmltooltip" runat="server">
        <h1 id="ItayTitle" runat="server" class="hebrew"></h1>
        <p id="ItayText" runat="server"></p>
        <br />
        <a id="closeBut1" runat="server" href="#fadeInOut" class="showDetailsR"></a>
        </div>                  
        
        <div class="spacer"></div>
        <div id="outerContainer">
            <div id="dudu" class="staffSec dudu" accesskey="htmltooltip">
            </div>
            <div id="peri" class="staffSec peri" accesskey="htmltooltip">
            </div>
            <div id="napo" class="staffSec napo" accesskey="htmltooltip">
            </div>
            <div id="itay" class="staffSec itay" accesskey="htmltooltip">
            </div>
        </div>
    </div>
</asp:Content>
