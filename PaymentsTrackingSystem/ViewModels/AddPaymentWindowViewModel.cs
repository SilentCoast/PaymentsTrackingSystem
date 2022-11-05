using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PaymentsTrackingSystem.DataBase;
using PropertyChanged;

namespace PaymentsTrackingSystem.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AddPaymentWindowViewModel
    {
        AddPaymentWindow addPaymentWindow { get; set; }
        public AddPaymentWindowViewModel(AddPaymentWindow window)
        {
            this.addPaymentWindow = window;
        }
        public List<string> categories => WorkWithDB.GetCategoryNames();
        public List<string> products => WorkWithDB.GetProductsNames(category);
        public string category { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public Payment payment { get; set; }

        private RelayCommand addPaymentClick;
        public RelayCommand AddPaymentClick => addPaymentClick ?? (addPaymentClick = new RelayCommand(obj =>
        {
            //MessageBox.Show($"quantity is {quantity}\n price is {price}\n category \"{category}\"\n product {product}");
            if (!IsValid(addPaymentWindow))
            {
                MessageBox.Show("Проверьте введённые данные!","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (category == null || category == "")
            {
                MessageBox.Show("Выберете категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(product == null || product == "")
            {
                MessageBox.Show("Выберете продукт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            payment = new Payment
            {
                Product = WorkWithDB.GetProduct(product)//in order to get id from db
            };
            payment.ProductId = payment.Product.Id;
            payment.Product.Category = WorkWithDB.GetCategory(category);//same
            payment.Product.CategoryID = payment.Product.Category.Id;
            payment.Count = quantity;
            payment.Price = price;
            payment.Sum = price * quantity;
            addPaymentWindow.DialogResult = true;
            addPaymentWindow.Close();
        }));
        private bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
            LogicalTreeHelper.GetChildren(obj)
            .OfType<DependencyObject>()
            .All(IsValid);
        }
    }
}
