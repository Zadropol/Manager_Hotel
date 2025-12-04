using FluentValidation;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISecurityService _securityService;

    public TokenController(IConfiguration configuration, ISecurityService securityService)
    {
        _configuration = configuration;
        _securityService = securityService;
    }

    [HttpPost]
    public async Task<IActionResult> Authentication([FromBody] UserLogin userLogin)
    {
        // 1. Validar las credenciales
        var validation = await IsValidUser(userLogin);

        if (validation.Item1)
        {
            // 2. Generar el token si las credenciales son válidas
            var token = GenerateToken(validation.Item2);
            return Ok(new { token });
        }

        // 3. Devolver 404 (NotFound) para credenciales inválidas
        return NotFound("Credenciales inválidas.");
    }

    private async Task<(bool, Security)> IsValidUser(UserLogin login)
    {
        var user = await _securityService.GetLoginByCredentials(login);
        // Retorna una tupla: (bool is_valid, Security user_data)
        return (user != null, user!);
    }