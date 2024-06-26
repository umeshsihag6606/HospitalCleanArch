﻿using Application.Features.Cities.Command.CreateCity;
using Application.Features.Countries.Commands.CreateCountries;
using Application.Features.Employees.Command.CreateEmployees;
using Application.Features.States.Commands.CreateStates;
using AutoMapper;
using Domain.Entities.Cities;
using Domain.Entities.Counteries;
using Domain.Entities.Employees;
using Domain.Entities.States;
using System.Reflection;

namespace Application.Comman.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        // mapping  custom
        CreateMap<CreateCountryCommand, Countary>();
        CreateMap<CreateStateCommand, State>();
        CreateMap<CreateCityCommand,City>();
        CreateMap<CreateEmployeeCommand,Employee>();
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapfromType = typeof(IMapFrom<>);
        var mappingMethodName = nameof(IMapFrom<object>.Mapping);
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapfromType;
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
        var argumentTypes = new Type[] { typeof(Profile) };
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(mappingMethodName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);
                        interfaceMethodInfo.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
