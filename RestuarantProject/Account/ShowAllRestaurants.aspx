<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAllRestaurants.aspx.cs" Inherits="RestuarantProject.Account.ShowAllRestaurants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
               <asp:GridView ID="gvShowAllRestaurants" runat="server" AutoGenerateColumns="False" DataKeyNames="restaurantId" >
           
            <Columns>
                <asp:BoundField DataField="restaurantId" HeaderText="restaurantId" SortExpression="restaurantId" />
                <asp:BoundField DataField="restaurantName" HeaderText="restaurantName" SortExpression="restaurantName" />
                <asp:BoundField DataField="country" HeaderText="country" SortExpression="country" />
                <asp:BoundField DataField="city" HeaderText="city" SortExpression="city" />
                <asp:BoundField DataField="address" HeaderText="address" SortExpression="address" />
                <asp:BoundField DataField="Cell" HeaderText="Cell" SortExpression="Cell" />
                <asp:BoundField DataField="pricePerPerson" HeaderText="pricePerPerson" SortExpression="pricePerPerson" />
                <asp:BoundField DataField="restaurantServices" HeaderText="restaurantServices" SortExpression="restaurantServices" />
                <asp:BoundField DataField="restaurantFoodType" HeaderText="restaurantFoodType" SortExpression="restaurantFoodType" />
                <asp:BoundField DataField="restaurantFoodTime" HeaderText="restaurantFoodTime" SortExpression="restaurantFoodTime" />
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
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                If there&#39;s a change in the restaurant information press <span style="text-decoration: underline">Contact Us</span></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
                <asp:Button ID="btnCountactUs" runat="server" Text="Contact US" OnClick="btnCountactUs_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
