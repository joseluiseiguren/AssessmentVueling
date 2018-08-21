using AssessmentVueling.Models;
using System.Collections.Generic;

namespace AssessmentVueling.Interfaces
{
    public interface ITransactionManager
    {
        IEnumerable<Transaction> GetAllTransactions();

        IEnumerable<Transaction> GetTransactionsByProduct(string productId);
    }
}
