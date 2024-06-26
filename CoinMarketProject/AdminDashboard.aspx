<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="CoinMarketProject.AdminDashboard" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Admin Dashboard</h2>
        <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-danger mr-2" CommandArgument='<%# Eval("UserId") %>' OnClick="DeleteButton_Click" />
                        <asp:Button ID="ChangeRoleButton" runat="server" Text="Change Role" CssClass="btn btn-secondary" CommandArgument='<%# Eval("UserId") + ";" + Eval("Role") %>' OnClick="ChangeRoleButton_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
