﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pixicity.Data;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Enums;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Pixicity.Service.Implementations
{
    public class SeguridadService : ISeguridadService
    {
        private readonly PixicityDbContext _dbContext;
        private readonly IParametrosService _parametrosService;
        private readonly IJwtService _jwtService;

        public SeguridadService(PixicityDbContext dbContext, IParametrosService parametrosService, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _parametrosService = parametrosService;
            _jwtService = jwtService;
        }

        public long CountUsuarios()
        {
            try
            {
                return _dbContext.Usuario.Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioByUserName(string userName)
        {
            try
            {
                return _dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.UserName.ToLower().Equals(userName.Trim().ToLower()) && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioById(long id)
        {
            try
            {
                return _dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            try
            {
                return _dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Email.ToLower().Equals(email.Trim().ToLower()) && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long RegistrarUsuario(UsuarioViewModel model)
        {
            try
            {
                Usuario usuario = GetUsuarioByUserName(model.UserName);

                if (usuario != null)
                    throw new Exception($"Hubo un error al tratar de registrar un usuario, el 'userName' {model.UserName} ya se encuentra registrado en el sistema");

                usuario = GetUsuarioByEmail(model.Email);

                if (usuario != null)
                    throw new Exception($"Hubo un error al tratar de registrar un usuario, el 'email' {model.Email} ya se encuentra registrado en el sistema");

                usuario = new Usuario()
                {
                    Id = 0,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = PasswordHasher.HashPassword(model.Password),
                    FechaNacimiento = model.FechaNacimiento,
                    EstadoId = model.EstadoId
                };

                // Se hace una validación por si ponen en el frontend un género que no exista
                bool isGenero = Enum.IsDefined(typeof(Enums.GenerosEnum), model.Genero);

                if (isGenero == false)
                    usuario.Genero = Enums.GenerosEnum.Otros;
                else
                    usuario.Genero = (Enums.GenerosEnum)model.Genero;

                _parametrosService.CreateRangoIfNotExists("Administrador");
                _parametrosService.CreateRangoIfNotExists("Moderador");
                usuario.RangoId = _parametrosService.CreateRangoIfNotExists("Usuario");

                _dbContext.Usuario.Add(usuario);
                _dbContext.SaveChanges();

                return usuario.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario LoginUsuario(UsuarioViewModel model)
        {
            try
            {
                Usuario usuario = _dbContext.Usuario
                    .AsNoTracking()
                    .Include(x => x.Rango)
                    .FirstOrDefault(x => x.UserName.ToLower().Equals(model.UserName.Trim().ToLower()));

                if (usuario == null)
                    return null;

                return PasswordHasher.ValidatePassword(model.Password, usuario.Password) ? usuario : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GenerarJWT(Usuario model)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, model.UserName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtService.GetHashKeyJwt()), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
