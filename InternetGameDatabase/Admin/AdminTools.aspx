<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminTools.aspx.cs" Inherits="InternetGameDatabase.Admin.AdminTools1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Administration Tools</h1>
        <p class="lead">Use the following links to add games, genres, and platforms to the database.</p>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4">
            <h2>Add Game</h2>
            <p>
                Expand the library of games.
            </p>
            <p>
            <a class="btn btn-default" href="AddGame.aspx">Add Game &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Add Genre</h2>
            <p>
                Maintain a list of newly created genres.
            </p>
            <p>
                <a class="btn btn-default" href="AddGenre.aspx">Add Genre &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Add Platform</h2>
            <p>
                Keep the database current with new platforms.
            </p>
            
            <p>
                <a class="btn btn-default" href="AddPlatform.aspx">Add Platform &raquo;</a>
            </p>
        </div>
        
    </div>
</asp:Content>
