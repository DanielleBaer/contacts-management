﻿using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Authenticate user and generate token.
    /// </summary>
    /// <param name="user">User credentials.</param>
    /// <returns>Authentication token.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto user)
    {
        var token = await _tokenService.GetToken(user);

        if (!string.IsNullOrWhiteSpace(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }
}
