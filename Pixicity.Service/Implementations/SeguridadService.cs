using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pixicity.Data;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Enums;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
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
        private IAppPrincipal _currentUser { get; }

        public SeguridadService(PixicityDbContext dbContext, IParametrosService parametrosService, IJwtService jwtService, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _parametrosService = parametrosService;
            _jwtService = jwtService;
            _currentUser = currentUser;
        }

        public List<Usuario> GetUsuarios(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Usuario
                    .AsNoTracking()
                    .Include(x => x.Estado.Pais)
                    .Include(x => x.Sessions.Where(x => x.Eliminado == false))
                    .Where(x => x.Eliminado == false && x.Baneado == false);

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
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

        public Usuario GetUsuarioInfoByUserName(string userName)
        {
            try
            {
                return _dbContext.Usuario.AsNoTracking()
                    .Include(x => x.Estado.Pais)
                    .FirstOrDefault(x => x.UserName.ToLower() == userName.Trim().ToLower() && x.Eliminado == false);
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

        public long UpdateUsuario(Usuario model)
        {
            try
            {
                Usuario usuario = GetUsuarioById(model.Id);

                usuario.Genero = model.Genero;
                usuario.EstadoId = model.EstadoId;
                usuario.FechaNacimiento = model.FechaNacimiento;
                usuario.Email = model.Email;

                usuario.FechaActualiza = DateTime.Now;
                usuario.UsuarioActualiza = _currentUser.UserName;

                _dbContext.Update(usuario);
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
                    Expires = DateTime.UtcNow.AddDays(15),
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

        public int SumarPuntosUsuario(long usuarioId, int puntos)
        {
            try
            {
                Usuario usuario = GetUsuarioById(usuarioId);

                if (usuario == null)
                    throw new Exception("Un error ha ocurrido tratando de sumar puntos para el usuario ya que no se ha encontrado mediante el id del usuario");

                if (usuario.Puntos <= 0)
                    usuario.Puntos = 0;

                usuario.Puntos += puntos;

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return usuario.Puntos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetLoggedUserByJwt()
        {
            try
            {
                return _dbContext.Usuario
                    .AsNoTracking()
                    .Include(x => x.Estado)
                    .FirstOrDefault(x => x.Id == _currentUser.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ChangeUserPassword(ChangePasswordUsuarioViewModel model)
        {
            try
            {
                var usuario = GetUsuarioById(_currentUser.Id);

                if (usuario == null)
                    throw new Exception($"Hubo un error al tratar de cambiar la contraseña del usuario ya que no existe el usuario con id {_currentUser.Id}");

                if (!PasswordHasher.ValidatePassword(model.CurrentPassword, usuario.Password))
                    throw new Exception("La contraseña no coincide con la contraseña actual");

                usuario.Password = PasswordHasher.HashPassword(model.NewPassword);

                _dbContext.Update(usuario);
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Session> GetSesiones(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var query = _dbContext.Session
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .AsQueryable();

                totalCount = query.Count();

                return query
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Session GetSessionById(long id)
        {
            try
            {
                return _dbContext.Session.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Session GetSessionByToken(string token)
        {
            try
            {
                return _dbContext.Session.FirstOrDefault(x => x.Token.Trim().ToLower() == token.Trim().ToLower());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DateTime UpdateSessionActivoDate(Session model)
        {
            try
            {
                model.Activo = DateTime.Now;

                _dbContext.Update(model);
                _dbContext.SaveChanges();

                return model.Activo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveUserSession(Session model)
        {
            try
            {
                _dbContext.Session.Add(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteSession(long id)
        {
            try
            {
                Session session = GetSessionById(id);

                if (session == null)
                    throw new Exception($"No se ha encontrado una sesión con el id {id}");

                session.UsuarioElimina = _currentUser.UserName;
                session.FechaElimina = DateTime.Now;
                session.Eliminado = true;

                _dbContext.Update(session);
                _dbContext.SaveChanges();

                return session.Eliminado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
