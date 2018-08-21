using AssessmentVueling.Models;
using System.Collections.Generic;

namespace AssessmentVueling.Interfaces
{
    interface IRateManager
    {
        IEnumerable<Rate> GetAllRates();
    }
}
