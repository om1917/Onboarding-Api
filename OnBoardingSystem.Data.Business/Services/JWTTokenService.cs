//-----------------------------------------------------------------------
// <copyright file="JWTTokenService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace OnBoardingSystem.Data.Business.Services
{
    using System;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Linq.Dynamic.Core.Tokenizer;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using AutoMapper;
    using Azure;
    using Azure.Core;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json.Linq;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;

    /// <inheritdoc />
    public class JWTTokenService
    {
        public static UserInfo user = new UserInfo();
        private readonly JWT _jwtSetting;
        public string RefreshToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private const string ConnectionString = "ConnectionStrings:OnBoardingSystem";

        public JWTTokenService(IOptions<JWT> options, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSetting = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc />
        public Data.Abstractions.Models.Token TokenGenerate(string username, string password, string check)
        {
            try
            {
                var token = new Data.Abstractions.Models.Token();
                var refreshToken = new Data.Abstractions.Models.Token();
                using (SqlConnection conn = new SqlConnection())
                {
                    var ConnectionStrings = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                    var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                    conn.ConnectionString = webConfigConnectionString;
                    conn.Open();
                    var command = conn.CreateCommand();
                    command.CommandText = "EXEC " + "usp_ConfigurationAPISecureKey";
                    string jwtTokenKey = "";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            reader.Read();
                            {
                                jwtTokenKey = reader["SecretKey"].ToString();
                            }
                        }
                    }
                    conn.Close();
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(jwtTokenKey);
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                         {
                      new Claim("userId", username),
                      new Claim("browserId", _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString()),
                      new Claim("systemIP", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()),
                         }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                    };
                    token.CreatedToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                    token.TokenExpires = DateTime.UtcNow.AddDays(1);
                    token.TokenCreated = DateTime.Now;
                    refreshToken = GenerateRefreshToken(token);
                    return token;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private Abstractions.Models.Token GenerateRefreshToken(Abstractions.Models.Token token)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            token.RefreshTokenExpires = DateTime.UtcNow.AddHours(1);
            token.RefreshTokenCreated = DateTime.Now;
            RefreshToken = token.RefreshToken;
            SetRefreshToken(token.RefreshToken);
            return token;
        }

        private void SetRefreshToken(string token)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(1),
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        public void RemoveToken()
        {
            RefreshToken = "";
        }

        public string GetPrincipalFromExpiredToken(string? tokens)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                var ConnectionStrings = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                conn.ConnectionString = webConfigConnectionString;
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC " + "usp_ConfigurationAPISecureKey";
                string jwtTokenKey = "";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        reader.Read();
                        {
                            jwtTokenKey = reader["SecretKey"].ToString();
                        }
                    }
                }
                conn.Close();                
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenKey)),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = new Data.Abstractions.Models.Token();
                var refreshToken = new Data.Abstractions.Models.Token();
                JwtSecurityTokenHandler tokenHandlers = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(jwtTokenKey);
                var stream = "[encoded jwt]";
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokens);
                var tokenS = jsonToken as JwtSecurityToken;
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                          {
                      new Claim(ClaimTypes.Name,tokenS.Claims.ToString()),

                          }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                };
                token.CreatedToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                token.TokenExpires = DateTime.UtcNow.AddMilliseconds(1);
                token.TokenCreated = DateTime.Now;
                refreshToken = GenerateRefreshToken(token);
                return token.CreatedToken;
            }
        }

    }
}
