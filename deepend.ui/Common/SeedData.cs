using deepend.entity.Request;
using deepend.common;
using deepend.ui.Controllers;

namespace deepend.ui.Common
{
    public class SeedData
    {
        public void Seed()
        {
            ChequeController chequeController = new ChequeController(new HttpClientProvider());
            ChequeRequest cheque1 = new ChequeRequest
            {
                ChequeAmount = 125.50m,
                PersonName = "John Smith"
            };

            chequeController.AddSampleCheque(cheque1);

            ChequeRequest cheque2 = new ChequeRequest
            {
                ChequeAmount = 400.20m,
                PersonName = "Mary Brown"
            };

            chequeController.AddSampleCheque(cheque2);
        }
    }
}