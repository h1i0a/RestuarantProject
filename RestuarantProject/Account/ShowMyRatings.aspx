<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowMyRatings.aspx.cs" Inherits="RestuarantProject.Account.ShowMyRatings" 
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
                <asp:GridView ID="gvShowMyRatings" runat="server" AutoGenerateColumns="False" DataKeyNames="RatingId" >
           
            <Columns>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="RatingId">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server"  
                                        CommandArgument='<%# Bind("ratingId") %>' OnClick="populateForm_Click"
                                        Text='<%# Eval("ratingId")  %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>
                <asp:BoundField DataField="restaurantName" HeaderText="restaurantName" SortExpression="restaurantName" />
                <asp:BoundField DataField="overAllRating" HeaderText="overAllRating" SortExpression="overAllRating" />
                <asp:BoundField DataField="restaurantRatingCritiraAndPoints" HeaderText="restaurantRatingCritiraAndPoints" SortExpression="restaurantRatingCritiraAndPoints" />
                <asp:BoundField DataField="notes" HeaderText="notes" SortExpression="notes" />
            </Columns>
        </asp:GridView>
            </td>
        </tr>
           <tr>
        <td colspan="2">Every Red star <span style="color: red">*</span> is important to fill:</td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;">&nbsp;</td>
            <td style="width: 725px; height: 34px;">
            </td>
        </tr>
        <tr>
            <td style="width: 378px; height: 34px;"><span style="text-decoration: underline">Rating Id</span></td>
            <td style="width: 725px; height: 34px;">
                <asp:TextBox ID="txtRatingId" runat="server"></asp:TextBox>
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
            <td style="height: 28px;" colspan="2">What's your overall rating for the restaurant? <span style="color: red">*</span></td>
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
            <td></td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" style="color: #FF0000" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>
