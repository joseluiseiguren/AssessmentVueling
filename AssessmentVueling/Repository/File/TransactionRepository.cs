using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using Newtonsoft.Json;

namespace AssessmentVueling.Repository.File
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _fileName = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\" + ConfigurationManager.AppSettings["BackupTransactionsFile"];

        public void Create(IEnumerable<Transaction> lstTransactions)
        {
            var jsonTransactions = JsonConvert.SerializeObject(lstTransactions);

            using (StreamWriter sw = new StreamWriter(_fileName, false, System.Text.Encoding.Default))
            {
                sw.Write(jsonTransactions);
            }
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            IEnumerable<Transaction> lstTransactions = null;

            using (StreamReader sr = new StreamReader(_fileName, System.Text.Encoding.Default))
            {
                var jsonResult = sr.ReadToEnd();

                lstTransactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(jsonResult) as IEnumerable<Transaction>;
            }

            return lstTransactions;
        }
    }
}