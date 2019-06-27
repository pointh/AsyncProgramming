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
using System.Globalization;
using System.Threading;


namespace Spider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CultureInfo[] ci = { new CultureInfo("cs-CZ"), new CultureInfo("en") };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = ci[(sender as ComboBox).SelectedIndex];
            MessageBox.Show(Thread.CurrentThread.CurrentUICulture.ToString());
            //DirLabel.Content = FindResource("LabelRoot")?.ToString();
        }
    }
}
