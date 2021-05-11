using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SERGO.SevDeskManager.WindowsUI.Modals
{
    /// <summary>
    /// Interaktionslogik für SevDeskApiCredentialsModal.xaml
    /// </summary>
    public partial class SevDeskApiCredentialsModal : Window
    {
        public SevDeskApiCredentialsModal()
        {
            InitializeComponent();

            // set API key to box, if given
            this.TextBoxSevDeskApiKey.Text = App.SevDeskApiKey;
        }

        private void ButtonClick_SaveApiKey(object sender, RoutedEventArgs e)
        {
            App.SevDeskApiKey = this.TextBoxSevDeskApiKey.Text;

            // TODO: implement a method to test the API key and connection.

            this.Close();
        }
    }
}
