using AutoMapper;
using TaskManager.Api.Common;
using TaskManager.Api.Contracts.v1.Requests;
using TaskManager.Api.Contracts.v1.Responses;
using TaskManager.Api.Domain;
using TaskManager.Api.Entities;

namespace TaskManager.Api.Helpers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        /* CreateTaskRequest -> TaskDomain */
        CreateMap<CreateTaskRequest, TaskDomain>();
        /* UpdateTaskRequest -> TaskDomaim */
        CreateMap<UpdateTaskRequest, TaskDomain>();

        /* TaskDomain -> CreateTaskResponse */
        CreateMap<TaskDomain, CreateTaskResponse>();
        /* TaskDomain -> UpdateTaskResponse */
        CreateMap<TaskDomain, UpdateTaskResponse>();
        /* TaskDomain -> GetTaskResponse */
        CreateMap<TaskDomain, GetTaskResponse>();

        /* TaskDomain -> TaskEntity AND TaskEntity -> TaskDomain */
        CreateMap<TaskDomain, TaskEntity>().ReverseMap();

        /* AccountDomain -> AccountEntity */
        CreateMap<AccountDomain, AccountEntity>()
            .ForMember(dest =>
            dest.Roles,
            opt => opt.MapFrom(src => src.Roles.Select(x => new RoleEntity { Role=x })))
            .ReverseMap();

        /* AccountOperationsResult -> ChangeAccountDataResult */
        CreateMap<AccountOperationsResult, ChangeAccountDataResult>();
    }
}