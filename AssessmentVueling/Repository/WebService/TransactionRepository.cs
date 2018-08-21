using System.Collections.Generic;
using System.Net;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using Newtonsoft.Json;

namespace AssessmentVueling.Repository.WebService
{
    public class TransactionRepository : ITransactionRepository
    {
        public void Create(IEnumerable<Transaction> lstTransactions)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            IEnumerable<Transaction> lstTransactions = null;

            using (WebClient wc = new WebClient())
            {
                var urlTransactions = System.Configuration.ConfigurationManager.AppSettings["UrlTransactions"];
                var jsonResult = wc.DownloadString(urlTransactions);

                lstTransactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(jsonResult) as IEnumerable<Transaction>;
            }

            return lstTransactions;
        }
    }
}