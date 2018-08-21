using AssessmentVueling.Configuration;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssessmentVueling.Manager
{
    public class TransactionManager : ITransactionManager
    {
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return this.FetchTransactions();
        }

        public IEnumerable<Transaction> GetTransactionsByProduct(string productId)
        {
            //get all transactions
            var transactions = this.FetchTransactions();

            //filter by product id
            transactions = transactions.Where(p => p.Sku.Equals(productId)).ToList();

            if (transactions.Count() > 0)
            {
                //get all rates
                var rates = DependencyResolver.Resolve<IRateRepository>().GetAllRates();

                //convert each transaction to Euro
                foreach (var item in transactions)
                {
                    item.Currency = "EUR";
                    item.Amount = this.ConvertToEuro(item.Amount, item.Currency, rates);
                }
            }            

            return transactions;
        }

        private IEnumerable<Transaction> FetchTransactions()
        {
            IEnumerable<Transaction> lstTransactions = null;
            bool failMainRepository = false;

            try
            {
                //try to get transactions from main repository
                lstTransactions = DependencyResolver.Resolve<ITransactionRepository>().GetAllTransactions();
            }
            catch (Exception ex)
            {
                failMainRepository = true;
                DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't get Transactions from main repository");
            }

            //fail to get transactions from the main repository
            if (failMainRepository)
            {
                try
                {
                    //get the transactions from the backup repository
                    lstTransactions = DependencyResolver.Resolve<ITransactionRepository>("backup").GetAllTransactions();
                }
                catch (Exception ex)
                {
                    DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't get Transactions from backup repository");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    //save the transactions in the backup repository
                    DependencyResolver.Resolve<ITransactionRepository>("backup").Create(lstTransactions);
                }
                catch (Exception ex)
                {
                    DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't save Transactions into backup repository");
                }
            }

            return lstTransactions;
        }        

        private decimal ConvertToEuro(decimal amountToConvert, string amountCurrency, IEnumerable<Rate> lstRates)
        {
            //direct convertion
            var rate = lstRates.Where(p => p.From.Equals(amountCurrency)).Where(p => p.To.Equals("EUR")).FirstOrDefault();
            if (rate == null)
            {
                
            }


            return amountToConvert * rate.Value;
        }
    }
}