<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InternetGameDatabase._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>IGDB</h1>
        <p class="lead">Web App to search for new games and meet other gamers.</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Get Started!</h2>
            <p>
                Log in or register for a new account.
            </p>
            <br />
            <p>
            <a class="btn btn-default" href="/Account/Login.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get Involved!</h2>
            <p>
                Provide your own ratings and strategies for your favorite games.
            </p>
            
            <p>
                <a class="btn btn-default" href="Games.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get Gaming!</h2>
            <p>
                Find and purchase new games. Meet other gamers with similar interests.
            </p>
            <p>
                <a class="btn btn-default" href="Default.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
