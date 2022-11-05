using PaymentsTrackingSystem.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsTrackingSystem
{
    public static class WorkWithDB
    {
        public static Payments_DBEntities1 db = new Payments_DBEntities1();
        
        //without filters
        public static List<Payment> GetPayments(int userId)
        {
            return (from p in db.Payments where p.UserId == userId select p).ToList();
        }
        //with filters
        public static List<Payment> GetPayments(int userId, string categoryName)
        {
            return (from p in db.Payments where (p.UserId == userId) where (p.Product.Category.Name == categoryName) select p).ToList();
        }

        public static List<string> GetUsersLogins()
        {
            return (from p in db.Users select p.Login).ToList();
        }
        public static string GetUserPassword(string login)
        {
            return (from p in db.Users where p.Login == login select p.Password).FirstOrDefault();
        }
        public static User GetUser(string login)
        {
            return (from p in db.Users where p.Login == login select p).FirstOrDefault();
        }
        public static List<string> GetCategoryNames()
        {
            return (from p in db.Categories select p.Name).ToList();
        }
        public static List<string> GetProductsNames(string category)
        {
            return (from p in db.Products where p.Category.Name == category select p.Name).ToList();
        }
        public static Category GetCategory(string categoryName)
        {
            return (from p in db.Categories where p.Name == categoryName select p).FirstOrDefault();
        }
        public static Product GetProduct(string productName)
        {
            return (from p in db.Products where p.Name == productName select p).FirstOrDefault();
        }
        public static void AddPayment(Payment payment)
        {
            db.Payments.Add(payment);
            db.SaveChanges();
        }
        public static void RemovePayment(Payment payment)
        {
            db.Payments.Remove(payment);
            db.SaveChanges();
        }
    }
}
