using AutoMapper;
using React.ProjectService.Application.Dtos;
using React.ProjectService.Domain.Entities;


namespace React.ProjectService.Application.Profiles;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<Credential, CredentialDto>()
            .ReverseMap()
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore());

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.TechnologyIds, opt => opt.MapFrom(src => src.ProjectTechnologies.Select(pt => pt.TechnologyId).ToList()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => (double)src.Budget))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials))
            .ForMember(dest => dest.TotalHoursLogged, opt => opt.MapFrom(src => src.TotalHoursLogged))
            .ForMember(dest => dest.ReportCount, opt => opt.MapFrom(src => src.ReportCount))
            .ForMember(dest => dest.ActiveEmployees, opt => opt.MapFrom(src => src.ActiveEmployees))
            .ForMember(dest => dest.EmployeeIds, opt => opt.MapFrom(src => src.ProjectEmployees.Select(pe => pe.EmployeeId).ToList()));

        CreateMap<ProjectDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => (double)src.Budget))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.TotalHoursLogged, opt => opt.MapFrom(src => src.TotalHoursLogged))
            .ForMember(dest => dest.ReportCount, opt => opt.MapFrom(src => src.ReportCount))
            .ForMember(dest => dest.ActiveEmployees, opt => opt.MapFrom(src => src.ActiveEmployees))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials))
            .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectEmployees, opt => opt.Ignore());

        CreateMap<Project, ProjectShortDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.TechnologyIds, opt => opt.MapFrom(src => src.ProjectTechnologies.Select(pt => pt.TechnologyId).ToList()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<ProjectShortDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DetailedDescription, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.EndDate, opt => opt.Ignore())
            .ForMember(dest => dest.Budget, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.TotalHoursLogged, opt => opt.Ignore())
            .ForMember(dest => dest.ReportCount, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveEmployees, opt => opt.Ignore())
            .ForMember(dest => dest.Credentials, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectEmployees, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore());

    }
}

