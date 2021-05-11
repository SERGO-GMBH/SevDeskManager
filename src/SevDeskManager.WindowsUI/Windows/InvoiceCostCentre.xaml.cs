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
    public partial class InvoiceCostCentre : Window
    {
        public InvoiceCostCentre()
        {
            this.SevDeskClient = new SevDeskClient(App.SevDeskApiKey);

            Task.Run(async () =>
            {
                var costCentreTask = await this.SevDeskClient.GetCostCentres();
                var invoiceTask = await this.SevDeskClient.GetInvoices();

                this.CostCentres = new ObservableCollection<CostCentre>(costCentreTask.Objects.OrderBy(costCentre => costCentre.Number));
                this.Invoices = new ObservableCollection<Invoice>(invoiceTask.Objects.OrderBy(invoice => invoice.Id));
            }).Wait();

            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<Invoice> Invoices { get; set; }

        public ObservableCollection<CostCentre> CostCentres { get; set; }

        private SevDeskClient SevDeskClient { get; set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCostCentre = (CostCentre)e.AddedItems[0];

            ComboBox comboBox = (ComboBox)sender;
            var currentInvoice = (Invoice)((DataGridRow)InvoiceList.ContainerFromElement(comboBox)).Item;

            Task.Run(async () =>
            {
                await SevDeskClient.UpdateCostCentreForInvoice(currentInvoice.Id, selectedCostCentre.Id);
            }).Wait();
        }
    }
}
