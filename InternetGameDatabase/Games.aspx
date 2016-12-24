<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Games.aspx.cs" Inherits="InternetGameDatabase.Games" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Library of Games Added So Far</h1>
        <p class="lead">Click on the Links Below for a Detailed View</p>
    </div>
    <table class="table table-striped table-hover ">
        <thead>
            <tr>
                <th>Filter by:
                </th>
                <th></th>
                <th></th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="FilterGameName" runat="server">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownGenre" runat="server"
                        ItemType="InternetGameDatabase.Models.Genre"
                        SelectMethod="getGenres"
                        DataTextField="GenreName"
                        DataValueField="GenreID">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBoxList ID="chklstStates" runat="server" SelectMethod="getPlatforms" ItemType="InternetGameDatabase.Models.Platform"
                        DataTextField="PlatformName" DataValueField="PlatformID" RepeatColumns="5" RepeatDirection="Horizontal" />
                </td>
                <td>
                    <asp:Button ID="FilterButton" OnClick="FilterButton_Click" runat="server" Text="Apply Filter" />
                </td>
            </tr>
        </tbody>
    </table>

            <asp:ListView ID="GamesListView" ItemType="InternetGameDatabase.Models.Game" runat="server" SelectMethod="GetGames" OnSorting="GamesListView_Sorting">
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" class="table table-striped table-hover ">
            <tr>
                <th></th>
                <th>Name
                    <asp:ImageButton ID="imGameName" CommandArgument="GAMENAME" CommandName="Sort" ImageUrl="/Images/Icons/desc.png" runat="server" />
                </th>
                <th>Genre
                    <%--<asp:ImageButton ID="imGenre" CommandArgument="GENRE.GENRENAME" CommandName="Sort" ImageUrl="/Images/Icons/desc.png" runat="server" />--%>
                </th>
                <th>Platform
                    <%--<asp:ImageButton ID="imPlatform" CommandArgument="PLATFORMS" CommandName="Sort" ImageUrl="/Images/Icons/desc.png" runat="server" />--%>
                </th>
                <th>Avg. Critic Rating
                    <asp:ImageButton ID="imCriticRating" CommandArgument="GAMERATING" CommandName="Sort" ImageUrl="/Images/Icons/desc.png" runat="server" />
                </th>
                <th>Avg. User Rating
                    <asp:ImageButton ID="imUserRating" CommandArgument="AVERAGEUSERRATING" CommandName="Sort" ImageUrl="/Images/Icons/desc.png" runat="server" />
                </th>
                <th>Description
                </th>
            </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                        </table>

                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <a href="/GameDetails.aspx?id=<%#: Item.GameID %>">
                                <img src="<%#:(Item.ImagePath.Substring(0,4).Equals("http")) ? Item.ImagePath : "/Images/Games/" + Item.ImagePath %>" style="border: solid; height: 100px" alt="<%#:Item.GameName %>" />
                            </a>
                        </td>
                        <td>
                            <b style="font-size: large; font-style: normal; text-align: left">
                                <a href="/GameDetails.aspx?id=<%#: Item.GameID %>">
                                    <%#: Item.GameName %> 
                                </a>
                            </b>
                        </td>
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
                            <%#: (Item.GameRating == null) ? "NA":Item.GameRating.Value.ToString()%>
                        </td>
                        <td>
                            <%#: (Item.AverageUserRating == null) ? "NA":Item.AverageUserRating.Value.ToString()%>
                        </td>
                        <td>
                            <%#: Item.GameDescription %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>

    <div id="GameMenu" style="text-align: center">
        <asp:ListView ID="gameList" ItemType="InternetGameDatabase.Models.Game" runat="server" SelectMethod="GetGames">
            <ItemTemplate>
                <b style="font-size: large; font-style: normal; text-align: left">
                    <a href="/GameDetails.aspx?id=<%#: Item.GameID %>">
                        <%#: Item.GameName %>
                    </a>
                </b>
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <br />
            </ItemSeparatorTemplate>
        </asp:ListView>
    </div>
</asp:Content>
