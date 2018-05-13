using deepend.entity.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace deepend.data
{
    public interface IChequeData
    {
        int AddCheque(ChequeResponse cheque);
        IEnumerable<ChequeResponse> GetAllCheque();

        ChequeResponse FindCheque(int Id);
    }
}
