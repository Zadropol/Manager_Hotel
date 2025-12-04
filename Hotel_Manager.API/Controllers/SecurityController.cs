using AutoMapper;
using FluentValidation;
using Hotel_Manager.Core.DTOs;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Enums;
using Hotel_Manager.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public SecurityController(ISecurityService securityService, IMapper mapper)
    {
        _securityService = securityService;
        _mapper = mapper;
    }

    // Método de prueba: requiere cualquier token válido
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        // Se puede acceder a los Claims del usuario autenticado
        var userLogin = User.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
        var userName = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        return Ok(new
        {
            Message = $"Conexión exitosa. Usuario autenticado: {userName} ({userLogin}), Rol: {userRole}",
            IsAuthorized = true
        });
    }

    // Endpoint de registro: Solo el rol 'Administrator' puede ejecutarlo.
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [HttpPost]
    public async Task<IActionResult> Post(SecurityDto securityDto)
    {
        // 1. Mapear DTO a Entidad
        var security = _mapper.Map<Security>(securityDto);

        // 2. Registrar el usuario (la contraseña se debe hashear aquí en una implementación real)
        await _securityService.RegisterUser(security);

        // 3. Mapear la Entidad resultante a DTO para la respuesta
        securityDto = _mapper.Map<SecurityDto>(security);

        return Ok(securityDto);
    }
}