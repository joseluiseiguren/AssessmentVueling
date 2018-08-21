using AssessmentVueling.Models;
using System.Collections.Generic;

namespace AssessmentVueling.Interfaces
{
    public interface IRateRepository
    {
        IEnumerable<Rate> GetAllRates();

        void Create(IEnumerable<Rate> lstRates);
    }
}
