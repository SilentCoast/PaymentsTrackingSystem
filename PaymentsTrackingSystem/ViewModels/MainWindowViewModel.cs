using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PaymentsTrackingSystem.DataBase;
using PropertyChanged;

namespace PaymentsTrackingSystem.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public User user { get; set; }
        public MainWindowViewModel(User user)
        {
            this.user = user;
            Payments = WorkWithDB.GetPayments(user.Id);
        }
        private List<Payment> payments;
        public List<Payment> Payments { get { return payments ; } set { payments = value; } }
        public string Category { get; set; }
        public List<string> categories => WorkWithDB.GetCategoryNames();
        private RelayCommand addPaymentClick;
        public RelayCommand AddPaymentClick => addPaymentClick ?? (addPaymentClick = new RelayCommand(obj =>
        {
            var addPaymentWindow = new AddPaymentWindow();
            if (addPaymentWindow.ShowDialog().Value)
            {
                var payment = addPaymentWindow.addPaymentWindowViewModel.payment;
                payment.UserId = user.Id;
                payment.User = user;
                
                WorkWithDB.AddPayment(payment);
                Payments = WorkWithDB.GetPayments(user.Id);//in order to update datagrid
                MessageBox.Show("Успешно добавлено");
            }
        }));

        private RelayCommand aplyFilters;
        public RelayCommand AplyFilters => aplyFilters ?? (aplyFilters = new RelayCommand(obj =>
        {
            Payments = WorkWithDB.GetPayments(user.Id, Category);
        }));

        private RelayCommand makeReport;
        public RelayCommand MakeReport => makeReport ?? (makeReport = new RelayCommand(obj =>
        {
            
        }));
        public Payment SelectedPayment { get; set; }//just selectedItem of a datagrid
        private RelayCommand deleteRow;
        public RelayCommand DeleteRow => deleteRow ?? (deleteRow = new RelayCommand(obj =>
        {
            var str = $"Будет удалён следующий элемент:\n\nПродукт: {SelectedPayment.Product.Name}\nкол-во: {SelectedPayment.Count}" +
            $" цена: {SelectedPayment.Price} стоимость: {SelectedPayment.Sum}\nкатегория: {SelectedPayment.Product.Category.Name}";
            MessageBoxResult result = MessageBox.Show(str,
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                WorkWithDB.RemovePayment(SelectedPayment);
                Payments = WorkWithDB.GetPayments(user.Id);//in order to update datagrid
            }
        }));
        


    }

}
