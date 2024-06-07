using Application.Comman.Mappings;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Queries.GetAllCities
{
    public class GetAllCityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
