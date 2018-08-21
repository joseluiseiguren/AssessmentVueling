using AssessmentVueling.Models;
using System.Collections.Generic;

namespace AssessmentVueling.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();

        void Create(IEnumerable<Transaction> lstTransactions);
    }
}
