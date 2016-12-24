<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPlatform.aspx.cs" Inherits="InternetGameDatabase.Admin.AddPlatform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="SuccessLabel" runat="server" class="alert alert-dismissible alert-success">
        <button type="button" class="close" data-dismiss="alert">×</button>
        <strong>Platform successfully added!</strong> Feel free to dismiss this alert message.
    </div>
    <div class="jumbotron">
        <h1>AddPlatform</h1>
        <p class="lead">Both name and description are required.</p>
    </div>

    <asp:Label Text="Platform Name: " runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="AddPlatformName" runat="server">
    </asp:TextBox>

    <asp:RequiredFieldValidator ID="PlatformNameValidator" runat="server"
        Text="* Platform  Name Required"
        ControlToValidate="AddPlatformName"
        SetFocusOnError="true" Display="Dynamic">
    </asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label Text="Platform Description: " runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="AddPlatformDescription" runat="server" Rows="3" Columns="60" TextMode="MultiLine">
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="PlatformDescriptionValidator" runat="server"
        Text="* Platform Description Required"
        ControlToValidate="AddPlatformDescription"
        SetFocusOnError="true"
        Display="Dynamic">
    </asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="AddPlatformButton" runat="server" Text="Add Platform" OnClick="AddPlatformButton_Click" CausesValidation="true" />
    <asp:Label ID="AddPlatformStatusLabel" Text="" runat="server"></asp:Label>
    <!--Remove Logic -->
    <div class="jumbotron">
        <h1>Or Remove Platform</h1>
        <p class="lead">Hope this goes smoothly...</p>
    </div>
    <td>
        <asp:CheckBoxList ID="PlatformCheck" runat="server" DataValueField="PlatformID" SelectMethod="getPlatforms" ItemType="InternetGameDatabase.Models.Platform" DataTextField="PlatformName" />
    </td>
    <asp:Button ID="ConfirmRemove" runat="server" Text="Remove Platform(s)" OnClick="ConfirmRemove_Click" CausesValidation="false" />
    <div id="RemoveWarning" runat="server" class="alert alert-dismissible alert-danger" visible="false">
        <button type="button" class="close" data-dismiss="alert">×</button>
        <strong>Platform(s) not removed!</strong> Something went wrong.
    </div>

</asp:Content>
