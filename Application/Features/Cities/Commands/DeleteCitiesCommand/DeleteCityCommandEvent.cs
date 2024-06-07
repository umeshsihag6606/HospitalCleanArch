using Domain.Comman;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Commands.DeleteCitiesCommand
{
    public class DeleteCityCommandEvent:BaseEvent
    {
        public City City {  get; set; }
        public DeleteCityCommandEvent(City city)
        {
             City = city;
        }
    }
}
