using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using SERGO.SevDesk.API.Client;
using SERGO.SevDesk.API.Models;

namespace SERGO.SevDeskManager.WindowsUI.Windows
{
    /// <summary>
    /// Interaktionslogik für Voucher.xaml
    /// </summary>
    public partial class VoucherCostCentre : Window
    {
        public VoucherCostCentre()
        {
            this.SevDeskClient = new SevDeskClient(App.SevDeskApiKey);

            Task.Run(async () =>
            {
                var costCentreTask = await this.SevDeskClient.GetCostCentres();
                var voucherTask = await this.SevDeskClient.GetVouchers();

                this.CostCentres = new ObservableCollection<CostCentre>(costCentreTask.Objects.OrderBy(costCentre => costCentre.Number));
                this.Vouchers = new ObservableCollection<Voucher>(voucherTask.Objects.OrderBy(voucher => voucher.Id));
            }).Wait();

            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<Voucher> Vouchers { get; set; }

        public ObservableCollection<CostCentre> CostCentres { get; set; }

        private SevDeskClient SevDeskClient { get; set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCostCentre = (CostCentre)e.AddedItems[0];

            ComboBox comboBox = (ComboBox)sender;
            var currentInvoice = (Voucher)((DataGridRow)InvoiceList.ContainerFromElement(comboBox)).Item;

            Task.Run(async () =>
            {
                await SevDeskClient.UpdateCostCentreForVoucher(currentInvoice.Id, selectedCostCentre.Id);
            }).Wait();
        }
    }
}
