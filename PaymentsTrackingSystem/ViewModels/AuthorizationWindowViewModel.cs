using PaymentsTrackingSystem.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;
using System.Security.Cryptography;

namespace PaymentsTrackingSystem.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AuthorizationWindowViewModel
    {
        public AuthorizationWindowViewModel()
        { //TODO: clean test data below
            login = "alampiev_um_aza";
            password = "alampiev_um_aza";
            //end test data
        }
        public List<string> usersLogins { get => WorkWithDB.GetUsersLogins(); }
        public string login { get; set; }
        public string password { get; set; }

        private RelayCommand signInClick;
        public RelayCommand SignInClick 
        { get => signInClick ?? (signInClick = new RelayCommand(obj =>
            {
                var passwordHashed = WorkWithDB.GetUserPassword(login);
                if(getHash(this.password) == passwordHashed)
                {
                    //succesfuly authorize
                    new MainWindow(WorkWithDB.GetUser(login)).Show();
                    CloseAction();
                }
                else
                {
                    MessageBox.Show("Имя пользователя или пароль неверны!","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }));
        }

        private RelayCommand exitClick;
        public RelayCommand ExitClick => exitClick ?? (exitClick = new RelayCommand(obj =>
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?",
                "Выход",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }));

        public Action CloseAction { get; set; }


        public string getHash(string text)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha256.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToLower();
            }
        }
    }
}
