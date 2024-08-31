<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAllRestaurantsAdmin.aspx.cs" Inherits="RestuarantProject.Admin.ShowAllRestaurantsAdmin" %>
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
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="restaurantId">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server"  
                                        CommandArgument='<%# Bind("restaurantId") %>' OnClick="populateForm_Click"
                                        Text='<%# Eval("restaurantId")  %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>
                <asp:BoundField DataField="restaurantName" HeaderText="restaurantName" SortExpression="restaurantName" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Cell" HeaderText="Cell" SortExpression="Cell" />
                <asp:BoundField DataField="pricePerPerson" HeaderText="pricePerPerson" SortExpression="pricePerPerson" />
                <asp:BoundField DataField="RestaurantServices" HeaderText="RestaurantServices" SortExpression="RestaurantServices" />
                <asp:BoundField DataField="RestaurantFoodType" HeaderText="RestaurantFoodType" SortExpression="RestaurantFoodType" />
                <asp:BoundField DataField="RestaurantFoodTime" HeaderText="RestaurantFoodTime" SortExpression="RestaurantFoodTime" />
            </Columns>
        </asp:GridView>
            </td>
        </tr>
           <tr>
        <td colspan="2">Every Red star <span style="color: red">*</span> is important to fill:</td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;">Restaurant Id <span style="color: red">*</span></td>
            <td style="width: 725px; height: 34px;">
                <asp:TextBox ID="txtRestaurantId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;">Restaurant Name <span style="color: red">*</span></td>
            <td style="width: 725px; height: 34px;">
                <asp:TextBox ID="TxtRestaurantName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px">Cell <span style="color: red">*</span></td>
            <td style="width: 725px">
                <asp:TextBox ID="txtCell" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px">Address <span style="color: red">*</span></td>
            <td style="width: 725px">
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px">City <span style="color: red">*</span></td>
            <td style="width: 725px">
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px">Country <span style="color: red">*</span></td>
            <td style="width: 725px">
                <asp:DropDownList ID="ddlCountry" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;">How much did you spend per person?</td>
            <td style="width: 725px; height: 34px;">
                <asp:TextBox ID="txtPricePerPerson" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">What's the food type that the restaurant provides? <span style="color: red">*</span></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBoxList ID="cblFoodtype" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2">What services does it provide? <span style="color: red">*</span></td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px;">
                <asp:CheckBoxList ID="cblRestuarantServices" runat="server">
                </asp:CheckBoxList>
            </td>
            <td style="width: 725px; height: 28px;">
                <asp:CheckBoxList ID="cblFoodTime" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px; color: red;">
                *</td>
            <td style="width: 725px; height: 28px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" style="color: #FF0000" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click" Text="ExportToExcel" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>
