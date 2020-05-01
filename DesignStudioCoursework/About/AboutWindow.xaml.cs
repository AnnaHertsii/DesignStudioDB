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

namespace DesignStudioCoursework.About
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutClicked(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Content = new AboutPage();
            AboutSection.FontWeight = FontWeights.Bold;
            WorksSection.FontWeight = FontWeights.Normal;
        }

        private void WorksClicked(object sender, MouseButtonEventArgs e)
        {
            MyFrame.Content = new WorksPage();
            WorksSection.FontWeight = FontWeights.Bold;
            AboutSection.FontWeight = FontWeights.Normal;
        }
    }
}
