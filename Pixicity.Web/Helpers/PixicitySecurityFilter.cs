using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Transversal;
using Pixicity.Service.Interfaces;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace Pixicity.Web.Helpers
{
    public class PixicitySecurityFilter : IActionFilter
    {
        public ISeguridadService _seguridadService;
        public IJwtService _jwtService;
        private string _functionality;
        private Stopwatch _stopWatch;
        private IAppPrincipal _appPrincipal;

        public PixicitySecurityFilter(ISeguridadService seguridadService, IJwtService jwtService, string functionality, IAppPrincipal appPrincipal)
        {
            _seguridadService = seguridadService;
            _jwtService = jwtService;
            _functionality = functionality;
            _appPrincipal = appPrincipal;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                _stopWatch = Stopwatch.StartNew();

                if (_functionality == "None")
                    return;

                string Controller = context.RouteData.Values["Controller"].ToString().ToLower();
                string Action = context.RouteData.Values["Action"].ToString().ToLower();

                string bearer = context.HttpContext.Request.Headers["Authorization"];

                if (bearer == null || string.IsNullOrEmpty(bearer.ToString()))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                string jwtUniqueName = _jwtService.GetUniqueName(bearer);

                if (jwtUniqueName == null)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    return;
                }

                var user = _seguridadService.GetUsuarioWithRangoByUserName(jwtUniqueName);
                var activeSession = _seguridadService.GetSessionByToken(_jwtService.GetTokenFromJWT(bearer));

                if(activeSession == null || DateTime.Now > activeSession.FechaExpiracion || activeSession.Eliminado == true)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Locked;
                    return;
                }

                _seguridadService.UpdateSessionActivoDate(activeSession);

                if (_functionality == "Jwt")
                {
                    if (user == null)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }

                    user.UltimaIP = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    SetUserInfoToAppPrincipal(user);
                    return;
                }

                SetUserInfoToAppPrincipal(user);

                //base.OnActionExecuting(context);
            }
            catch (Exception e)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                throw e;
            }
        }

        private void SetUserInfoToAppPrincipal(Usuario usuario)
        {
            _appPrincipal.Id = usuario.Id;
            _appPrincipal.UserName = usuario.UserName;
            _appPrincipal.IP = usuario.UltimaIP;
            
            _appPrincipal.IsAdmin = usuario.Rango?.Nombre == "Administrador";
            _appPrincipal.IsModerador = usuario.Rango?.Nombre == "Moderador";
        }
        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();

            if (filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.Headers.Add("ExecutionTime", _stopWatch.Elapsed.TotalSeconds.ToString());
            }
        }
    }
}
