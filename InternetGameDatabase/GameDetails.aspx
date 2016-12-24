<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="GameDetails.aspx.cs"
    Inherits="InternetGameDatabase.GameDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="gameDetail" runat="server" ItemType="InternetGameDatabase.Models.Game" SelectMethod="getGame" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.GameName %></h1>
            </div>
            <div>
                <img src="<%#:(Item.ImagePath.Substring(0,4).Equals("http")) ? Item.ImagePath : "/Images/Games/" + Item.ImagePath %>" style="border: solid; height: 100px" alt="<%#:Item.GameName %>" />
            </div>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr>
                        <th>Genre
                        </th>
                        <th>Platform
                        </th>
                        <th>Avg. Critic Rating
                        </th>
                        <th>Avg. User Rating
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
                            <%#: (Item.GameRating == null) ? "NA":Item.GameRating.Value.ToString()%>
                        </td>
                        <td>
                            <%#: (Item.AverageUserRating == null) ? "NA":Item.AverageUserRating.Value.ToString()%>
                        </td>
                        <td>
                            <%#: Item.GameDescription %>
                        </td>
                    </tr>
                </tbody>
            </table>

        </ItemTemplate>
    </asp:FormView>
    <asp:LinkButton ID="ReviewGame" runat="server" Text="Review Game" Visible="false" />
    <br />
    <asp:LinkButton ID="RemoveGame" runat="server" Text="Remove Game" Visible="false" />
    <asp:LinkButton ID="UpdateGame" runat="server" Text="Update Game" Visible="false" />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Width="292px" Visible="false"></asp:TextBox>
    &nbsp;
    <asp:Button ID="SearchButton" runat="server" Text="Buy it out on Amazon.com" OnClick="SearchButton_Click" />
    <br />
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>

    <div>
        <h1>Reviews</h1>


        <table class="table table-striped table-hover ">
            <thead>
                <tr>
                    <th>User Name
                    </th>
                    <th>Quick Impression
                    </th>
                    <th>Numeric Rating
                    </th>
                    <th>Detailed Description
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:ListView ID="UserRatingList" runat="server" SelectMethod="UserRatingList_GetData" ItemType="InternetGameDatabase.Models.UserRating">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Item.UserName %>
                            </td>
                            <td>
                                <%# Item.QuickDescription %>
                            </td>
                            <td>
                                <%# Item.NumericalRating %>
                            </td>
                            <td>
                                <%# Item.DetailedDescription %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </tbody>
        </table>


    </div>
</asp:Content>
