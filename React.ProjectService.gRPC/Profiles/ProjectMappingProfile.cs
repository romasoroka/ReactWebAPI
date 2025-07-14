using AutoMapper;
using Shared.Services;
using React.ProjectService.Application.Dtos;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<ProjectRequest, ProjectDto>()
            .ForMember(dest => dest.TechnologyIds, opt => opt.MapFrom(src => src.TechnologyIds))
            .ForMember(dest => dest.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeMilliseconds(src.StartDate).DateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate > 0 ? DateTimeOffset.FromUnixTimeMilliseconds(src.EndDate).DateTime : (DateTime?)null));

        CreateMap<ProjectDto, ProjectRequest>()
            .ForMember(dest => dest.TechnologyIds, opt => opt.MapFrom(src => src.TechnologyIds))
            .ForMember(dest => dest.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription ?? ""))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => new DateTimeOffset(src.StartDate).ToUnixTimeMilliseconds()))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue ? new DateTimeOffset(src.EndDate.Value).ToUnixTimeMilliseconds() : 0));

        CreateMap<CredentialRequest, CredentialDto>().ReverseMap();

        CreateMap<ProjectRequest, ProjectShortDto>()
            .ForMember(dest => dest.TechnologyIds,
                opt => opt.MapFrom(src => src.TechnologyIds.ToList()));

        CreateMap<ProjectShortDto, ProjectRequest>()
            .ForMember(dest => dest.TechnologyIds,
                opt => opt.MapFrom(src => src.TechnologyIds))

            .ForMember(dest => dest.DetailedDescription, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.EndDate, opt => opt.Ignore())
            .ForMember(dest => dest.Budget, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Credentials, opt => opt.Ignore())
            .ForMember(dest => dest.TotalHoursLogged, opt => opt.Ignore())
            .ForMember(dest => dest.ReportCount, opt => opt.Ignore())
            .ForMember(dest => dest.ActiveEmployees, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeIds, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeNames, opt => opt.Ignore());
    }
}
