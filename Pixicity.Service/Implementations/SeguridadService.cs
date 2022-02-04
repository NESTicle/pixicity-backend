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

        public long CommentsCountByUserId(long userId)
        {
            try
            {
                return _dbContext.Comentario.Count(x => x.UsuarioId == userId && x.Eliminado == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SeguidoresCountByUserId(long userId)
        {
            try
            {
                return _dbContext.UsuarioSeguidores.Count(x => x.SeguidoId == userId && x.Eliminado == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SiguiendoCountByUserId(long userId)
        {
            try
            {
                return _dbContext.UsuarioSeguidores.Count(x => x.SeguidorId == userId && x.Eliminado == false);
            }
            catch (Exception)
            {
                throw;
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

        public long GetOnlineUsersCount()
        {
            try
            {
                var query = _dbContext.Session.Where(x => x.Eliminado == false && x.Activo > DateTime.Now.AddMinutes(-15));
                return query.Count();
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
                DeleteAllSessionsByUsuarioId(model.UsuarioId);

                _dbContext.Session.Add(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long? DeleteAllSessionsByUsuarioId(long usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return 0;

                var sessions = _dbContext.Session.Where(x => x.UsuarioId == usuarioId).ToList();

                if (sessions?.Count > 0)
                {
                    foreach (var session in sessions)
                    {
                        session.Eliminado = true;
                        session.FechaElimina = DateTime.Now;
                    }

                    _dbContext.UpdateRange(sessions);
                    _dbContext.SaveChanges();
                }

                return sessions?.Count;
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

        public UsuarioInfoViewModel GetUsuarioInfo(string userName)
        {
            try
            {
                Usuario usuario = GetUsuarioByUserName(userName);

                if (usuario == null)
                    throw new Exception($"No se ha encontrado el usuario con el nombre de usuario {userName}");

                UsuarioInfoViewModel info = new UsuarioInfoViewModel
                {
                    Seguidores = _dbContext.UsuarioSeguidores.Count(x => x.SeguidoId == usuario.Id && x.Eliminado == false),
                    Posts = _dbContext.Post.Count(x => x.UsuarioId == usuario.Id && x.Eliminado == false),
                    Puntos = _dbContext.Usuario.FirstOrDefault(x => x.Id == usuario.Id && x.Eliminado == true)?.Puntos
                };

                return info;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SeguirUsuario(UsuarioSeguidores model)
        {
            try
            {
                UsuarioSeguidores usuarioSeguidor = null;

                if(model.SeguidorId == 0)
                {
                    Usuario usuario = GetUsuarioByUserName(model.UserName);

                    if (usuario == null)
                        throw new Exception($"No se ha encontrado un usuario con el userName {model.UserName}");

                    model.SeguidoId = usuario.Id;
                    usuarioSeguidor = _dbContext.UsuarioSeguidores.FirstOrDefault(x => x.SeguidoId == model.SeguidoId && x.SeguidorId == _currentUser.Id);
                }

                usuarioSeguidor = _dbContext.UsuarioSeguidores.FirstOrDefault(x => x.SeguidoId == model.SeguidoId && x.SeguidorId == _currentUser.Id);

                if (usuarioSeguidor != null)
                {
                    usuarioSeguidor.Eliminado = !usuarioSeguidor.Eliminado;

                    _dbContext.Update(usuarioSeguidor);
                    _dbContext.SaveChanges();

                    return usuarioSeguidor.Id;
                }
                else
                {
                    UsuarioSeguidores follow = new UsuarioSeguidores()
                    {
                        SeguidoId = model.SeguidoId,
                        SeguidorId = _currentUser.Id,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistra = _currentUser.UserName
                    };

                    _dbContext.UsuarioSeguidores.Add(follow);
                    _dbContext.SaveChanges();

                    return follow.Id;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsFollowingTheUser(string userName)
        {
            try
            {
                var follow = _dbContext.UsuarioSeguidores
                    .Include(x => x.Seguido)
                    .FirstOrDefault(x => x.Seguido.UserName == userName && x.SeguidorId == _currentUser.Id && x.Eliminado == false);

                return follow != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Usuario> GetFollowingUsersByUserId(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                if (string.IsNullOrEmpty(queryParameters.Query))
                    throw new Exception("El parámetro query es requerido para esta consulta");

                long userId = long.Parse(queryParameters.Query);

                var usuarios = _dbContext.UsuarioSeguidores
                    .AsNoTracking()
                    .Include(x => x.Seguido.Estado.Pais)
                    .Where(x => x.SeguidorId == userId && x.Eliminado == false)
                    .Select(x => x.Seguido);

                totalCount = usuarios.Count();

                return usuarios
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Usuario> GetFollowersByUserId(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                if (string.IsNullOrEmpty(queryParameters.Query))
                    throw new Exception("El parámetro query es requerido para esta consulta");

                long userId = long.Parse(queryParameters.Query);

                var usuarios = _dbContext.UsuarioSeguidores
                    .AsNoTracking()
                    .Include(x => x.Seguidor.Estado.Pais)
                    .Where(x => x.SeguidoId == userId && x.Eliminado == false)
                    .Select(x => x.Seguidor);

                totalCount = usuarios.Count();

                return usuarios
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsuarioPerfil GetCurrentPerfilInfo()
        {
            try
            {
                UsuarioPerfil usuarioPerfil = _dbContext.UsuarioPerfil.FirstOrDefault(x => x.UsuarioId == _currentUser.Id && x.Eliminado == false);
                return usuarioPerfil;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SavePerfilInfo(UsuarioPerfil model)
        {
            try
            {
                UsuarioPerfil usuarioPerfil = _dbContext.UsuarioPerfil.FirstOrDefault(x => x.UsuarioId == _currentUser.Id && x.Eliminado == false);

                if(usuarioPerfil == null)
                {
                    usuarioPerfil = model;
                    usuarioPerfil.UsuarioId = _currentUser.Id;
                    
                    _dbContext.UsuarioPerfil.Add(usuarioPerfil);
                }
                else
                {
                    usuarioPerfil.CompleteName = model.CompleteName;
                    usuarioPerfil.PersonalMessage = model.PersonalMessage;
                    usuarioPerfil.Website = model.Website;
                    usuarioPerfil.Facebook = model.Facebook;
                    usuarioPerfil.Twitter = model.Twitter;
                    usuarioPerfil.Tiktok = model.Tiktok;
                    usuarioPerfil.Youtube = model.Youtube;
                    usuarioPerfil.Like1 = model.Like1;
                    usuarioPerfil.Like2 = model.Like2;
                    usuarioPerfil.Like3 = model.Like3;
                    usuarioPerfil.Like4 = model.Like4;
                    usuarioPerfil.Like_All = model.Like_All;
                    usuarioPerfil.EstadoCivil = model.EstadoCivil;
                    usuarioPerfil.Hijos = model.Hijos;
                    usuarioPerfil.VivoCon = model.VivoCon;
                    usuarioPerfil.Altura = model.Altura;
                    usuarioPerfil.Peso = model.Peso;
                    usuarioPerfil.ColorCabello = model.ColorCabello;
                    usuarioPerfil.ColorOjos = model.ColorOjos;
                    usuarioPerfil.Complexion = model.Complexion;
                    usuarioPerfil.Dieta = model.Dieta;
                    usuarioPerfil.Tatuajes = model.Tatuajes;
                    usuarioPerfil.Piercings = model.Piercings;
                    usuarioPerfil.Fumo = model.Fumo;
                    usuarioPerfil.Alcohol = model.Alcohol;
                    usuarioPerfil.Estudios = model.Estudios;
                    usuarioPerfil.Profesion = model.Profesion;
                    usuarioPerfil.Empresa = model.Empresa;
                    usuarioPerfil.Sector = model.Sector;
                    usuarioPerfil.InteresesProfesionales = model.InteresesProfesionales;
                    usuarioPerfil.HabilidadesProfesionales = model.HabilidadesProfesionales;
                    usuarioPerfil.MisIntereses = model.MisIntereses;
                    usuarioPerfil.Hobbies = model.Hobbies;
                    usuarioPerfil.SeriesTV = model.SeriesTV;
                    usuarioPerfil.MusicaFavorita = model.MusicaFavorita;
                    usuarioPerfil.DeportesFavoritos = model.DeportesFavoritos;
                    usuarioPerfil.LibrosFavoritos = model.LibrosFavoritos;
                    usuarioPerfil.PeliculasFavoritas = model.PeliculasFavoritas;
                    usuarioPerfil.ComidaFavorita = model.ComidaFavorita;
                    usuarioPerfil.MisHeroesSon = model.MisHeroesSon;

                    _dbContext.Update(usuarioPerfil);
                }

                _dbContext.SaveChanges();

                return usuarioPerfil.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SocialMediaUsuarioPerfilViewModel GetSocialMediaByUsuarioId(long usuarioId)
        {
            try
            {
                UsuarioPerfil perfil = _dbContext.UsuarioPerfil.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Eliminado == false);

                if (perfil == null)
                    return null;

                return new SocialMediaUsuarioPerfilViewModel()
                {
                    Facebook = perfil.Facebook,
                    Twitter = perfil.Twitter,
                    Tiktok = perfil.Tiktok,
                    Youtube = perfil.Youtube
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
