<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Publish.aspx.cs" Inherits="Admin_Publish" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="publishHidden" runat="server" type="hidden" value="" />
        <div id="mainPublish" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="publishSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="publishSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addPublishGallery" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="galleryUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                        <asp:Label ID="galleryLastUpdateLabel" runat="server" Text=""></asp:Label>
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
                                        Gallery Name Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishGalleryNameHe" CssClass="textHeb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Gallery Name English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishGalleryNameEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Gallery Place:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishGalleryPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addPublishGalleryButton" Text="Add" runat="server" OnClick="addPublishGalleryButton_Click" />
                <asp:Button ID="cancelAddPublishGalleryButton" Text="Cancel" runat="server" OnClick="cancelPublishGalleryButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePublishGallery" runat="server" class="mailNo" visible="false">
            <input id="publishGalleryHiddenRe" type="hidden" runat="server" value="" />
            <input id="publishGalleryHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Gallery:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdatePublishGallerySelector" runat="server" Width="200px"
                                Height="200px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removePublishGalleryButton" Text="Remove" runat="server" OnClick="removePublishGalleryButton_Click" />
                <asp:Button ID="updatePublishGalleryButton" Text="Update" runat="server" OnClick="updatePublishGalleryButton_Click" />
                <asp:Button ID="cancelPublishGalleryButton" Text="Cancel" runat="server" OnClick="cancelPublishGalleryButton_Click" />
            </div>
        </div>
        <div id="addPicToGallery" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="publishPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="publishPicLastUpdateLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="publishPicStatusLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Add To Gallery:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="addPicGallerySelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
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
                                            Text Of Picture</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishPicTextHe" CssClass="textHeb" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishPicTextEn" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture Place In Home Page:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="topPlaceSelector" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture Place In Gallery:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="publishPicPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <img id="publishPicUploadPic" alt="" src="" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture:
                                    </td>
                                    <td align="left">
                                        <input id="publishPicUpload" type="file" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addPublishPicGalleryButton" Text="Add" runat="server" OnClick="addPublishPicGalleryButton_Click" />
                <asp:Button ID="disablePublishPicGalleryButton" Text="Disable" runat="server" OnClick="disablePublishPicGalleryButton_Click" />
                <asp:Button ID="enablePublishPicGalleryButton" Text="Enable" runat="server" OnClick="enablePublishPicGalleryButton_Click" />
                <asp:Button ID="cancelAddPicGalleryButton" Text="Cancel" runat="server" OnClick="cancelAddPicGalleryButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePicGallery" runat="server" class="mailNo" visible="false">
            <input id="publishPicHiddenRe" type="hidden" runat="server" value="" />
            <input id="publishPicHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Picture:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdatePicGallerySelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removePublishPicGalleryButton" Text="Remove" runat="server" OnClick="removePublishPicGalleryButton_Click" />
                <asp:Button ID="updatePublishPicGalleryButton" Text="Update" runat="server" OnClick="updatePublishPicGalleryButton_Click" />
                <asp:Button ID="cancelPublishPicGalleryButton" Text="Cancel" runat="server" OnClick="cancelPublishPicGalleryButton_Click" />
            </div>
        </div>
        <div id="publishNotify" runat="server" class="mailNo" visible="false">
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
