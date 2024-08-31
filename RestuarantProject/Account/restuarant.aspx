<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="restuarant.aspx.cs" Inherits="RestuarantProject.Account.restuarant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified" style="margin-right: 0px">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">Every Red star <span style="color: red">*</span> is important to fill:</td>
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
                <asp:DropDownList ID="ddlCountry" runat="server" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;">How much the avg spend per person?</td>
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
            <td style="width: 378px; height: 28px;">
                &nbsp;</td>
            <td style="width: 725px; height: 28px;">
                <div style="text-align: left; padding-top: 10px;">
                    <asp:Button ID="btnSubmitRest" runat="server" OnClick="btnSubmit_Click" Text="Submit Resturant" />

                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />

                </div>
            </td>
        </tr>
    </table>
</asp:Content>
