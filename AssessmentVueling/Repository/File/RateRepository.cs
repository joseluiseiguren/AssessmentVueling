using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AssessmentVueling.Repository.File
{
    public class RateRepository : IRateRepository
    {
        private readonly string _fileName = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\" + ConfigurationManager.AppSettings["BackupRatesFile"];

        public void Create(IEnumerable<Rate> lstRates)
        {
            var jsonTransactions = JsonConvert.SerializeObject(lstRates);

            using (StreamWriter sw = new StreamWriter(_fileName, false, System.Text.Encoding.Default))
            {
                sw.Write(jsonTransactions);
            }
        }

        public IEnumerable<Rate> GetAllRates()
        {
            IEnumerable<Rate> lstRate = null;

            using (StreamReader sr = new StreamReader(_fileName, System.Text.Encoding.Default))
            {
                var jsonResult = sr.ReadToEnd();

                lstRate = JsonConvert.DeserializeObject<IEnumerable<Rate>>(jsonResult) as IEnumerable<Rate>;
            }

            return lstRate;
        }
    }
}