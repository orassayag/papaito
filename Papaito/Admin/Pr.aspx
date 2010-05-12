<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Pr.aspx.cs" Inherits="Admin_Pr" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="prHidden" runat="server" type="hidden" value="" />
        <div id="mainPr" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="prSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="prSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addPrPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="prPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="prPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="prPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                                    <img id="prPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="prPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addPrPicButton" Text="Add" runat="server" OnClick="addPrPicButton_Click" />
                <asp:Button ID="disablePrPicButton" Text="Disable" runat="server" OnClick="disablePrPicButton_Click" />
                <asp:Button ID="enablePrPicButton" Text="Enable" runat="server" OnClick="enablePrPicButton_Click" />
                <asp:Button ID="cancelAddPrPicButton" Text="Cancel" runat="server" OnClick="cancelAddPrPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePic" runat="server" class="mailNo" visible="false">
            <input id="prHiddenRe" type="hidden" runat="server" value="" />
            <input id="prHiddenUp" type="hidden" runat="server" value="" />
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
                <asp:Button ID="removePrPicButton" Text="Remove" runat="server" OnClick="removePrPicButton_Click" />
                <asp:Button ID="updatePrPicButton" Text="Update" runat="server" OnClick="updatePrPicButton_Click" />
                <asp:Button ID="cancelPrPicButton" Text="Cancel" runat="server" OnClick="cancelPrPicButton_Click" />
            </div>
        </div>
        <div id="aboutPr" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="aboutPrUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="aboutPrLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
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
                                        <asp:TextBox ID="aboutPrTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutPrTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updatePrAboutButton" Text="Update" runat="server" OnClick="updatePrAboutButton_Click" />
                <asp:Button ID="cancelPrAboutButton" Text="Cancel" runat="server" OnClick="cancelPrAboutButton_Click" />
            </div>
        </div>
        <div id="prNotify" runat="server" class="mailNo" visible="false">
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
