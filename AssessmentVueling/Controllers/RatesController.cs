using AssessmentVueling.Configuration;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AssessmentVueling.Controllers
{
    public class RatesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                //get all rates for the repository
                var rates = DependencyResolver.Resolve<IRateManager>().GetAllRates();

                //convert to dto
                var ratesDto = MapperConfig.Instance._iMapper.Map<IEnumerable<Rate>>(rates);

                return this.Ok(ratesDto);
            }
            catch (Exception)
            {
                return this.InternalServerError();
            }
        }
    }
}
