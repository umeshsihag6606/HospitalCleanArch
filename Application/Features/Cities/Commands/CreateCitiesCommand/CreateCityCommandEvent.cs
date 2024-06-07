using Domain.Comman;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Commands.CreateCitiesCommand
{
    public class CreateCityCommandEvent:BaseEvent
    {
        public City City { get; set; }
        public CreateCityCommandEvent( City city)
        {
            City = city;
        }
    }

}
