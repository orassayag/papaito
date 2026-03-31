<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Staff.aspx.cs" Inherits="Admin_Staff" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="staffHidden" runat="server" type="hidden" value="" />
        <div id="mainStaff" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="staffSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="staffSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="updateStaff" runat="server" class="mailNo" visible="false">
            <input id="existingStaffHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Existing Staff:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="staffExistingSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="staffExistingSelector_SelectedIndexChanged"
                                Width="100px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="updateExistingCancel" Text="Cancel" runat="server" OnClick="updateExistingCancel_Click" />
            </div>
        </div>
        <div id="updateExistingStaff" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="existingStaffUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="existingStaffLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="existingStaffStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Staff Name Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffNameHe" runat="server" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Staff Name English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffNameEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Staff Text Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffExistingTextHe" CssClass="textHeb" runat="server" Height="67px"
                                            Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Staff Text English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffExistingTextEn" runat="server" Height="67px" Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addExistingStaffButton" Text="Add" runat="server" OnClick="addExistingStaffButton_Click" />
                <asp:Button ID="cancelAddExistingStaffButton" Text="Cancel" runat="server" OnClick="cancelAddExistingStaffButton_Click" />
            </div>
        </div>
        <div id="addNewStaff" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="newStaffUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="newStaffLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="newStaffStatusLabel" runat="server" Text=""></asp:Label>
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
                                        Name In Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="newStaffNameHe" runat="server" CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Name In English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="newStaffNameEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Text In Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffTextHe" TextMode="MultiLine" runat="server" Height="67px" Width="211px"
                                            CssClass="textHeb"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Text In English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffTextEn" TextMode="MultiLine" runat="server" Height="67px" Width="211px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Place In Page:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="staffPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture:
                                    </td>
                                    <td align="left">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <img id="staffPicUploadPic" alt="" src="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <input id="staffPicUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addNewStaffButton" Text="Add" runat="server" OnClick="addNewStaffButton_Click" />
                <asp:Button ID="disableStaffButton" Text="Disable" runat="server" OnClick="disableStaffButton_Click" />
                <asp:Button ID="enableStaffButton" Text="Enable" runat="server" OnClick="enableStaffButton_Click" />
                <asp:Button ID="cancelAddNewStaffButton" Text="Cancel" runat="server" OnClick="cancelAddNewStaffButton_Click" />
            </div>
        </div>
        <div id="removeUpdateNewStaff" runat="server" class="mailNo" visible="false">
            <input id="newStaffHiddenRe" type="hidden" runat="server" value="" />
            <input id="newStaffHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select New Staff:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateNewStaffSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeNewStaffButton" Text="Remove" runat="server" OnClick="removeNewStaffButton_Click" />
                <asp:Button ID="updateNewStaffButton" Text="Update" runat="server" OnClick="updateNewStaffButton_Click" />
                <asp:Button ID="cancelNewStaffButton" Text="Cancel" runat="server" OnClick="cancelNewStaffButton_Click" />
            </div>
        </div>
        <div id="staffNotify" runat="server" class="mailNo" visible="false">
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
