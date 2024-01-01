using PizzaOrderWebApplication.MyAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PizzaOrderWebApplication.Models;

namespace PizzaOrderWebApplication.Admin
{
    public partial class NewFood : System.Web.UI.Page
    {
        private string FileNameUpdate
        {
            get { return ViewState["FileNameUpdate"] as string; }
            set { ViewState["FileNameUpdate"] = value; }
        }
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
            String fileAddress = uploadAnh("user.png");
            int category = int.Parse(selCategory.SelectedValue);
            float sizeS = float.Parse(txtSizeS.Text);
            float sizeM = float.Parse(txtSizeM.Text);
            float sizeL = float.Parse(txtSizeL.Text);
            if (DBContext.InsertFood(foodId, name, ingredient, fileAddress, category) != 0
                && DBContext.InsertDish(foodId, "S", sizeS) != 0
                && DBContext.InsertDish(foodId, "M", sizeM) != 0
                && DBContext.InsertDish(foodId, "L", sizeL) != 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Thêm sản phẩm thành công')", true);
                GetDataToGridView();
                txtFoodID.Text = "";
                txtIngredient.Text = "";
                txtName.Text = "";
                txtSizeL.Text = "";
                txtSizeS.Text = "";
                txtSizeM.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Thêm sản phẩm thất bại')", true);

            }

        }

        public string uploadAnh(String nameFileDuPhong)
        {
            string fileName = nameFileDuPhong;
            if (FileANHSANPHAM.HasFile)
            {
                fileName = Path.GetFileName(FileANHSANPHAM.FileName);
                string filePath = Server.MapPath("~/Content/Images/Menu/Pizza/") + fileName;
                FileANHSANPHAM.SaveAs(filePath);
            }
            return fileName;
        }
        public void GetDataToGridView()
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
                if (DBContext.DeleteFood(FoodID) != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Xóa sản phẩm thành công')", true);
                    GetDataToGridView();
                }
            }
            else if (e.CommandName == "UpdatePizza")
            {
                string FoodID = e.CommandArgument.ToString();
                Food food = DBContext.GetPizzaById(FoodID);
                txtFoodID.Text = food.FoodID;
                txtIngredient.Text = food.Ingredients;
                txtName.Text = food.FoodName;
                FileNameUpdate = food.ImageString;
                selCategory.SelectedValue = food.CategoryID.ToString();

                Dictionary<string, float> prices = DBContext.GetPizzaPriceById(FoodID);
                float priceS, priceM, priceL;
                if (prices.TryGetValue("L", out priceL))
                {
                    txtSizeL.Text = priceL.ToString();
                }
                if (prices.TryGetValue("M", out priceM))
                {
                    txtSizeM.Text = priceM.ToString();
                }
                if (prices.TryGetValue("S", out priceS))
                {
                    txtSizeS.Text = priceS.ToString();
                }


            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            String foodId = txtFoodID.Text.Trim();
            String name = txtName.Text.Trim();
            String ingredient = txtIngredient.Text.Trim();
            String fileAddress = uploadAnh(FileNameUpdate);
            int category = int.Parse(selCategory.SelectedValue);
            float sizeS = float.Parse(txtSizeS.Text);
            float sizeM = float.Parse(txtSizeM.Text);
            float sizeL = float.Parse(txtSizeL.Text);
            if (DBContext.UpdateFood(foodId, name, ingredient, fileAddress, category) != 0
                && DBContext.UpdateDish(foodId, "S", sizeS) != 0
                && DBContext.UpdateDish(foodId, "M", sizeM) != 0
                && DBContext.UpdateDish(foodId, "L", sizeL) != 0
                )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Cập nhật sản phẩm thành công')", true);
                GetDataToGridView();
                txtFoodID.Text = "";
                txtIngredient.Text = "";
                txtName.Text = "";
                txtSizeL.Text = "";
                txtSizeS.Text = "";
                txtSizeM.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Cập sản phẩm thất bại')", true);

            }
        }
    }
}