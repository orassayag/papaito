<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" 
    CodeFile="Complex.aspx.cs" Inherits="Admin_Complex" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="complexHidden" runat="server" type="hidden" value="" />
        <input id="complexHiddenType" runat="server" type="hidden" value="" />
        <div id="mainComplex" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="complexSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="complexSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addComplexPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="complexPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="complexPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="complexPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                        <asp:DropDownList ID="complexPicTypeSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="complexPicTypeSelector_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="complexPicPlaceSelector" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <h3>
                                            Title Of The Room</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="complexTitleRoomHe" runat="server" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="complexTitleRoomEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <h3>
                                            About The Room</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="complexAboutRoomHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="complexAboutRoomEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
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
                                                    <img id="complexColorPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="complexColorPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="morePic" runat="server">
                                    <td align="right">
                                        Black And White Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="complexBWPicUploadPic" visible="false" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="complexBWPicUpload" visible="false"  type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addComplexPicButton" Text="Add" runat="server" OnClick="addComplexPicButton_Click" />
                <asp:Button ID="disableComplexPic" Text="Disable" runat="server" OnClick="disableComplexPic_Click" />
                <asp:Button ID="enableComplexPic" Text="Enable" runat="server" OnClick="enableComplexPic_Click" />
                <asp:Button ID="cancelAddComplexPicButton" Text="Cancel" runat="server" OnClick="cancelAddComplexPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePic" runat="server" class="mailNo" visible="false">
            <input id="complexHiddenRe" type="hidden" runat="server" value="" />
            <input id="complexHiddenUp" type="hidden" runat="server" value="" />
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
                <asp:Button ID="removeComplexPicButton" Text="Remove" runat="server" OnClick="removeComplexPicButton_Click" />
                <asp:Button ID="updateComplexPicButton" Text="Update" runat="server" OnClick="updateComplexPicButton_Click" />
                <asp:Button ID="cancelComplexPicButton" Text="Cancel" runat="server" OnClick="cancelComplexPicButton_Click" />
            </div>
        </div>
        <div id="aboutComplex" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="complexAboutUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="complexAboutLastUpdate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="bottom">
                            <h3>
                                About Complex</h3>
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
                                        <asp:TextBox ID="aboutComplexTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="aboutComplexTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateComplexAboutButton" Text="Update" runat="server" OnClick="updateComplexAboutButton_Click" />
                <asp:Button ID="cancelComplexAboutButton" Text="Cancel" runat="server" OnClick="cancelComplexAboutButton_Click" />
            </div>
        </div>
        <div id="complexNotify" runat="server" class="mailNo" visible="false">
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
