using DesignStudioCoursework;
using DesignStudioCoursework.NewProject;
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

namespace DesignStudioCoursework
{
    public partial class NewProjectPage : Page
    {
        public List<Style> Styles { get; set; }
        String interiorType;
        private Action goBack;

        public NewProjectPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            BindComboStyle();
        }

        public NewProjectPage(String val, Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            interiorType = val;
            this.Loaded += new RoutedEventHandler(Page2_Loaded);
            BindComboStyle();
        }

        void Page2_Loaded(object sender, RoutedEventArgs e)
        {
           interior.Text = interiorType;
           BitmapImage image = null;
           if (interiorType == "Кухня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/kitchen.jpg", UriKind.Relative));               
            }
            else if (interiorType == "Спальня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/bed2.png", UriKind.Relative));
            }
            else if (interiorType == "Вітальня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/living.jpg", UriKind.Relative));
            }
            else if (interiorType == "Санвузол")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/toilet.jpg", UriKind.Relative));
            }
            else if (interiorType == "Кабінет")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/work.jpg", UriKind.Relative));
            }
            else if (interiorType == "Дитяча")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/child.jpg", UriKind.Relative));
            }
            else if (interiorType == "Гардероб")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/wardrobe.jpg", UriKind.Relative));
            }
            else if (interiorType == "Коридор")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/corridor.png", UriKind.Relative));
            }
            projectImage.Source = image;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void interior_Click(object sender, RoutedEventArgs e)
        {
            InteriorTypePage interior = new InteriorTypePage(goBack);
            this.NavigationService.Navigate(interior);
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow orders = new OrdersWindow(order, employee);
            orders.Show();
        }

        private void employee_Click(object sender, RoutedEventArgs e)
        {
            InteriorTypePage interior = new InteriorTypePage(goBack);
            this.NavigationService.Navigate(interior);
        }

        private void BindComboStyle()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Style.ToList();
            Styles = items;
            DataContext = Styles;
        }
    }
}
