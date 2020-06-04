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

namespace DesignStudioCoursework
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public int Worker_ID;

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWwindow = new MainWindow();

            using (var Content = new DesignStudioEntities())
            {
                try
                {
                    string login = LoginBox.Text;

                    var q = (from q1 in Content.Workers
                             where q1.Login == login
                             select q1).First();
                    if (q.Password == PasswordBox.Password)
                    {
                        Worker_ID = q.Worker_ID;

                        mainWwindow.Show();
                        
                    }
                    else MessageBox.Show("Логін користувача або пароль були введені невірно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Логін користувача або пароль були введені невірно");
                }
            }

            this.Close();
        }

    }
}
