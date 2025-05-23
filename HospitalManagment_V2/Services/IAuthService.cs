﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HospitalManagment_V2.JwtSetting;

namespace HospitalManagment_V2.Services;

public interface IAuthService
{
    string GetToken(string username);
}

public class AuthService(IOptions<JwtSettings> settings) : IAuthService
{
    private readonly IOptions<JwtSettings> _settings = settings;
    private readonly JwtSecurityTokenHandler _handler = new();

    public string GetToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "cafe.uz",
            "cafe.uz",
            claims: [],
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: credentials
        );

        return _handler.WriteToken(token);
    }
}