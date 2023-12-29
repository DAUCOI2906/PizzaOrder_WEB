<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Layout/WebformLayout.Master"
    AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="PizzaOrderWebApplication.CustomerOrders.OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Navigation" runat="server">
    <li class="active">
        <asp:HyperLink runat="server" NavigateUrl="~/Home/Index">Home</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/CustomerOrder/Index">Menu</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/Home/Contact">Contact</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/CustomerOrders/OrderList.aspx">Order</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" NavigateUrl="~/Home/Login">Login</asp:HyperLink>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h2><span class="label label-info">Order List</span></h2>
    <br />
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepanel" runat="server">
            <ContentTemplate>
                <asp:Timer ID="timerUpdate" runat="server" Interval="3000" OnTick="timerUpdate_Tick" />
                <asp:GridView ID="gvOrders" CssClass="table table-responsive" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnRowCommand="Orders_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                        <asp:BoundField DataField="ShippedDate" HeaderText="Delivery Date" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Name" />
                        <asp:BoundField DataField="CustomerAddress" HeaderText="Address" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="Note" HeaderText="Note" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="OrderDetail.aspx?OrderID=<%# Eval("OrderID") %>">View Order Details</a>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
