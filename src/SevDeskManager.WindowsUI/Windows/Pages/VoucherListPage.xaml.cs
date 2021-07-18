using SERGO.SevDesk.API.Client;
using SERGO.SevDesk.API.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Diagnostics;
using System.Data;
using System.Windows.Data;

namespace SERGO.SevDeskManager.WindowsUI.Windows.Pages
{
    public partial class VoucherListPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Voucher> _vouchers = new ObservableCollection<Voucher>();

        private ObservableCollection<CostCentre> _costCentres = new ObservableCollection<CostCentre>();

        private SevDeskClient SevDeskClient { get; set; }

        public ObservableCollection<Voucher> Vouchers
        {
            get => _vouchers;
            set
            {
                _vouchers = value;
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

        public VoucherListPage()
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
                var voucherResponse = await this.SevDeskClient.GetVouchers();
                var costCentreResponse = await this.SevDeskClient.GetCostCentres();

                var voucherList = new ObservableCollection<Voucher>(voucherResponse.Objects.OrderBy(voucher => voucher.Id));
                var costCentreList = new ObservableCollection<CostCentre>(costCentreResponse.Objects.OrderBy(costCentre => costCentre.Number));

                e.Result = new Tuple<ObservableCollection<Voucher>, ObservableCollection<CostCentre>>(voucherList, costCentreList);
            }).Wait();
        }

        private void BackgroundLoaderWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var (vouchers, costCentres) = (Tuple<ObservableCollection<Voucher>, ObservableCollection<CostCentre>>)e.Result;

            this.Vouchers = vouchers;
            this.CostCentres = costCentres;
            
            VoucherList.Visibility = Visibility.Visible;
        }
        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCostCentre = (CostCentre)e.AddedItems[0];

            var comboBox = (ComboBox)sender;
            var currentInvoice = (Voucher)((DataGridRow)VoucherList.ContainerFromElement(comboBox))?.Item;

            Task.Run(async () =>
            {
                if (selectedCostCentre != null && currentInvoice != null)
                {
                    await SevDeskClient.UpdateCostCentreForVoucher(currentInvoice.Id, selectedCostCentre.Id);
                }
            }).Wait();
        }

        private void Link__OpenInSevDesk(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Voucher currentVoucher)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = SevDeskClient.SevDeskVoucherDetailUrl + currentVoucher.Id,
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