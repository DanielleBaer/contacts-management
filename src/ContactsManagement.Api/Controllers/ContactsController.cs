using ContactsManagement.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactsRepository _contactsRepository;
    public ContactsController(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var contacts =_contactsRepository.GetAllAsync();

        if (contacts.Any())
        {
            return Ok(contacts);
        }

        return Ok(Empty);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }
}
