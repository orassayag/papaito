<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Design.aspx.cs" Inherits="Admin_Design" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="designHidden" runat="server" type="hidden" value="" />
        <div id="mainDesign" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="designSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="designSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addDesignPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="designPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="designPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="designPicStatusLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="designPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="designPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addDesignPicButton" Text="Add" runat="server" OnClick="addDesignPicButton_Click" />
                <asp:Button ID="disableDesignPicButton" Text="Disable" runat="server" OnClick="disableDesignPicButton_Click" />
                <asp:Button ID="enableDesignPicButton" Text="Enable" runat="server" OnClick="enableDesignPicButton_Click" />
                <asp:Button ID="cancelAddDesignPicButton" Text="Cancel" runat="server" OnClick="cancelAddDesignPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePic" runat="server" class="mailNo" visible="false">
            <input id="designHiddenRe" type="hidden" runat="server" value="" />
            <input id="designHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Picture:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdatePicSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeDesignPicButton" Text="Remove" runat="server" OnClick="removeDesignPicButton_Click" />
                <asp:Button ID="updateDesignPicButton" Text="Update" runat="server" OnClick="updateDesignPicButton_Click" />
                <asp:Button ID="cancelDesignPicButton" Text="Cancel" runat="server" OnClick="cancelDesignPicButton_Click" />
            </div>
        </div>
        <div id="aboutDesign" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="aboutDesignUpdateInfo" runat="server">
                        <td>
                            Last Update:
                            <asp:Label ID="aboutDesignLastUpdateLabel" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="bottom">
                            <h3>
                                About</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutDesignTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutDesignTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateDesignAboutButton" Text="Update" runat="server" OnClick="updateDesignAboutButton_Click" />
                <asp:Button ID="cancelDesignAboutButton" Text="Cancel" runat="server" OnClick="cancelDesignAboutButton_Click" />
            </div>
        </div>
        <div id="designNotify" runat="server" class="mailNo" visible="false">
            <asp:Label ID="notifyLabel" CssClass="notify" Text="" runat="server"></asp:Label><br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="okBut" runat="server" Text="Ok" OnClick="okBut_Click" />
                    </td>
                    <td>
                        <asp:Button ID="cancelBut" runat="server" Text="Cancel" OnClick="cancelBut_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
