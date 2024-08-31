<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAllRatings.aspx.cs" Inherits="RestuarantProject.Account.ShowAllRatings" 
    EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvShowAllRatings" runat="server" AutoGenerateColumns="False" DataKeyNames="RatingId" >
           
            <Columns>
                <asp:BoundField DataField="ratingId" HeaderText="ratingId" SortExpression="ratingId" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="restaurantName" HeaderText="restaurantName" SortExpression="restaurantName" />
                <asp:BoundField DataField="restaurantRatingCritiraAndPoints" HeaderText="restaurantRatingCritiraAndPoints" SortExpression="restaurantRatingCritiraAndPoints" />
                <asp:BoundField DataField="overAllRating" HeaderText="overAllRating" SortExpression="overAllRating" />
                <asp:BoundField DataField="notes" HeaderText="notes" SortExpression="notes" />
            </Columns>
        </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Enter the Restaurant Name you want to search about ?</td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExportToWordPdf" runat="server" Text="Export to pdf / word" OnClick="btnExportToWordPdf_Click" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
