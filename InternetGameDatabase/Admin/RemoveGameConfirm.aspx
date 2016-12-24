<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RemoveGameConfirm.aspx.cs" Inherits="InternetGameDatabase.Admin.RemoveGameConfirm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView runat="server" ID="RemoveGame" ItemType="InternetGameDatabase.Models.Game" SelectMethod="getRemoveGame" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.GameName %></h1>
            </div>
            <div>
                <img src="../Images/Temporary/<%#:Item.ImagePath %>" style="border: solid; height: 300px" alt="<%#:Item.GameName %>" />
            </div>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr>
                        <th>Genre
                        </th>
                        <th>Platform
                        </th>
                        <th>Description
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <%#: Item.Genre.GenreName%>
                        </td>
                        <td>
                            <asp:ListView DataSource="<%# Item.Platforms %>" ItemType="InternetGameDatabase.Models.Platform" runat="server">
                                <ItemTemplate><%# Eval("PlatformName") %></ItemTemplate>
                                <ItemSeparatorTemplate>, </ItemSeparatorTemplate>
                            </asp:ListView>
                        </td>

                        <td>
                            <%#: Item.GameDescription %>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:Button ID="ConfirmButton" runat="server" OnClick="ConfirmButton_Click" Text="Confirm Remove Game" />
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
