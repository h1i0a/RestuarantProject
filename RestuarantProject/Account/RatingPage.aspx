<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RatingPage.aspx.cs" Inherits="RestuarantProject.Account.RatingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified" style="margin-right: 0px">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px; color: black;">
                Choose the Resaurant you want to rate</td>
            <td style="width: 725px; height: 28px;">
                <asp:DropDownList ID="ddlRestaurant" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px; color: red;">
                *</td>
            <td style="width: 725px; height: 28px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 28px;" colspan="2">
                <asp:Repeater ID="repeaterRating" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th style="width:350px; background-color:lightgray; text-align:center">Restaurant Rating Criteria</th>
                                <th style="width:400px; background-color:lightgray; text-align:left">Restaurant Rating</th>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnRestaurantRatingCriteriaId" runat="server" Value='<%# Eval("RestaurantRatingCritiraId") %>' />
                                </td>
                                <td style="width:350px; background-color:lightgray; text-align:center">
                                    <asp:Label ID="lblRestaurantRatingCriteria" runat="server" Text='<%# Eval("RestaurantRatingCritira") %>' />
                                </td>
                                <td style="width:400px; background-color:lightgray; text-align:center">
                                    <asp:RadioButtonList ID="rblrestaurantRatingCriteriaPoint" runat="server" RepeatDirection="Horizontal" CellSpacing="2">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px;">
                &nbsp;</td>
            <td style="width: 725px; height: 28px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 28px;" colspan="2">What's your OverAll rating for the Restaurant? <span style="color: red">*</span></td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px;">
                <asp:RadioButtonList ID="rblOverAllRating" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
            <td style="width: 725px; height: 28px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px;">Share more details about your experience:</td>
            <td style="width: 725px; height: 28px;">
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" style="margin-right:200px; width: 100%;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 28px;">
                &nbsp;</td>
            <td style="width: 725px; height: 28px;">
                <div style="text-align: left; padding-top: 10px;">
                    <asp:Button ID="btnSubmitRating" runat="server" OnClick="btnSubmit_Click_Rating" Text="Submit Rating" />
                    <asp:Button ID="btnShowMyRatings" runat="server" Text="Show my ratings" OnClick="btnShowMyRatings_Click" style="background-color: #FFFFFF" />
                    <asp:Button ID="btnShowOtherRatings" runat="server" OnClick="btnShowALLRatings_Click" Text="Show All ratings" />
                    <asp:Button ID="btnSubmitRest" runat="server" OnClick="btnSubmitNewRestaurant_Click" Text="Submit New Resturant" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
