<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="NewFood.aspx.cs" Inherits="PizzaOrderWebApplication.Admin.NewFood" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Navigation" runat="server">
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/OrderList.aspx">Orders</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Contact.aspx">Contact</asp:HyperLink>
    </li>
    <li class="active">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/NewFood.aspx">Manage Pizza</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Logout.aspx">Log out</asp:HyperLink>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <form id="loginform" runat="server">
        <div class="table-list-pizza" style="width: 100%;">
                <div class="column">
                    <h2><span class="label label-info">Manage Pizza</span></h2>
                    <div class="form-group">
                        <label for="username">FOOD ID:</label>
                        <asp:TextBox runat="server" ID="txtFoodID" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="password">NAME:</label>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="username">Ingredients:</label>
                        <asp:TextBox runat="server" ID="txtIngredient" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="category">Category:</label>
                        <br />
                        <asp:DropDownList ID="selCategory" runat="server">
                            <asp:ListItem Value="1">Asian Pizza</asp:ListItem>
                            <asp:ListItem Value="2">European Pizza</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="username">Image:</label>
                        <asp:FileUpload ID="FileANHSANPHAM" runat="server" />
                    </div>
                    <div class="form-group">
                        <label>Price</label>
                        <div>
                            <label for="password">S:</label>
                            <asp:TextBox runat="server" ID="txtSizeS" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <br />
                        </div>
                        <div>
                            <label for="password">M:</label>
                            <asp:TextBox runat="server" ID="txtSizeM" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div>
                            <label for="password">L:</label>
                            <asp:TextBox runat="server" ID="txtSizeL" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="BtnAddNew" CssClass="btn btn-success" runat="server" Text="Add New" OnClick="BtnAddNew_Click" />
                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" />
                    <br />
                    <asp:Label runat="server" ID="lblNotify"></asp:Label>
                </div>

                <div class="column">
                    <h2><span class="label label-info">List Pizza</span></h2>
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="FoodID" HeaderText="ID" />
                            <asp:BoundField DataField="FoodName" HeaderText="Name" />
                            <asp:BoundField DataField="Ingredients" HeaderText="Ingredients" />
                            <asp:ImageField DataAlternateTextField="ImageString" DataImageUrlField="ImageString" DataImageUrlFormatString="/Content/Images/Menu/Pizza/{0}"  HeaderText="Image">
                            </asp:ImageField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemStyle CssClass="action" />
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Update" CommandName="UpdatePizza" CommandArgument='<%# Eval("FoodID") %>' CssClass="btn btn-info btn-sm" />
                                    <asp:Button runat="server" Text="Delete" CommandName="DeletePizza" CommandArgument='<%# Eval("FoodID") %>' CssClass="btn-warning btn btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>

        </div>

    </form>

    <br />

</asp:Content>
