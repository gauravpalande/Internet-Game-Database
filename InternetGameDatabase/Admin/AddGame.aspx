<%@ Page Title="Add Game" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGame.aspx.cs" Inherits="InternetGameDatabase.Admin.AdminTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1><%#:Page.Title%></h1>
    </div>
    <div class="jumbotron">
        <h1 id="Jumbo" runat="server">Add Game</h1>
        <p class="lead">Fill in all fields.</p>
    </div>
    <table class="table table-striped table-hover ">
        <thead>
            <tr>
                <th>Name
                </th>
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
                    <asp:TextBox ID="AddGameName" runat="server" columns="120">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="GameNameValidator" runat="server"
                        Text="* Game Name Required"
                        ControlToValidate="AddGameName"
                        SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>

                </td>
                <td>
                    <asp:DropDownList ID="DropDownGenre" runat="server"
                        ItemType="InternetGameDatabase.Models.Genre"
                        SelectMethod="getGenres"
                        DataTextField="GenreName" ForeColor="Black"
                        DataValueField="GenreID">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBoxList ID="PlatformCheck" runat="server" DataValueField="PlatformID" SelectMethod="getPlatforms" ItemType="InternetGameDatabase.Models.Platform" DataTextField="PlatformName" />
                </td>
                <td>
                    <asp:TextBox ID="AddGameDescription" runat="server" Rows="10" Columns="60" TextMode="MultiLine">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="GameDescriptionValidator" runat="server"
                        Text="* Game Description Required"
                        ControlToValidate="AddGameDescription"
                        SetFocusOnError="true"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <br />
                <asp:FileUpload ID="gameImageConfirm" onchange="document.getElementById('quickLabel').innerHTML = this.value;" runat="server" />
                <!--<asp:FileUpload ID="gameImage" runat="server" />-->
                <p id="quickLabel"></p>
                <br />
                <asp:ImageButton ID="GameIcon" runat="server" Height="200px" ImageUrl="~/Images/Icons/upload-icon.png" OnClick="GameIcon_Click" />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="GameIcon" />
            </Triggers>
        </asp:UpdatePanel>
        <div>
        </div>
        <br />
        <asp:Label ID="imagePathLabel" runat="server"></asp:Label>
    </div>
    <div>
        <asp:Button ID="AddGame" runat="server" OnClick="AddGame_Click" Text="AddGame" />
        <asp:Button ID="UpdateGame" runat="server" OnClick="UpdateGame_Click" Text="Update Game" Visible="false" />
        <asp:Label ID="GameDetailLabel" runat="server"></asp:Label>
    </div>


</asp:Content>
