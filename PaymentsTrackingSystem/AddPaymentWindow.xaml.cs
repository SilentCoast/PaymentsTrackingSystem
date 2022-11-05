using PaymentsTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PaymentsTrackingSystem
{
    /// <summary>
    /// Interaction logic for AddPaymentWindow.xaml
    /// </summary>
    public partial class AddPaymentWindow : Window
    {
        public AddPaymentWindowViewModel addPaymentWindowViewModel{get;set;}
        public AddPaymentWindow()
        {
            InitializeComponent();
            addPaymentWindowViewModel = new AddPaymentWindowViewModel(this);
            DataContext = addPaymentWindowViewModel;
        }
    }
}
