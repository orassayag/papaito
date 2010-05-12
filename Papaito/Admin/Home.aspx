<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Admin_Home" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="homeHidden" runat="server" type="hidden" value="" />
        <div id="mainHome" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="homeSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="homeSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addHeaderPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="headerPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="headerPicLastUpdateLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="headerPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="headerPicPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="headerPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="headerPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addHeaderPicButton" Text="Add" runat="server" OnClick="addHeaderPicButton_Click" />
                <asp:Button ID="diableHeaderPicButton" Text="Disable" runat="server" OnClick="diableHeaderPicButton_Click" />
                <asp:Button ID="enableHeaderPicButton" Text="Enable" runat="server" OnClick="enableHeaderPicButton_Click" />
                <asp:Button ID="cancelAddHeaderPicButton" Text="Cancel" runat="server" OnClick="cancelAddHeaderPicButton_Click" />
            </div>
        </div>
        <div id="removeUpdateHeaderPic" runat="server" class="mailNo" visible="false">
            <input id="headerPicHiddenRe" type="hidden" runat="server" value="" />
            <input id="headerPicHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Picture:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateHeaderPicSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateHeaderPicButton" Text="Update" runat="server" OnClick="updateHeaderPicButton_Click" />
                <asp:Button ID="removeHeaderPicButton" Text="Remove" runat="server" OnClick="removeHeaderPicButton_Click" />
                <asp:Button ID="cancelHeaderPicButton" Text="Cancel" runat="server" OnClick="cancelHeaderPicButton_Click" />
            </div>
        </div>
        <div id="addLastRecordsPic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="lastRecordsPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        Last Update:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lastRecordsPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Status:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lastRecordsPicStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="lastRecordsPicPlaceSelector" runat="server">
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
                                                    <img id="lastRecordsColorPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="lastRecordsColorPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Black And White Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="lastRecordsBWPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="lastRecordsBWPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addLastRecordsButton" Text="Add" runat="server" OnClick="addLastRecordsButton_Click" />
                <asp:Button ID="disableLastRecordsButton" Text="Disable" runat="server" OnClick="disableLastRecordsButton_Click" />
                <asp:Button ID="enableLastRecordsButton" Text="Enable" runat="server" OnClick="enableLastRecordsButton_Click" />
                <asp:Button ID="cancelAddLastRecordsButton" Text="Cancel" runat="server" OnClick="cancelAddLastRecordsButton_Click" />
            </div>
        </div>
        <div id="removeUpdateLastRecordsPic" runat="server" class="mailNo" visible="false">
            <input id="lastRecordsHiddenRe" type="hidden" runat="server" value="" />
            <input id="lastRecordsHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Select Picture:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="removeUpdatelastRecordsSelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateLastRecordsButton" Text="Update" runat="server" OnClick="updateLastRecordsButton_Click" />
                <asp:Button ID="removeLastRecordsButton" Text="Remove" runat="server" OnClick="removeLastRecordsButton_Click" />
                <asp:Button ID="cancelLastRecordsButton" Text="Cancel" runat="server" OnClick="cancelLastRecordsButton_Click" />
            </div>
        </div>
        <div id="addWillComePic" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="willComeUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="willComeLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="willComeStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="willComePicPlaceSelector" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Color Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr align="left">
                                                <td>
                                                    <img id="willComeColorPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <input id="willComeColorPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Black And White Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="willComeBWPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="willComeBWPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addWillComePicButton" Text="Add" runat="server" OnClick="addWillComePicButton_Click" />
                <asp:Button ID="disableWillComePicButton" Text="Disable" runat="server" OnClick="disableWillComePicButton_Click" />
                <asp:Button ID="enableWillComePicButton" Text="Enable" runat="server" OnClick="enableWillComePicButton_Click" />
                <asp:Button ID="cancelAddWillComePicButton" Text="Cancel" runat="server" OnClick="cancelAddWillComePicButton_Click" />
            </div>
        </div>
        <div id="removeUpdateWillComePic" runat="server" class="mailNo" visible="false">
            <input id="willComePicHiddenRe" type="hidden" runat="server" value="" />
            <input id="willComePicHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Select Picture:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="removeUpdateWillComePicSelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateWillComePicButton" Text="Update" runat="server" OnClick="updateWillComePicButton_Click" />
                <asp:Button ID="removeWillComePicButton" Text="Remove" runat="server" OnClick="removeWillComePicButton_Click" />
                <asp:Button ID="cancelWillComePicButton" Text="Cancel" runat="server" OnClick="cancelWillComePicButton_Click" />
            </div>
        </div>
        <div id="addLastNews" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="lastNewsUpdateInfo" runat="server">
                        <td>
                            Last Update:
                            <asp:Label ID="lastNewsLastUpdateLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Last News Place:
                            <asp:TextBox ID="lastNewsPlace" runat="server" Width="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3>
                                Last News Text</h3>
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
                                        <asp:TextBox ID="lastNewsTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="lastNewsTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addLastNewsButton" Text="Add" runat="server" OnClick="addLastNewsButton_Click" />
                <asp:Button ID="cancelAddLastNewsButton" Text="Cancel" runat="server" OnClick="cancelAddLastNewsButton_Click" />
            </div>
        </div>
        <div id="removeUpdateLastNews" runat="server" class="mailNo" visible="false">
            <input id="lastNewsHiddenRe" type="hidden" runat="server" value="" />
            <input id="lastNewsHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Select News:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="removeUpdateLastNewsSelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateLastNewsButton" Text="Update" runat="server" OnClick="updateLastNewsButton_Click" />
                <asp:Button ID="removeLastNewsButton" Text="Remove" runat="server" OnClick="removeLastNewsButton_Click" />
                <asp:Button ID="cancelLastNewsButton" Text="Cancel" runat="server" OnClick="cancelLastNewsButton_Click" />
            </div>
        </div>
        <div id="addContactAndAbout" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="contactAndAboutUpdateInfo" runat="server">
                        <td>
                            Last Update:
                            <asp:Label ID="homeContactAndAboutLastUpdateLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3>
                                Contact</h3>
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
                                        <asp:TextBox ID="homeContactTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="homeContactTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                                        <asp:TextBox ID="homeAboutTextHe" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="homeAboutTextEn" TextMode="MultiLine" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addContactAndAboutButton" Text="Add" runat="server" OnClick="addContactAndAboutButton_Click" />
                <asp:Button ID="cancelContactAndAboutButton" Text="Cancel" runat="server" OnClick="cancelContactAndAboutButton_Click" />
            </div>
        </div>
        <div id="homeNotify" runat="server" class="mailNo" visible="false">
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
