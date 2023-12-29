using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaOrderWebApplication.Models
{
    public partial class User
    {
        public string CustomerName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public DateTime Dateofbirth { set; get; }
        public User() { }
        public User(String CustomerName,String Email, String Password, String Address,String Phone, DateTime Dateofbirth)
        {
            this.CustomerName = CustomerName;
            this.Email = Email;
            this.Password = Password;
            this.Address = Address;
            this.Phone = Phone;
            this.Dateofbirth = Dateofbirth;
        }
    }
}