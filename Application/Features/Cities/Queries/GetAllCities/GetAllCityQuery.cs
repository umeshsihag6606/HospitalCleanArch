using Application.Interfaces.UnitofworkRepositories;
using AutoMapper;
using Domain.Entities.Cities;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Queries.GetAllCities
{
   
        public class GetAllCityQuery : IRequest<Result<List<GetAllCityDto>>>;
       

        internal class GetAllCityQueryHandler : IRequestHandler<GetAllCityQuery, Result<List<GetAllCityDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllCityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<GetAllCityDto>>> Handle(GetAllCityQuery request, CancellationToken cancellationToken)
            {
            var cities = await _unitOfWork.Repositary<City>().GetAllAsync();
                var cityDtos = _mapper.Map<List<GetAllCityDto>>(cities);
                return Result<List<GetAllCityDto>>.Success(cityDtos, "Cities retrieved successfully!");
            }
        }
}

