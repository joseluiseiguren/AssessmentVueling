using AssessmentVueling.Configuration;
using AssessmentVueling.Dto;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AssessmentVueling.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("api/Products/Transactions")]
        public IHttpActionResult Transactions()
        {
            try
            {
                //get all transactions for the repository
                var transactions = DependencyResolver.Resolve<ITransactionManager>().GetAllTransactions();

                //convert to dto
                var transactionsDto = MapperConfig.Instance._iMapper.Map<IEnumerable<TransactionDto>>(transactions);

                return this.Ok(transactionsDto);
            }
            catch (Exception)
            {
                return this.InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/Products/{id}/Transactions")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                //get transactions filtered for the repository
                var transactions = DependencyResolver.Resolve<ITransactionManager>().GetTransactionsByProduct(id);

                //convert to dto
                TransactionSumaryDto transactionsDto = new TransactionSumaryDto();
                transactionsDto.Transactions = MapperConfig.Instance._iMapper.Map<IEnumerable<TransactionDto>>(transactions);
                transactionsDto.TotalAmount = transactions.Sum(p => p.Amount);

                return this.Ok(transactionsDto);                
            }
            catch (Exception)
            {
                return this.InternalServerError();
            }
        }
    }
}
