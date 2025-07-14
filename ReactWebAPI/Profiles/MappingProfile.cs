using AutoMapper;
using React.Application.Dtos;
using React.Domain.Entities;
using Shared.Services;

namespace ReactWebAPI.Profiles;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // gRPC → DTO
        CreateMap<ProjectRequest, ProjectDto>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeMilliseconds(src.StartDate).DateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate > 0 ? DateTimeOffset.FromUnixTimeMilliseconds(src.EndDate).DateTime : (DateTime?)null))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.TechnologyIds.ToList()))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials.ToList()));

        CreateMap<CredentialRequest, CredentialDto>();

        // DTO → gRPC
        CreateMap<ProjectDto, ProjectRequest>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => new DateTimeOffset(src.StartDate).ToUnixTimeMilliseconds()))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue ? new DateTimeOffset(src.EndDate.Value).ToUnixTimeMilliseconds() : 0))
            .ForMember(dest => dest.TechnologyIds, opt => opt.MapFrom(src => src.Technologies))
            .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Credentials));

        CreateMap<CredentialDto, CredentialRequest>();

        // Список проєктів
        CreateMap<ProjectListRequest, List<ProjectDto>>()
            .ConvertUsing((src, dest, context) =>
            src.Projects.Select(p => context.Mapper.Map<ProjectDto>(p)).ToList());

        CreateMap<List<ProjectDto>, ProjectListRequest>()
            .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src));


    }
}