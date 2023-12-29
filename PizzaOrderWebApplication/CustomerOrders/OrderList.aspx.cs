using PizzaOrderWebApplication.Models;
using PizzaOrderWebApplication.MyAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PizzaOrderWebApplication.CustomerOrders
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["AccountUser"]!=null)
            {
                GetDataToGridView();
            }
            else
            {
                Response.Redirect("~/Home/Login");
            }      
        }

        void GetDataToGridView()
        {
            User user = (User)Session["AccountUser"];
            List<PizzaOrderWebApplication.Models.Order> listOd = DBContext.GetAllOrdersbyUser(user.CustomerName);
            gvOrders.DataSource = listOd;
            gvOrders.DataBind();
        }

        protected void timerUpdate_Tick(object sender, EventArgs e)
        {
            GetDataToGridView();
        }

        protected void Orders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                //get order id
                int OrderID = Convert.ToInt32(e.CommandArgument);

                Response.Redirect("~/CustomerOrders/OrderDetail.aspx?OrderID=" + OrderID);
            }
        }
    }
}