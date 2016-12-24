<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGenre.aspx.cs" Inherits="InternetGameDatabase.Admin.AddGenre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="SuccessLabel" runat="server" class="alert alert-dismissible alert-success" visible="false">
        <button type="button" class="close" data-dismiss="alert">×</button>
        <strong>Genre successfully added!</strong> Feel free to dismiss this alert message.
    </div>
    <div class="jumbotron">
        <h1>Add Genre</h1>
        <p class="lead">Both name and description are required.</p>
    </div>

    <asp:Label Text="Genre Name: " runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="AddGenreName" runat="server" ValidationGroup="Add">
    </asp:TextBox>

    <asp:RequiredFieldValidator ID="GenreNameValidator" runat="server"
        Text="* Game Name Required"
        ControlToValidate="AddGenreName"
        SetFocusOnError="true" Display="Dynamic">
    </asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label Text="Genre Description: " runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="AddGenreDescription" runat="server" ValidationGroup="Add" Rows="3" Columns="60" TextMode="MultiLine">
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="GenreDescriptionValidator" runat="server"
        Text="* Game Description Required"
        ControlToValidate="AddGenreDescription"
        SetFocusOnError="true"
        Display="Dynamic">
    </asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="AddGenreButton" runat="server" Text="Add Genre" OnClick="AddGenreButton_Click" CausesValidation="true" ValidationGroup="Add" />
    <!--Remove Logic -->
    <div class="jumbotron">
        <h1>Or Remove Genre</h1>
        <p class="lead">Genres of which games are members cannot be removed.</p>
    </div>
    <asp:DropDownList ID="DropDownGenre" runat="server"
        ItemType="InternetGameDatabase.Models.Genre"
        SelectMethod="getGenres"
        DataTextField="GenreName"
        DataValueField="GenreID">
    </asp:DropDownList>
    <asp:Button ID="ConfirmRemove" runat="server" Text="Remove Genre" OnClick="ConfirmRemove_Click" CausesValidation="false"/>
    <div id="RemoveWarning" runat="server" class="alert alert-dismissible alert-danger" visible="false">
        <button type="button" class="close" data-dismiss="alert">×</button>
        <strong>Genre not removed!</strong> One or more games belong to this genre.
    </div>

</asp:Content>
