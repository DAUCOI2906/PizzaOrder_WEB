using PizzaOrderWebApplication.MyAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PizzaOrderWebApplication.Admin
{
    public partial class NewFood : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["admin"];
                if (cookie != null)
                {
                    GetDataToGridView();
                }
                else
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
            }
        }

        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            String foodId = txtFoodID.Text.Trim();
            String name = txtName.Text.Trim();
            String ingredient = txtIngredient.Text.Trim();
            String fileAddress = uploadAnh();
            int category = int.Parse(selCategory.SelectedValue);
            float sizeS = float.Parse(txtSizeS.Text);
            float sizeM = float.Parse(txtSizeM.Text);
            float sizeL = float.Parse(txtSizeL.Text);
            if (DBContext.InsertFood(foodId, name, ingredient, fileAddress, category) != 0
                && DBContext.InsertDish(foodId, "S", sizeS) != 0
                && DBContext.InsertDish(foodId, "M", sizeM) != 0
                && DBContext.InsertDish(foodId, "L", sizeL) != 0)
            {
                lblNotify.Text = "Thêm sản phẩm thành công";
                txtFoodID.Text = "";
                txtIngredient.Text = "";
                txtName.Text = "";
                txtSizeL.Text = "";
                txtSizeS.Text = "";
                txtSizeM.Text = "";
            }
            else
            {
                lblNotify.Text = "Lỗi không thể thêm sản phẩm";
            }

        }

        public string uploadAnh()
        {
            string fileName = "";
            if (FileANHSANPHAM.HasFile)
            {
                fileName = Path.GetFileName(FileANHSANPHAM.FileName);
                string filePath = Server.MapPath("~/Content/Images/Menu/Pizza/") + fileName;
                FileANHSANPHAM.SaveAs(filePath);
                lblNotify.Text = "File uploaded successfully!";
            }
            else
            {
                lblNotify.Text = "Please select a file to upload.";
            }
            return fileName;
        }
        void GetDataToGridView()
        {
            List<PizzaOrderWebApplication.Models.Food> listOd = DBContext.GetAllPizza();
            GridView1.DataSource = listOd;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeletePizza")
            {
                //get order id
                string FoodID = e.CommandArgument.ToString();
                string confirmScript = "if (confirm('Are you sure you want to proceed?')) { deleteFood('" + FoodID + "'); }";

                // Đăng ký script sử dụng ScriptManager
                ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmScript", confirmScript, true);

                //Response.Redirect("~/Admin/OrderDetail.aspx?OrderID=" + OrderID);
            }
        }

    }
}