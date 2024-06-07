using Domain.Comman;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Commands.UpdateCitiesCommand
{
    public class UpdateCityCommandEvent:BaseEvent
    {
        public City City { get; set; }
        public UpdateCityCommandEvent(City city)
        {
            City = city;
        }
    }
}
