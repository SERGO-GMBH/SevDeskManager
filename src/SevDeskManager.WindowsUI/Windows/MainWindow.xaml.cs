using SERGO.SevDeskManager.WindowsUI.Modals;
using SERGO.SevDeskManager.WindowsUI.Windows.Pages;
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

namespace SERGO.SevDeskManager.WindowsUI.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(App.SevDeskApiKey))
            {
                var credentialsModal = new SevDeskApiCredentialsModal();
                credentialsModal.ShowDialog();
            }
        }

        private void MenuItemClick__SettingsSevDeskApiKey(object sender, RoutedEventArgs e)
        {
            var credentialsModal = new SevDeskApiCredentialsModal();
            credentialsModal.ShowDialog();
        }

        private void MenuItemClick__InvoiceCostCentre(object sender, RoutedEventArgs e)
        {
            this.OpenInvoiceCostCentre();
        }

        private void MenuItemClick__VoucherCostCentre(object sender, RoutedEventArgs e)
        {
            this.OpenVoucherCostCentre();
        }

        private void MenuItemClick__SettingsExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenVoucherCostCentre()
        {
            this.PageFrame.Navigate(new VoucherListPage());
        }

        private void OpenInvoiceCostCentre()
        {
            this.PageFrame.Navigate(new InvoiceListPage());
        }
    }
}
