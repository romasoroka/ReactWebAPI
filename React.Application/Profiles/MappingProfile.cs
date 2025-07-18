﻿using AutoMapper;
using React.Application.Dtos;
using React.Domain.Entities;

namespace React.Application.Profiles;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Employee <-> EmployeeDto
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.ProjectIds, opt => opt.MapFrom(src => src.Projects.Select(p => p.Id).ToList()))
            .ForMember(dest => dest.WorkSessions, opt => opt.MapFrom(src => src.WorkSessions))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(t => t.Name).ToList()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl ?? string.Empty));
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Projects, opt => opt.Ignore())
            .ForMember(dest => dest.WorkSessions, opt => opt.Ignore())
            .ForMember(dest => dest.Skills, opt => opt.Ignore()) // Skills обробляються в контролері
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl ?? string.Empty));

        // Project <-> ProjectDto
        CreateMap<ProjectDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.EmployeeIds.Select(id => new Employee { Id = id }).ToList()))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.Technologies.Select(t => new Technology { Name = t }).ToList()))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials));
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.EmployeeIds, opt => opt.MapFrom(src => src.Employees.Select(e => e.Id).ToList()))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.Technologies.Select(t => t.Name).ToList()))
            .ForMember(dest => dest.EmployeeNames, opt => opt.MapFrom(src => src.Employees.Select(e => e.FullName).ToList()))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials));

        // WorkSession <-> WorkSessionDto
        CreateMap<WorkSessionDto, WorkSession>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<WorkSession, WorkSessionDto>();

        // Credential <-> CredentialDto
        CreateMap<CredentialDto, Credential>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Projects, opt => opt.Ignore());
        CreateMap<Credential, CredentialDto>();

        CreateMap<Technology, TechnologyDto>().ReverseMap();


        // Technology <-> TechnologyDto
        CreateMap<Technology, TechnologyDto>()
            .ForMember(dest => dest.ProjectIds, opt => opt.MapFrom(src => src.Projects.Select(p => p.Id).ToList()))
            .ForMember(dest => dest.EmployeeIds, opt => opt.MapFrom(src => src.Employees.Select(e => e.Id).ToList()));

        CreateMap<TechnologyDto, Technology>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Projects, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

    }
}
