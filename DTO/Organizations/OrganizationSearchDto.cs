using Demo.Domain;
using Demo.Miselaneous;
using DTOql.Interfaces;
using DTOql.Models;
using System.Threading;
using static DTOql.Utilities.Builder;

namespace Demo.DTO.Organizations
{
    public class OrganizationSearchDto : ISearch
    {
        public OrganizationSearchDto()
        {
            (this as IDynamicProperty).RegisterLogic(typeof(OrganizationSearchInterceptor));
        }
        public Dictionary<string, SearchProperty> SearchProperties { get; } = new Dictionary<string, SearchProperty>();

        public int Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public PagingWithSortModel PaginationWithSort { get; set; }

        public IEnumerable<DynamicProperty> GetOverrideProperties()
        {
            return new DynamicProperty[] {

                       new DynamicProperty
                       {
                           PropertyName=nameof(UserName),
                           Allias=nameof(UserName),
                           SourcePropertyPath= _<Organization>(x=>x.Users.Select(x=>x.Name)), // "Users[].Name"
                       },
                        new DynamicProperty
                       {
                           PropertyName=nameof(UserEmail),
                           Allias=nameof(UserEmail),
                           SourcePropertyPath= _<Organization>(x=>x.Users.Select(x=>x.Email)), // "Users[].Email",
                       }
           };
        }
    }

    public class OrganizationSearchInterceptor : IDtoSearchInterceptor<OrganizationSearchDto>
    {
        private ApplicationExecutionContext _executionContext;

        public OrganizationSearchInterceptor(ApplicationExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public async Task ExecuteAsync(OrganizationSearchDto dto)
        {
            if (_executionContext.UserType == UserType.WebsiteAdmin)
            {
                return;
            }


            dto.SearchProperties.Add("Id", new SearchProperty()
            {
                condition = "=",
                value = _executionContext.OrganizationId
            });
        }
    }
}


