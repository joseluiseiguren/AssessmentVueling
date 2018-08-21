using AssessmentVueling.Configuration;
using AssessmentVueling.Interfaces;
using AssessmentVueling.Models;
using System;
using System.Collections.Generic;

namespace AssessmentVueling.Manager
{
    public class RateManager : IRateManager
    {
        public IEnumerable<Rate> GetAllRates()
        {
            IEnumerable<Rate> lstRates = null;
            bool failMainRepository = false;

            try
            {
                //try to get rates from main repository
                lstRates = DependencyResolver.Resolve<IRateRepository>().GetAllRates();
            }
            catch (Exception ex)
            {
                failMainRepository = true;
                DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't get Rates from main repository");
            }

            //fail to get rates from the main repository
            if (failMainRepository)
            {
                try
                {
                    //get the rates from the backup repository
                    lstRates = DependencyResolver.Resolve<IRateRepository>("backup").GetAllRates();
                }
                catch (Exception ex)
                {
                    DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't get Rates from backup repository");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    //save the rates in the backup repository
                    DependencyResolver.Resolve<IRateRepository>("backup").Create(lstRates);
                }
                catch (Exception ex)
                {
                    DependencyResolver.Resolve<IAppLogger>().Error(ex, "Can't save Rates into backup repository");
                }
            }

            return lstRates;
        }
    }
}