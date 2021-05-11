namespace SERGO.SevDesk.API.Client
{
    using RestSharp;

    using SevDesk.API.Models;
    using System.Threading.Tasks;

    public class SevDeskClient
    {
        private const string SevDeskApiUrl = "https://my.sevdesk.de/api/v1/";

        private RestClient SevDeskApiClient { get; set; }

        public SevDeskClient(string apiToken)
        {
            this.SevDeskApiClient = new RestClient(SevDeskApiUrl);
            this.SevDeskApiClient.AddDefaultQueryParameter("token", apiToken);
        }

        public async Task<RequestResponseList<Invoice>> GetInvoices()
        {
            var request = new RestRequest("Invoice", DataFormat.Json);

            var response = await this.SevDeskApiClient.GetAsync<RequestResponseList<Invoice>>(request);

            return response;
        }
        public async Task<RequestResponseList<Voucher>> GetVouchers()
        {
            var request = new RestRequest("Voucher", DataFormat.Json);

            var response = await this.SevDeskApiClient.GetAsync<RequestResponseList<Voucher>>(request);

            return response;
        }

        public async Task<RequestResponseList<CostCentre>> GetCostCentres()
        {
            var request = new RestRequest("CostCentre", DataFormat.Json);

            var response = await this.SevDeskApiClient.GetAsync<RequestResponseList<CostCentre>>(request);

            return response;
        }

        public Task UpdateCostCentreForInvoice(string invoiceId, string costCentreId)
        {
            var request = new RestRequest("Invoice/" + invoiceId, DataFormat.Json);

            request.AddJsonBody(new
            {
                costCentre = new {
                    id = costCentreId,
                    objectName = "CostCentre",
                }
            });

            var response = this.SevDeskApiClient.PutAsync<RequestResponseList<Invoice>>(request);

            return response;
        }

        public Task UpdateCostCentreForVoucher(string invoiceId, string costCentreId)
        {
            var request = new RestRequest("Voucher/" + invoiceId, DataFormat.Json);

            request.AddJsonBody(new
            {
                costCentre = new
                {
                    id = costCentreId,
                    objectName = "CostCentre",
                }
            });

            var response = this.SevDeskApiClient.PutAsync<RequestResponseList<Invoice>>(request);

            return response;
        }
    }
}
