using ContactsManagement.Api.Models.Requests;
using ContactsManagement.Domain.Models.Responses;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactsRepository _contactsRepository;
    private readonly IContactsService _contactsService;

    public ContactsController(
        IContactsRepository contactsRepository,
        IContactsService contactsService)
    {
        _contactsRepository = contactsRepository;
        _contactsService = contactsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var contacts = await _contactsRepository.GetAllAsync();

        return Ok(contacts.Select(ContactsResponse.From!));
    }

    [HttpGet("by-id/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var contact = await _contactsRepository.GetByNavigationIdAsync(id);

        return contact is not null
            ? Ok(ContactsResponse.From(contact))
            : NotFound();
    }

    [HttpGet("by-ddd/{ddd}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByDddAsync([FromRoute] string ddd)
    {
        var contact = await _contactsRepository.GetByDddAsync(ddd);

        return contact is not null
            ? Ok(ContactsResponse.From(contact))
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync([FromBody] ContactsRequest request)
    {
        var contactId = await _contactsService.CreateAsync(request.ToDomainModel());

        return contactId != Guid.Empty
            ? Created()
            : BadRequest();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] ContactsRequest request)
    {
        var contact = await _contactsService.UpdateAsync(request.ToDomainModel(id));

        return contact is not null
            ? Ok(ContactsResponse.From(contact))
            : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        return await _contactsRepository.DeleteAsync(id)
            ? Ok()
            : NotFound();
    }
}
