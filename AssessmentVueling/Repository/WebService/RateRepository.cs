using System.Collections.Generic;
using System.Configuration;
using System.Net;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using Newtonsoft.Json;

namespace AssessmentVueling.Repository.WebService
{
    public class RateRepository : IRateRepository
    {
        public void Create(IEnumerable<Rate> lstRates)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Rate> GetAllRates()
        {
            IEnumerable<Rate> lstRates = null;

            using (WebClient wc = new WebClient())
            {
                var urlRates = ConfigurationManager.AppSettings["UrlRates"];
                var jsonResult = wc.DownloadString(urlRates);

                lstRates = JsonConvert.DeserializeObject<IEnumerable<Rate>>(jsonResult) as IEnumerable<Rate>;
            }

            return lstRates;
        }
    }
}