<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FireWorks._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Button ID="Start" runat="server" Text="Start" OnClick="Program_Start" />
    <asp:Button ID="Refresher" runat="server" OnClick="Refresh" Text="Refresh text" />
    <input type="Text" name="textin" placeholder="Input" data-toggle="tooltip" id="textin" runat="server" />
    <asp:Button ID="submit_button" runat="server" OnClick="Submit_button" Text="Submit" UseSubmitBehavior="true" />
    <br />
    <asp:Literal runat="server" ID="textout" />
    <script>
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
