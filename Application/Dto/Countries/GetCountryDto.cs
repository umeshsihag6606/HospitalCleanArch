using Application.Comman.Mappings;
using Domain.Entities.Counteries;

namespace Application.Dto.Countries
{
    public class GetCountryDto:IMapFrom<Countary>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
