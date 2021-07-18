using SERGO.SevDesk.API.Client;
using SERGO.SevDesk.API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SERGO.SevDeskManager.WindowsUI.Windows.Pages
{
    public partial class InvoiceListPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Invoice> _invoices = new ObservableCollection<Invoice>();

        private ObservableCollection<CostCentre> _costCentres = new ObservableCollection<CostCentre>();
        
        private SevDeskClient SevDeskClient { get; set; }
        
        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CostCentre> CostCentres
        {
            get => _costCentres;
            set
            {
                _costCentres = value;
                OnPropertyChanged();
            }
        }
        
        public InvoiceListPage()
        {
            DataContext = this;

            this.SevDeskClient = new SevDeskClient(App.SevDeskApiKey);
            
            InitializeComponent();
            
            var backgroundLoaderWorker = new BackgroundWorker();
            backgroundLoaderWorker.DoWork += BackgroundLoaderWorker;
            backgroundLoaderWorker.RunWorkerCompleted += BackgroundLoaderWorkerCompleted;
            backgroundLoaderWorker.RunWorkerAsync();
        }
        
        private void BackgroundLoaderWorker(object sender, DoWorkEventArgs e)
        {
            Task.Run(async () =>
            {
                var invoiceResponse = await this.SevDeskClient.GetInvoices();
                var costCentreResponse = await this.SevDeskClient.GetCostCentres();

                var invoiceList = new ObservableCollection<Invoice>(invoiceResponse.Objects.OrderBy(invoice => invoice.Id));
                var costCentreList = new ObservableCollection<CostCentre>(costCentreResponse.Objects.OrderBy(costCentre => costCentre.Number));

                e.Result = new Tuple<ObservableCollection<Invoice>, ObservableCollection<CostCentre>>(invoiceList, costCentreList);
            }).Wait();
        }

        private void BackgroundLoaderWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var (invoices, costCentres) = (Tuple<ObservableCollection<Invoice>, ObservableCollection<CostCentre>>)e.Result;

            this.Invoices = invoices;
            this.CostCentres = costCentres;

            InvoiceList.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCostCentre = (CostCentre)e.AddedItems[0];

            var comboBox = (ComboBox)sender;
            var currentInvoice = (Invoice)((DataGridRow)InvoiceList.ContainerFromElement(comboBox))?.Item;

            Task.Run(async () =>
            {
                if (currentInvoice != null && selectedCostCentre != null)
                {
                    await SevDeskClient.UpdateCostCentreForInvoice(currentInvoice.Id, selectedCostCentre.Id);
                }
            }).Wait();
        }

        private void Link__OpenInSevDesk(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Invoice currentVoucher)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = SevDeskClient.SevDeskInvoiceDetailUrl + currentVoucher.Id,
                    UseShellExecute = true
                });
            }
        }
        
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
