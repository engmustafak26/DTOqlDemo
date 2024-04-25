using Demo.Domain;
using Demo.DTO.Organizations;
using Demo.Infrastructure;
using DTOql.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers.App
{
    [ApiController]
    [Route("[controller]")]
    public class TenantOrganizationController : DtoWithGraphBaseController<Organization, OrganizationListDto, OrganizationSearchDto, OrganizationListDto, OrganizationListDto>
    {
        public TenantOrganizationController(IService<Organization> service) : base(service)
        {
        }
    }
}
