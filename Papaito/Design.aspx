<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="Design.aspx.cs" Inherits="_Design" %>

<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">
    <div class="designClass">
        <div class="classLeft">
            <p class="space4">
            </p>
            <div class="container_12" id="wrapper">
                <div style="margin-top:20px" class="grid_8" id="content">
                    <div class="grid_6 prefix_1 suffix_1" id="gallery">
                        <div id="pictures">
                            <img class=DesignPicsSize id=DesignImg1 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg2 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg3 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg4 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg5 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg6 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg7 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg8 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg9 runat="server" alt="" />
                            <img class=DesignPicsSize id=DesignImg10 runat="server" alt="" />
                            <div style="height:380px">
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <div class="grid_3 alpha" id="prev">
                                            <a id="Back" runat="server" href="#previous"></a>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="grid_3 omega" id="next">
                                            <a id="Next" runat="server" href="#next"></a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="classRight">
            <div class="space11">
            </div>
            <h1 id="About" runat="server" class="Papaito">
                </h1>
            <div class="space4">
            </div>
            <p id="DesignText" runat="server">
                </p>
            <div class="space5">
            </div>
        </div>
    </div>
</asp:Content>
