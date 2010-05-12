<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Studio.aspx.cs" Inherits="Admin_Studio" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="studioHidden" runat="server" type="hidden" value="" />
        <input id="studioHiddenType" runat="server" type="hidden" value="" />
        <div id="mainStudio" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="studioSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studioSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addStudioPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="studioPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="studioPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="studioPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Picture Type:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="studioPicTypeSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studioPicTypeSelector_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="studioPicPlaceSelector" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Color Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="studioColorPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="studioColorPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="morePic" visible="false" runat="server">
                                    <td align="right">
                                        Black And White Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="studioBWPicUploadPic" alt="" src="" runat="server" visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="studioBWPicUpload" type="file" runat="server" visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addStudioPicButton" Text="Add" runat="server" OnClick="addStudioPicButton_Click" />
                <asp:Button ID="disableStudioPic" Text="Disable" runat="server" OnClick="disableStudioPic_Click" />
                <asp:Button ID="enableStudioPic" Text="Enable" runat="server" OnClick="enableStudioPic_Click" />
                <asp:Button ID="cancelAddStudioPicButton" Text="Cancel" runat="server" OnClick="cancelAddStudioPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePic" runat="server" class="mailNo" visible="false">
            <input id="studioHiddenRe" type="hidden" runat="server" value="" />
            <input id="studioHiddenUp" type="hidden" runat="server" value="" />
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
                <asp:Button ID="removeStudioPicButton" Text="Remove" runat="server" OnClick="removeStudioPicButton_Click" />
                <asp:Button ID="updateStudioPicButton" Text="Update" runat="server" OnClick="updateStudioPicButton_Click" />
                <asp:Button ID="cancelStudioPicButton" Text="Cancel" runat="server" OnClick="cancelStudioPicButton_Click" />
            </div>
        </div>
        <div id="aboutStudio" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="studioAboutUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="studioAboutLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <h3>
                                            Technical</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutTeStudioTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutTeStudioTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h3>
                                            About Studio</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutStudioTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutStudioTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateStudioAboutButton" Text="Update" runat="server" OnClick="updateStudioAboutButton_Click" />
                <asp:Button ID="cancelStudioAboutButton" Text="Cancel" runat="server" OnClick="cancelStudioAboutButton_Click" />
            </div>
        </div>
        <div id="studioNotify" runat="server" class="mailNo" visible="false">
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
