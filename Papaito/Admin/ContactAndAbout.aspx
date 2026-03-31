<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ContactAndAbout.aspx.cs" Inherits="Admin_ContactAndAbout" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="contactAndAboutHidden" runat="server" type="hidden" value="" />
        <div id="mainContactAndAbout" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="contactAndAboutSelector" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="contactAndAboutSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addContactAndAboutPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="contactAndAboutPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="contactAndAboutPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="contactAndAboutPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                                    <img id="contactAndAboutPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="contactAndAboutPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addContactAndAboutPicButton" Text="Add" runat="server" OnClick="addContactAndAboutPicButton_Click" />
                <asp:Button ID="disableContactAndAboutPicButton" Text="Disable" runat="server" OnClick="disableContactAndAboutPicButton_Click" />
                <asp:Button ID="enableContactAndAboutPicButton" Text="Enable" runat="server" OnClick="enableContactAndAboutPicButton_Click" />
                <asp:Button ID="cancelAddContactAndAboutPicButton" Text="Cancel" runat="server" OnClick="cancelAddContactAndAboutPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePic" runat="server" class="mailNo" visible="false">
            <input id="contactAndAboutHiddenRe" type="hidden" runat="server" value="" />
            <input id="contactAndAboutHiddenUp" type="hidden" runat="server" value="" />
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
                <asp:Button ID="removeContactAndAboutPicButton" Text="Remove" runat="server" OnClick="removeContactAndAboutPicButton_Click" />
                <asp:Button ID="updateContactAndAboutPicButton" Text="Update" runat="server" OnClick="updateContactAndAboutPicButton_Click" />
                <asp:Button ID="cancelContactAndAboutPicButton" Text="Cancel" runat="server" OnClick="cancelContactAndAboutPicButton_Click" />
            </div>
        </div>
        <div id="aboutContactAndAbout" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="aboutContactAndAboutUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="aboutContactAndAboutLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <h3>
                                            About</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutContactAndAboutTextHe" TextMode="MultiLine" runat="server"
                                            Height="67px" Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutContactAndAboutTextEn" TextMode="MultiLine" runat="server"
                                            Height="67px" Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <h3>
                                            Contact</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="contactContactAndAboutTextHe" TextMode="MultiLine" runat="server"
                                            Height="67px" Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="contactContactAndAboutTextEn" TextMode="MultiLine" runat="server"
                                            Height="67px" Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateContactAndAboutAboutButton" Text="Update" runat="server" OnClick="updateContactAndAboutAboutButton_Click" />
                <asp:Button ID="cancelContactAndAboutAboutButton" Text="Cancel" runat="server" OnClick="cancelContactAndAboutAboutButton_Click" />
            </div>
        </div>
        <div id="contactAndAboutNotify" runat="server" class="mailNo" visible="false">
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
