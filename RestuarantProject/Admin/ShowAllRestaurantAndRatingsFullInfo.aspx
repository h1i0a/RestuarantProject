<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAllRestaurantAndRatingsFullInfo.aspx.cs" Inherits="RestuarantProject.Admin.ShowAllRestaurantAndRatingsFullInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                 <asp:GridView ID="gvShowAllRatingsAndRestaurants" runat="server" AutoGenerateColumns="False" DataKeyNames="RatingId" >
           
            <Columns>
                <asp:BoundField DataField="RatingId" HeaderText="RatingId" SortExpression="RatingId" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="restaurantName" HeaderText="restaurantName" SortExpression="restaurantName" />
                <asp:BoundField DataField="country" HeaderText="country" SortExpression="country" />
                <asp:BoundField DataField="city" HeaderText="city" SortExpression="city" />
                <asp:BoundField DataField="address" HeaderText="address" SortExpression="address" />
                <asp:BoundField DataField="cell" HeaderText="cell" SortExpression="cell" />
                <asp:BoundField DataField="pricePerPerson" HeaderText="pricePerPerson" SortExpression="pricePerPerson" />
                <asp:BoundField DataField="restaurantServices" HeaderText="restaurantServices" SortExpression="restaurantServices" />
                <asp:BoundField DataField="restaurantFoodType" HeaderText="restaurantFoodType" SortExpression="restaurantFoodType" />
                <asp:BoundField DataField="restaurantFoodTime" HeaderText="restaurantFoodTime" SortExpression="restaurantFoodTime" />
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
                <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click" Text="ExportToExcel" />
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
