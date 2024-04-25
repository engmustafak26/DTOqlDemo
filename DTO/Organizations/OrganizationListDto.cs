using Demo.Domain;
using Demo.Infrastructure;
using Demo.Miselaneous;
using DTOql.Continuations;
using DTOql.Enums;
using DTOql.Interfaces;
using DTOql.Models;
using Microsoft.EntityFrameworkCore;
using EntityState = DTOql.Enums.EntityState;

namespace Demo.DTO.Organizations
{
    public class OrganizationListDto : IDynamicProperty, IEntityState
    {
        public OrganizationListDto()
        {
            (this as IDynamicProperty).RegisterLogic(typeof(OrganizationLogicValidation));

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public UserDto[] Users { get; set; }
        public EntityState EntityState { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[0];
        }
    }

    public class UserDto : IDynamicProperty, IEntityState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public string ApiKey { get; set; }


        public UserRolesDto[] UserRoles { get; set; }
        public UserProfileHintDto[] Hints { get; set; }
        public EntityState EntityState { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[0];
        }
    }

    public class UserProfileHintDto : IDynamicProperty, IEntityState
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public EntityState EntityState { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[0];
        }
    }

    public class UserRolesDto : IDynamicProperty, IEntityState
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }

        public EntityState EntityState { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[0];
        }
    }

    public class RoleDto : IDynamicProperty, IEntityState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EntityState EntityState { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[0];
        }
    }

    public class OrganizationLogicValidation : IDtoLogicExecuter<OrganizationListDto>
    {
        private ApplicationExecutionContext _executionContext;
        private DataContext _dataContext;

        public OrganizationLogicValidation(ApplicationExecutionContext executionContext, DataContext dataContext)
        {
            _executionContext = executionContext;
            _dataContext = dataContext;
        }

        public EntityState[] ApplyWhenStates => new EntityState[] { EntityState.Add };

        public async Task<DTOqlBaseResponseDto<object>> ExecuteAsync(OrganizationListDto dto)
        {
            if ((_executionContext.UserType == Domain.UserType.OrganizationAdmin
                    && (dto.EntityState != EntityState.Update || dto.Id != _executionContext.OrganizationId))
                    || _executionContext.UserType == Domain.UserType.NormalUser)
            {
                return new DTOqlBaseResponseDto<object>().Error("operation not allowed for you access level", 400);
            }


            return DTOqlBaseResponseDto<object>.Success(null);
        }
    }
}
