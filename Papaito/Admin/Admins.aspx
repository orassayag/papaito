<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Admins.aspx.cs" Inherits="Admin_Admins" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="adminsHidden" runat="server" type="hidden" value="" />
        <div id="mainAdmins" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="adminsSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="adminsSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addAdmins" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="adminsUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Created On:
                                    </td>
                                    <td>
                                        <asp:Label ID="adminsCreationLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Login:
                                    </td>
                                    <td>
                                        <asp:Label ID="adminsLastLoginLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="adminsLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="adminsStatusLabel" runat="server" Text=""></asp:Label>
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
                                        User ID:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="adminsUserID" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Password:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="adminsPassword1" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Re-Enter Password:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="adminsPassword2" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addAdminsButton" Text="Add" runat="server" OnClick="addAdminsButton_Click" />
                <asp:Button ID="disableAdminsButton" Text="Disable" runat="server" OnClick="disableAdminsButton_Click" />
                <asp:Button ID="enableAdminsButton" Text="Enable" runat="server" OnClick="enableAdminsButton_Click" />
                <asp:Button ID="cancelAdminsButton" Text="Cancel" runat="server" OnClick="cancelAdminsButton_Click" />
            </div>
        </div>
        <div id="removeUpdateAdmins" runat="server" class="mailNo" visible="false">
            <input id="adminsHiddenRe" type="hidden" runat="server" value="" />
            <input id="adminsHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Admin:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateAdminsSelector" runat="server" Width="200px" Height="200px">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeAdminsPicButton" Text="Remove" runat="server" OnClick="removeAdminsButton_Click" />
                <asp:Button ID="updateAdminsPicButton" Text="Update" runat="server" OnClick="updateAdminsButton_Click" />
                <asp:Button ID="cancelAdminsPicButton" Text="Cancel" runat="server" OnClick="cancelAdminsButton_Click" />
            </div>
        </div>
        <div id="adminsNotify" runat="server" class="mailNo" visible="false">
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
