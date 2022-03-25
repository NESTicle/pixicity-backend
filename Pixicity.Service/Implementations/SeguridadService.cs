using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pixicity.Data;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.AppSettings;
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
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Service.Implementations
{
    public class SeguridadService : ISeguridadService
    {
        private readonly PixicityDbContext _dbContext;
        private readonly IParametrosService _parametrosService;
        private readonly IJwtService _jwtService;
        private readonly ILogsService _logsService;
        private readonly KeysAppSettingsViewModel _keys;
        private IAppPrincipal _currentUser { get; }

        public SeguridadService(PixicityDbContext dbContext, IParametrosService parametrosService, IJwtService jwtService, IAppPrincipal currentUser, ILogsService logsService, IOptions<KeysAppSettingsViewModel> keys)
        {
            _dbContext = dbContext;
            _parametrosService = parametrosService;
            _jwtService = jwtService;
            _currentUser = currentUser;
            _logsService = logsService;
            _keys = keys.Value;
        }

        public List<Usuario> GetUsuarios(QueryParamsHelper queryParameters, out long totalCount, bool isAdmin = false)
        {
            try
            {
                var posts = _dbContext.Usuario
                    .AsNoTracking()
                    .Include(x => x.Posts)
                    .Include(x => x.Comentarios)
                    .Include(x => x.Estado.Pais)
                    .Include(x => x.Rango)
                    .Include(x => x.Sessions.Where(x => x.Eliminado == false))
                    .AsQueryable();

                if (isAdmin == false)
                    posts = posts.Where(x => x.Eliminado == false && x.Baneado == false);

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
                return _dbContext.Usuario
                    .AsNoTracking()
                    .FirstOrDefault(x => x.UserName.ToLower().Equals(userName.Trim().ToLower()) && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario GetUsuarioWithRangoByUserName(string userName)
        {
            try
            {
                return _dbContext.Usuario
                    .AsNoTracking()
                    .Include(x => x.Rango)
                    .FirstOrDefault(x => x.UserName.ToLower().Equals(userName.Trim().ToLower()) && x.Eliminado == false);
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
                    .Include(x => x.UsuarioPerfil)
                    .Include(x => x.Rango)
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
                Configuracion configuracion = _dbContext.Configuracion.AsNoTracking().FirstOrDefault();

                if (configuracion != null && configuracion.DisableUserRegistration == true)
                    throw new Exception(!string.IsNullOrEmpty(configuracion.DisableUserRegistrationMessage) ? configuracion.DisableUserRegistrationMessage : "Se ha cerrado el registro de más usuarios en la comunidad");

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
                bool isGenero = Enum.IsDefined(typeof(GenerosEnum), model.Genero);

                if (isGenero == false)
                    usuario.Genero = GenerosEnum.Otros;
                else
                    usuario.Genero = (GenerosEnum)model.Genero;

                usuario.RangoId = _parametrosService.CreateRangoIfNotExists("Novato");

                _dbContext.Usuario.Add(usuario);
                _dbContext.SaveChanges();

                // Enviar correo para validar el usuario
                // TODO

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

        public Session GetSessionByUsuarioId(long usuarioId)
        {
            try
            {
                return _dbContext.Session
                    .Where(x => x.UsuarioId == usuarioId && x.Eliminado == false)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
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
                var count = _dbContext.Session.Where(x => x.Eliminado == false && x.Activo > DateTime.Now.AddMinutes(-15)).Count();

                var configuracion = _dbContext.Configuracion.FirstOrDefault();

                if (configuracion != null && count > configuracion.RecordOnlineUsers)
                {
                    configuracion.RecordOnlineUsers = count;
                    configuracion.RecordOnlineTime = DateTime.Now;

                    _dbContext.Update(configuracion);
                    _dbContext.SaveChanges();
                }

                return count;
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

        public long? DeleteAllSessionsByUsuarioId(long? usuarioId)
        {
            try
            {
                if (usuarioId == null || usuarioId <= 0)
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

        public int DeleteSessionByIP(string IP)
        {
            try
            {
                List<Session> sesiones = _dbContext.Session.Where(x => x.IP == IP).ToList();

                if (sesiones == null || sesiones.Count < 1)
                    return 0;

                foreach (var sesion in sesiones)
                {
                    sesion.UsuarioElimina = "SYSTEM";
                    sesion.FechaElimina = DateTime.Now;
                    sesion.Eliminado = true;
                }

                _dbContext.UpdateRange(sesiones);
                return _dbContext.SaveChanges();
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

                if (model.SeguidorId == 0)
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

                    _logsService.SaveMonitor(new Data.Models.Logs.Monitor()
                    {
                        UsuarioId = model.SeguidoId,
                        UsuarioQueHaceAccionId = _currentUser.Id,
                        Tipo = TipoMonitor.Seguir,
                        Mensaje = $"Te está siguiendo",
                    });

                    SaveActividadUsuario(new Actividad()
                    {
                        UsuarioId = _currentUser.Id,
                        ObjId1 = model.SeguidoId,
                        TipoActividad = TipoActividad.SiguiendoUsuario
                    });

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

        public List<Usuario> GetLastFollowersByUserId(long userId, out long totalCount)
        {
            try
            {
                if (userId <= 0)
                {
                    totalCount = 0;
                    return new List<Usuario>();
                }

                var usuarios = _dbContext.UsuarioSeguidores
                    .AsNoTracking()
                    .Include(x => x.Seguidor.Estado.Pais)
                    .Where(x => x.SeguidoId == userId && x.Eliminado == false);

                totalCount = usuarios.Count();

                var lastFollowers = usuarios.Take(15)
                    .Select(x => x.Seguidor)
                    .ToList();

                return lastFollowers;
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

        public UsuarioPerfil GetUsuarioPerfilByUsuarioId(long usuarioId)
        {
            try
            {
                UsuarioPerfil usuarioPerfil = _dbContext.UsuarioPerfil.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Eliminado == false);
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

                if (usuarioPerfil == null)
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

        public Usuario GetUsuarioByJWT(string jwt)
        {
            try
            {
                if (string.IsNullOrEmpty(jwt))
                    return null;

                string jwtUniqueName = _jwtService.GetUniqueName(jwt);

                if (string.IsNullOrEmpty(jwtUniqueName))
                    return null;

                return GetUsuarioByUserName(jwtUniqueName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SetUltimaDireccionIPUsuario(Usuario usuario, string IP)
        {
            try
            {
                usuario.UltimaConexion = DateTime.Now;
                usuario.UltimaIP = IP;

                _dbContext.Update(usuario);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long BanUser(Usuario model)
        {
            try
            {
                Usuario usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == model.Id && x.Eliminado == false);

                if (usuario == null)
                    throw new Exception($"Hubo un error al banear un usuario con id {model.Id}");

                usuario.Baneado = true;
                usuario.RazonBaneo = model.RazonBaneo;
                usuario.TiempoBaneado = model.TiempoBaneado;
                usuario.BaneadoPermanente = model.BaneadoPermanente;

                Session session = GetSessionByUsuarioId(model.Id);

                if (session != null)
                    DeleteSession(session.Id);

                Historial historial = new Historial()
                {
                    FechaRegistro = DateTime.Now,
                    IP = _currentUser.IP,
                    Tipo = TipoHistorial.Usuario,
                    TipoId = 0,
                    Razon = model.RazonBaneo,
                    UsuarioRegistra = _currentUser.UserName
                };

                if (model.BaneadoPermanente)
                {
                    Rango rango = _dbContext.Rango.FirstOrDefault(x => x.Eliminado == false && x.Nombre == "Baneado");

                    if (rango != null)
                        usuario.RangoId = rango.Id;

                    historial.Accion = "Baneo Permanente";
                }
                else
                {
                    historial.Accion = "Baneo Temporal";
                }

                _dbContext.Historial.Add(historial);
                _dbContext.SaveChanges();

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return usuario.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long UnbanUser(Usuario model)
        {
            try
            {
                Usuario usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == model.Id && x.Eliminado == false);

                if (usuario == null)
                    throw new Exception($"Hubo un error al desbanear un usuario con id {model.Id}");

                usuario.Baneado = false;
                usuario.TiempoBaneado = null;
                usuario.BaneadoPermanente = false;

                Session session = GetSessionByUsuarioId(model.Id);

                if (session != null)
                    DeleteSession(session.Id);

                _dbContext.Historial.Add(new Historial()
                {
                    FechaRegistro = DateTime.Now,
                    IP = _currentUser.IP,
                    Tipo = TipoHistorial.Usuario,
                    TipoId = 0,
                    Razon = model.RazonBaneo,
                    Accion = "El usuario ha sido desbaneado",
                    UsuarioRegistra = _currentUser.UserName
                });

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return usuario.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ChangeAvatar(string avatar)
        {
            try
            {
                if (string.IsNullOrEmpty(avatar))
                    return string.Empty;

                Usuario usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == _currentUser.Id && x.Eliminado == false);

                if (usuario == null)
                    return string.Empty;

                usuario.Avatar = avatar;

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return avatar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Usuario> GetLastRegisteredUsers()
        {
            try
            {
                return _dbContext.Usuario
                    .Where(x => x.Eliminado == false && x.Baneado == false)
                    .OrderByDescending(x => x.Id)
                    .Take(5)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Rango> GetRangosUsuarios(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var rangos = _dbContext.Rango
                   .AsNoTracking()
                   .Include(x => x.Usuarios.Where(x => x.Eliminado == false))
                   .Where(x => x.Eliminado == false);

                totalCount = rangos.Count();

                return rangos
                    .OrderByDescending(x => x.Id)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Rango> GetRangosDropdown()
        {
            try
            {
                return _dbContext.Rango
                    .AsNoTracking()
                    .Where(x => x.Eliminado == false)
                    .OrderBy(x => x.Nombre)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveActividadUsuario(Actividad model)
        {
            try
            {
                if (model == null)
                    return 0;

                model.UsuarioRegistra = _currentUser.UserName;
                _dbContext.Actividad.Add(model);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActividadLogsViewModel GetActividadesByUsuario(long usuarioId, TipoActividad? tipoActividad)
        {
            try
            {
                if (usuarioId < 1)
                    return new ActividadLogsViewModel();

                var query = _dbContext.Actividad
                    .Include(x => x.Usuario)
                    .Where(x => x.UsuarioId == usuarioId);

                if (tipoActividad.HasValue)
                    query = query.Where(x => x.TipoActividadString == tipoActividad.Value.ToString());

                var data = query.OrderByDescending(x => x.Id)
                    .Take(30)
                    .ToList();

                var actividades = data.Select(actividad => new ActividadUsuarioViewModel()
                {
                    Id = actividad.Id,
                    FechaRegistro = actividad.FechaRegistro,
                    TipoActividad = actividad.TipoActividad,
                    Texto = SetActividadText(actividad.TipoActividad, actividad.ObjId1, actividad.Datos)
                }).ToList();

                ActividadLogsViewModel actividad = new ActividadLogsViewModel()
                {
                    Hoy = ActividadesGroupBy(actividades, "hoy"),
                    Ayer = ActividadesGroupBy(actividades, "ayer"),
                    Semana = ActividadesGroupBy(actividades, "semana"),
                    Mes = ActividadesGroupBy(actividades, "mes")
                };

                return actividad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public RecordUsersOnlineViewModel GetRecordUsersOnline()
        {
            try
            {
                Configuracion configuracion = _dbContext.Configuracion.FirstOrDefault();

                if (configuracion == null)
                    return new RecordUsersOnlineViewModel();

                return new RecordUsersOnlineViewModel()
                {
                    UsersOnline = configuracion.RecordOnlineUsers,
                    Date = configuracion.RecordOnlineTime
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long AddUpdateRango(Rango model)
        {
            try
            {
                if (model == null)
                    return 0;

                if (model.Id > 0)
                {
                    Rango rango = _dbContext.Rango.FirstOrDefault(x => x.Id == model.Id);

                    if (rango == null)
                        throw new Exception($"No se ha encontrado rango con Id {model.Id}");

                    rango.Color = model.Color;
                    rango.Icono = model.Icono;
                    rango.Nombre = model.Nombre;
                    rango.Tipo = model.Tipo;
                    rango.Puntos = model.Puntos;

                    rango.FechaActualiza = DateTime.Now;
                    rango.UsuarioActualiza = _currentUser.UserName;

                    _dbContext.Update(rango);
                }
                else
                {
                    model.UsuarioRegistra = _currentUser.UserName;

                    _dbContext.Rango.Add(model);
                }

                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long ChangeRangoUsuario(RangoViewModel model)
        {
            try
            {
                Usuario usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == model.UsuarioId && x.Eliminado == false);

                if (usuario == null)
                    throw new Exception("No se ha podido actualizar el rango del usuario");

                usuario.RangoId = model.Id;

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return usuario.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int SessionOnlineUser(string bearer, string IP)
        {
            try
            {
                if (string.IsNullOrEmpty(IP))
                    return 0;

                Session session = null;

                if(!string.IsNullOrEmpty(bearer))
                {
                    bearer = bearer.Replace("Bearer ", "");

                    session = _dbContext.Session.FirstOrDefault(x => x.Token == bearer);

                    if (session != null)
                        return 0;
                }

                DeleteSessionByIP(IP);

                _dbContext.Session.Add(new Session()
                {
                    Activo = DateTime.Now,
                    FechaRegistro = DateTime.Now,
                    IP = IP
                });

                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string SetActividadText(TipoActividad tipoActividad, long id, string data)
        {
            try
            {
                if (id <= 0)
                    return string.Empty;

                switch (tipoActividad)
                {
                    case TipoActividad.PostNuevo:
                    case TipoActividad.PostFavorito:
                    case TipoActividad.PostVotado:
                    case TipoActividad.ComentarioNuevo:
                    case TipoActividad.ComentarioVotado:
                    case TipoActividad.SiguiendoPost:
                        Post post = _dbContext.Post
                                .Include(x => x.Categoria)
                                .AsNoTracking()
                                .FirstOrDefault(x => x.Id == id);

                        if (tipoActividad == TipoActividad.PostVotado)
                            return $"Dejó <strong>{data}</strong> puntos en el post {SetPostURL(post)}";
                        else if (tipoActividad == TipoActividad.ComentarioNuevo)
                            return $"Comentó el post {SetPostURL(post)}";
                        else if (tipoActividad == TipoActividad.PostNuevo)
                            return $"Creó un nuevo post {SetPostURL(post)}";
                        else if (tipoActividad == TipoActividad.SiguiendoPost)
                            return $"Está siguiendo el post {SetPostURL(post)}";
                        else if (tipoActividad == TipoActividad.PostFavorito)
                            return $"Agregó a favoritos el post {SetPostURL(post)}";
                        else if (tipoActividad == TipoActividad.ComentarioVotado)
                            return $"Votó {data} un comentario en el post {SetPostURL(post)}";
                        break;

                    case TipoActividad.SiguiendoUsuario:
                        Usuario usuario = _dbContext.Usuario.AsNoTracking().FirstOrDefault(x => x.Id == id);
                        return $"Está siguiendo a <a href='/perfil/{usuario.UserName}'><img class='img-fluid' height='16px' width='16px' src='{(!string.IsNullOrEmpty(usuario.Avatar) ? _keys.PixicityURL + "/images/avatars/" + usuario.UserName + "/" + usuario.Avatar : "/assets/images/avatar.gif")}' title='{usuario.UserName}' /> {usuario.UserName}</a>";
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string SetPostURL(Post post)
        {
            try
            {
                if (post == null)
                    return "'Este post ya no existe en la comunidad'";

                return $"<a href='/posts/{post.Categoria.SEO}/{post.Id}/{post.URL}'>{post.Titulo}</a>";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private List<ActividadUsuarioViewModel> ActividadesGroupBy(List<ActividadUsuarioViewModel> actividades, string date)
        {
            try
            {
                DateTime today = DateTime.Today;

                switch (date)
                {
                    case "hoy":
                        DateTime endDateTime = today.AddDays(1).AddTicks(-1);
                        return actividades.Where(x => x.FechaRegistro >= today && x.FechaRegistro <= endDateTime).ToList();
                    case "ayer":
                        DateTime yesterday = today.AddDays(-1);
                        return actividades.Where(x => x.FechaRegistro >= yesterday && x.FechaRegistro < today).ToList();
                    case "semana":
                        DateTime filter = today.AddDays(-2);
                        DateTime filter2 = today.AddDays(-20);
                        return actividades.Where(x => x.FechaRegistro < filter && x.FechaRegistro >= filter2).ToList();
                    case "mes":
                        DateTime dateFilter = today.AddDays(-21);
                        return actividades.Where(x => x.FechaRegistro <= dateFilter).ToList();
                    default:
                        return actividades;
                }
            }
            catch (Exception)
            {
                return new List<ActividadUsuarioViewModel>();
            }
        }
    }
}
