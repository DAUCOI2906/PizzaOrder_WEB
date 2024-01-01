using PizzaOrderWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PizzaOrderWebApplication.MyAPI
{
    public class DBContext
    {

        public static SqlConnection GetConnection()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["PizzaDB"].ToString();
            SqlConnection conn = new SqlConnection(strConnection);
            return conn;
        }

        static public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();
            SqlConnection connection = GetConnection();

            string query = @"SELECT [OrderID]
                              ,[OrderDate]
                              ,[ShippedDate]
                              ,[CustomerName]
                              ,[CustomerAddress]
                              ,[Phone]
                              ,[Note]
                          FROM [dbo].[Orders]";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Order o = new Order
                {
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                    ShippedDate = Convert.ToDateTime(dr["ShippedDate"]),
                    CustomerName = dr["CustomerName"].ToString(),
                    CustomerAddress = dr["CustomerAddress"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    Note = dr["Note"].ToString()
                };
                list.Add(o);
            }
            return list;
        }
        static public List<Order> GetAllOrdersbyUser(String username)
        {
            List<Order> list = new List<Order>();
            SqlConnection connection = GetConnection();

            string query = @"SELECT [OrderID]
                              ,[OrderDate]
                              ,[ShippedDate]
                              ,[CustomerName]
                              ,[CustomerAddress]
                              ,[Phone]
                              ,[Note]
                          FROM [dbo].[Orders] WHERE CustomerName='" + username + "'";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Order o = new Order
                {
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                    ShippedDate = Convert.ToDateTime(dr["ShippedDate"]),
                    CustomerName = dr["CustomerName"].ToString(),
                    CustomerAddress = dr["CustomerAddress"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    Note = dr["Note"].ToString()
                };
                //int OrderId = Convert.ToInt32(dr["OrderID"]);
                //DateTime OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                //DateTime ShippedDate = Convert.ToDateTime(dr["ShippedDate"]);
                //string CustomerName = dr["CustomerName"].ToString();
                //string CustomerAddress = dr["CustomerAddress"].ToString();
                //string Phone = dr["Phone"].ToString();
                //string Note = dr["Note"].ToString();
                //Order o = new Order(OrderId, OrderDate, ShippedDate, CustomerName, CustomerAddress, Phone, Note);
                list.Add(o);
            }
            return list;
        }

        static public List<OrderDetail> GetOrderDetailByOid(int odID)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            SqlConnection connection = GetConnection();
            string query = @"SELECT [OrderID]
                                  ,od.[FoodID]
                                  ,od.[Size]
                                  ,[Discount]
                                  ,[Quantity]
								  ,di.Price
								  ,fo.FoodName
                              FROM [dbo].[OrderDetail] od, [dbo].[Dish] di, [dbo].[Food] fo
                              WHERE od.FoodID = di.FoodID AND od.Size = di.Size
							        AND di.FoodID = fo.FoodID
							        AND [OrderID] = @odID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@odID", odID));
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Food food = new Food
                {
                    FoodName = dr["FoodName"].ToString()
                };
                Dish dish = new Dish
                {
                    Food = food,
                    Price = Convert.ToDouble(dr["Price"])
                };
                OrderDetail o = new OrderDetail
                {
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    FoodID = dr["FoodID"].ToString(),
                    Size = dr["Size"].ToString(),
                    Discount = Convert.ToDouble(dr["Discount"]),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    Dish = dish
                };

                list.Add(o);
            }
            return list;
        }

        static public List<Contact> GetAllContact()
        {
            List<Contact> list = new List<Contact>();
            SqlConnection connection = GetConnection();

            string query = @"SELECT [ContactName]
                                  ,[Email]
                                  ,[Phone]
                                  ,[ContactMessage]
                              FROM [dbo].[Contact]";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Contact o = new Contact
                {
                    ContactName = dr["ContactName"].ToString(),
                    Email = dr["Email"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    ContactMessage = dr["ContactMessage"].ToString()
                };
                list.Add(o);
            }
            return list;
        }


        static public bool GetAdminByUser(string user, string pass)
        {
            Account ad = new Account();
            SqlConnection connection = GetConnection();

            string query = @"SELECT [User]
                                      ,[Password]
                                  FROM [dbo].[Account]
                                  where [User] = @user and password = @pass";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@user", user));
            command.Parameters.Add(new SqlParameter("@pass", pass));
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            return false;
        }
        static public int InsertToDB(String querry)
        {

            int k = 0;
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand(querry, connection);
            try
            {

                k = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                k = 0;
            }
            return k;
        }
        static public int InsertFood(string foodId, string name, string ingredient, string fileAddress, int category)
        {
            int k = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Tạo SQL command
                string sqlCommand = "INSERT INTO Food (FoodID, FoodName, Ingredients, CategoryID, ImageString) VALUES (@FoodID, @FoodName, @Ingredients, @CategoryID, @ImageString)";
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    // Thêm các tham số
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    command.Parameters.AddWithValue("@FoodName", name);
                    command.Parameters.AddWithValue("@Ingredients", ingredient);
                    command.Parameters.AddWithValue("@CategoryID", category);
                    command.Parameters.AddWithValue("@ImageString", fileAddress);

                    // Thực hiện lệnh SQL
                    try
                    {

                        k = (int)command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        k = 0;
                    }
                }
            }
            return k;
        }
        static public int InsertDish(string foodId, string size, float price)
        {
            int k = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Tạo SQL command
                string sqlCommand = "INSERT INTO Dish (FoodID, Size, Price) VALUES (@FoodID, @Size, @Price)";
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    // Thêm các tham số
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    command.Parameters.AddWithValue("@Size", size);
                    command.Parameters.AddWithValue("@Price", price);

                    // Thực hiện lệnh SQL
                    try
                    {

                        k = (int)command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        k = 0;
                    }
                }
            }
            return k;

        }

        static public List<Food> GetAllPizza()
        {
            List<Food> list = new List<Food>();
            SqlConnection connection = GetConnection();

            string query = @"SELECT [FoodID]
                              ,[FoodName]
                              ,[Ingredients]
                              ,[CategoryID]
                              ,[ImageString]
                          FROM [dbo].[Food]";

            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Food o = new Food
                {
                    FoodID = dr["FoodId"].ToString(),
                    CategoryID = Convert.ToInt32(dr["CategoryID"]),
                    FoodName = dr["FoodName"].ToString(),
                    Ingredients = dr["Ingredients"].ToString(),
                    ImageString = dr["ImageString"].ToString(),
                };
                list.Add(o);
            }
            return list;
        }

        static public int DeleteFood(string foodID)
        {
            int k = 0;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    DeleteOrderDetails(connection, foodID);
                    DeleteDishRecords(connection, foodID);
                    DeleteFoodRecord(connection, foodID);
                    k = 1;
                }
                catch (Exception ex)
                {
                }
            }
            return k;
        }

        static public int DeleteOrderDetails(SqlConnection connection, string foodID)
        {
            int k = 0;
            using (SqlCommand command = new SqlCommand("DELETE FROM OrderDetail WHERE FoodID = @FoodID", connection))
            {
                command.Parameters.AddWithValue("@FoodID", foodID);
                k = (int)command.ExecuteNonQuery();
            }
            return k;
        }

        static public int DeleteDishRecords(SqlConnection connection, string foodID)
        {
            int k = 0;
            using (SqlCommand command = new SqlCommand("DELETE FROM Dish WHERE FoodID = @FoodID", connection))
            {
                command.Parameters.AddWithValue("@FoodID", foodID);
                k = (int)command.ExecuteNonQuery();
            }
            return k;
        }

        static public int DeleteFoodRecord(SqlConnection connection, string foodID)
        {
            int k = 0;
            using (SqlCommand command = new SqlCommand("DELETE FROM Food WHERE FoodID = @FoodID", connection))
            {
                command.Parameters.AddWithValue("@FoodID", foodID);
                k = (int)command.ExecuteNonQuery();
            }
            return k;
        }

        public static Food GetPizzaById(string idPizza)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = @"SELECT [FoodID]
                                      ,[FoodName]
                                      ,[Ingredients]
                                      ,[CategoryID]
                                      ,[ImageString]
                                  FROM [dbo].[Food] 
                                  WHERE FoodID = @FoodID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FoodID", idPizza);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            Food pizza = new Food
                            {
                                FoodID = dr["FoodID"].ToString(),
                                CategoryID = Convert.ToInt32(dr["CategoryID"]),
                                FoodName = dr["FoodName"].ToString(),
                                Ingredients = dr["Ingredients"].ToString(),
                                ImageString = dr["ImageString"].ToString(),
                            };
                            return pizza;
                        }
                        else
                        {
                            // Không tìm thấy Pizza với FoodID tương ứng
                            return null;
                        }
                    }
                }
            }
        }
        public static Dictionary<string, float> GetPizzaPriceById(string idPizza)
        {
            Dictionary<string, float> pizzaPrices = new Dictionary<string, float>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = @"SELECT [Size], [Price]
                          FROM [dbo].[Dish] 
                          WHERE FoodID = @FoodID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FoodID", idPizza);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            string size = dr["Size"].ToString();
                            float price = Convert.ToSingle(dr["Price"]);

                            // Thêm cặp key-value vào danh sách
                            pizzaPrices.Add(size, price);
                        }
                    }
                }
            }

            return pizzaPrices;
        }
        static public int UpdateFood(string foodId, string name, string ingredient, string fileAddress, int category)
        {
            int k = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Tạo SQL command
                string sqlCommand = "UPDATE Food SET FoodName = @FoodName, Ingredients = @Ingredients, CategoryID = @CategoryID, ImageString = @ImageString WHERE FoodID = @FoodID";
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    // Thêm các tham số
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    command.Parameters.AddWithValue("@FoodName", name);
                    command.Parameters.AddWithValue("@Ingredients", ingredient);
                    command.Parameters.AddWithValue("@CategoryID", category);
                    command.Parameters.AddWithValue("@ImageString", fileAddress);

                    // Thực hiện lệnh SQL
                    try
                    {
                        k = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        k = 0;
                    }

                }
            }
            return k;
        }

        static public int UpdateDish(string foodId, string size, float price)
        {
            int k = 0;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Tạo SQL command
                string sqlCommand = "UPDATE Dish SET Price = @Price WHERE FoodID = @FoodID AND Size = @Size";
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    // Thêm các tham số
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    command.Parameters.AddWithValue("@Size", size);
                    command.Parameters.AddWithValue("@Price", price);
                    // Thực hiện lệnh SQL
                    try
                    {
                        k = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        k = 0;
                    }
                }
            }
            return k;
        }


    }
}