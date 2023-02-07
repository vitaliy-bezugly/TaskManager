using AutoMapper;
using TaskManager.Refactored.Common;
using TaskManager.Refactored.Contracts.v1.Requests;
using TaskManager.Refactored.Contracts.v1.Responses;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Helpers;

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