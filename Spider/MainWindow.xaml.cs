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
using System.ComponentModel;

namespace Spider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Shows how national resources (en, cs-CZ) can be used
    /// dynamically in application.
    /// </summary>
    public partial class MainWindow : Window
    {
        CultureInfo[] ci = { new CultureInfo("cs-CZ"), new CultureInfo("en") };

        public MainWindow()
        {
            InitializeComponent();
            foreach (CultureInfo c in ci)
            {
                LanguageSelector.Items.Add(c.DisplayName);
                if (c.Equals(Thread.CurrentThread.CurrentUICulture))
                {
                    // Mark as selected according to the current CultureIUCulture
                    // Can be tested by changing the App.xaml.cs CultureUICulture setup in App()
                    LanguageSelector.SelectedIndex = LanguageSelector.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// This method returns true if Selection is not done inside dropdown
        /// initialization period
        /// </summary>
        /// <param name="e">
        /// The event that invoked the SelectionChanged
        /// </param>
        /// <returns>
        /// True, if the event was due to the clicked change on user interface
        /// </returns>
        private bool LanguageSelector_InitializationIsFinished(SelectionChangedEventArgs e)
        {
            return e.AddedItems.Count == e.RemovedItems.Count;
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector_InitializationIsFinished(e))
            {
                Thread.CurrentThread.CurrentUICulture = ci[(sender as ComboBox).SelectedIndex];

                MainWindow mw = new MainWindow();
                mw.Top = this.Top; mw.Left = this.Left; // Show new window as overlaping the old one
                mw.Show();
                this.Close();
            }
        }
    }
}
