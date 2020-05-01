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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.NewProject
{
    public partial class InteriorTypePage : Page
    {
        private Action goBack;

        public InteriorTypePage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
        }

        private void kitchen_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Кухня", goBack);
            this.NavigationService.Navigate(p);
        }

        private void bedroom_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Спальня", goBack);
            this.NavigationService.Navigate(p);
        }

        private void livingroom_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Вітальня", goBack);
            this.NavigationService.Navigate(p);
        }

        private void bathroom_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Санвузол", goBack);
            this.NavigationService.Navigate(p);
        }

        private void workplace_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Кабінет", goBack);
            this.NavigationService.Navigate(p);
        }

        private void childroom_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Дитяча", goBack);
            this.NavigationService.Navigate(p);
        }

        private void wardrobe_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Гардероб", goBack);
            this.NavigationService.Navigate(p);
        }

        private void corridor_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage p = new NewProjectPage("Коридор", goBack);
            this.NavigationService.Navigate(p);
        }
    }
}
