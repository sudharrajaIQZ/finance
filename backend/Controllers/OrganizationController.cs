using backend.Dto;
using backend.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/organization")]
    [ApiController]
    public class OrganizationController:ControllerBase
    {
        private readonly IOrganizationInterface _organizationInterface;

        public OrganizationController(IOrganizationInterface organizationInterface)
        {
            this._organizationInterface = organizationInterface;
        }
        [HttpGet]
        public async Task<IActionResult> getOrganization()
        {
            var getOrganisationList = await _organizationInterface.getOrganizationAsync();
            return Ok(getOrganisationList);
        }

        [HttpPost]
        public async Task<IActionResult> createOrganizations([FromBody] createOrganization createOrganization)
        {
            var addOrganization = await _organizationInterface.createOrganizationAsync(createOrganization);
            return StatusCode(addOrganization.Code,addOrganization);
        }
    }
}
