using System.Collections.Generic;

namespace AssessmentVueling.Dto
{
    public class TransactionSumaryDto
    {
        public decimal TotalAmount { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}