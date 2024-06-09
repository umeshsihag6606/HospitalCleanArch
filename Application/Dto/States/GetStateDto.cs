using Application.Comman.Mappings;
using Domain.Entities.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.States
{
    public class GetStateDto:IMapFrom<State>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CounatryId { get; set; }

    }
}
