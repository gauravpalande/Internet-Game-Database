<%@ Page Title="User Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserReview.aspx.cs" Inherits="InternetGameDatabase.User.UserReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1><%#:Page.Title%></h1>
    </div>
    <div>
        <asp:ListView ID="gameDetail" runat="server" ItemType="InternetGameDatabase.Models.Game" SelectMethod="getGame" RenderOuterTable="false">
            <ItemTemplate>
                <div>
                    <h1>Review <%#:Item.GameName %>?</h1>
                </div>
                <div>
                    <img src="<%#:(Item.ImagePath.Substring(0,4).Equals("http")) ? Item.ImagePath : "/Images/Games/" + Item.ImagePath %>" style="border: solid; height: 100px" alt="<%#:Item.GameName %>" />
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div>
        <table class="table table-striped table-hover ">
            <thead>
                <tr>
                    <th>Quick Impression
                    </th>
                    <th>Numeric Rating
                    </th>
                    <th>Detailed Description
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:TextBox ID="QuickText" runat="server" Columns="140">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="QuickTextValidator" runat="server"
                            Text="* Game Name Required"
                            ControlToValidate="QuickText"
                            SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownRating" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="DetailedText" runat="server" Rows="10" Columns="60" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="DetailedValidator" runat="server"
                            Text="* Detailed review required"
                            ControlToValidate="DetailedText"
                            SetFocusOnError="true"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <asp:Button ID="SubmitReview" runat="server" OnClick="SubmitReview_Click" Text="Submit Review" />

    </div>

</asp:Content>
