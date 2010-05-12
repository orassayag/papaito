<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="Production.aspx.cs" Inherits="Admin_Production" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="productionHidden" runat="server" type="hidden" value="" />
        <div id="mainProduction" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="productionSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="productionSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addProduction" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="productionUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="productionLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="productuionStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Artist Name Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="productionArtistsNameHe" CssClass="textHeb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Artist Name English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="productionArtistsNameEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        About Artist Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="productionAboutArtistsHe" CssClass="textHeb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        About Artist English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="productionAboutArtistsEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Production Place:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="productionPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Main Picture:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="productionMainPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="productionMainPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
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
                                                    <img id="productionColorPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="productionColorPicUpload" type="file" runat="server" />
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
                                                    <img id="productionBWPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="productionBWPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addProductionButton" Text="Add" runat="server" OnClick="addProductionButton_Click" />
                <asp:Button ID="diableProductionButton" Text="Disable" runat="server" OnClick="diableProductionButton_Click" />
                <asp:Button ID="enableProductionButton" Text="Enable" runat="server" OnClick="enableProductionButton_Click" />
                <asp:Button ID="cancelAddProductionButton" Text="Cancel" runat="server" OnClick="cancelAddProductionButton_Click" />
            </div>
        </div>
        <div id="removeUpdateProduction" runat="server" class="mailNo" visible="false">
            <input id="productionHiddenRe" type="hidden" runat="server" value="" />
            <input id="productionHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Production:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateProductionSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeProductionButton" Text="Remove" runat="server" OnClick="removeProductionButton_Click" />
                <asp:Button ID="updateProductionButton" Text="Update" runat="server" OnClick="updateProductionButton_Click" />
                <asp:Button ID="cancelProductionButton" Text="Cancel" runat="server" OnClick="cancelProductionButton_Click" />
            </div>
        </div>
        <div id="addSong" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="songUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="songLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="songStatusLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="left">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    Add To Production:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="addSongToProductionSelector" runat="server" Width="200px" Height="200px">
                                                    </asp:ListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <h3>
                                            Song Name</h3>
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
                                                    <asp:TextBox ID="songNameHe" runat="server" CssClass="textHeb" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    English:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="songNameEn" runat="server" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    YouTube Adrress:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="songYoutubeAddress" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Song Place:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="songPlace" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addSongButton" Text="Add" runat="server" OnClick="addSongButton_Click" />
                <asp:Button ID="disableSongButton" Text="Disable" runat="server" OnClick="disableSongButton_Click" />
                <asp:Button ID="enableSongButton" Text="Enable" runat="server" OnClick="enableSongButton_Click" />
                <asp:Button ID="cancelAddSongButton" Text="Cancel" runat="server" OnClick="cancelAddSongButton_Click" />
            </div>
        </div>
        <div id="removeUpdateSong" runat="server" class="mailNo" visible="false">
            <input id="songHiddenRe" type="hidden" runat="server" value="" />
            <input id="songHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Song:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateSongSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeSongButton" Text="Remove" runat="server" OnClick="removeSongButton_Click" />
                <asp:Button ID="updateSongButton" Text="Update" runat="server" OnClick="updateSongButton_Click" />
                <asp:Button ID="cancelSongButton" Text="Cancel" runat="server" OnClick="cancelSongButton_Click" />
            </div>
        </div>
        <div id="productionNotify" runat="server" class="mailNo" visible="false">
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
