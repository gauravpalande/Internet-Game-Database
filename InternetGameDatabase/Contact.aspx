<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="InternetGameDatabase.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>IGDB contact page.</h3>
    <address>
        One Nowhere Ave<br />
        Noplace, ZZ XXXXX-XXXX<br />
        <abbr title="Phone">P:</abbr>
        555.555.5555
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:admin@igdb.com">admin@igdb.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:admin@igdb.com">admin@igdb.com</a>
    </address>
</asp:Content>
