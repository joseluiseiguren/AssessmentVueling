using AssessmentVueling.Dto;
using AssessmentVueling.Models;
using AutoMapper;

namespace AssessmentVueling.Configuration
{
    public class MapperConfig
    {
        private static readonly MapperConfig instance = new MapperConfig();
        public readonly IMapper _iMapper;

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static MapperConfig()
        {
        }

        private MapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // mapping for Business: Transaction -> DTO: TransactionDto
                cfg.CreateMap<Transaction, TransactionDto>();

                // mapping for Business: Rate -> DTO: RateDto
                cfg.CreateMap<Rate, RateDto>();                
            });

            _iMapper = config.CreateMapper();
        }


        public static MapperConfig Instance
        {
            get
            {
                return instance;
            }
        }
    }
}