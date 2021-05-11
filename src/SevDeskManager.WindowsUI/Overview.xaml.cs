using System.Windows;

using SERGO.SevDeskManager.WindowsUI.Modals;
using SERGO.SevDeskManager.WindowsUI.Windows;

namespace SERGO.SevDeskManager.WindowsUI
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();
        }

        private void ButtonClick_OpenCostCentreInvoice(object sender, RoutedEventArgs e)
        {
            this.LoadSevDeskCredetials();

            if (!string.IsNullOrEmpty(App.SevDeskApiKey))
            {
                var voucherWindow = new VoucherCostCentre();
                voucherWindow.ShowDialog();
            }
        }

        private void ButtonClick_OpenCostCentreVoucher(object sender, RoutedEventArgs e)
        {
            this.LoadSevDeskCredetials();

            if (!string.IsNullOrEmpty(App.SevDeskApiKey))
            {
                var invoiceWindow = new InvoiceCostCentre();
                invoiceWindow.ShowDialog();
            }
        }

        private void ButtonClick_Settings(object sender, RoutedEventArgs e)
        {
            this.LoadSevDeskCredetials(true);
        }

        private void LoadSevDeskCredetials(bool forceOpenModal = false)
        {
            // todo: implement logic to save the key encrypted on disk

            if (string.IsNullOrEmpty(App.SevDeskApiKey) || forceOpenModal)
            {
                SevDeskApiCredentialsModal credentialsModal = new SevDeskApiCredentialsModal();
                credentialsModal.ShowDialog();
            }
        }
    }
}
