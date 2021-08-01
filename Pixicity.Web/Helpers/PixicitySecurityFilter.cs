﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Transversal;
using Pixicity.Service.Interfaces;
using System;
using System.Diagnostics;
using System.Net;

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
                    context.Result = new UnauthorizedResult();

                    return;
                }

                string jwtUniqueName = _jwtService.GetUniqueName(bearer);

                if (jwtUniqueName == null)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    context.Result = new ForbidResult();

                    return;
                }

                var user = _seguridadService.GetUsuarioByUserName(jwtUniqueName);

                if (_functionality == "Jwt")
                {
                    if (user == null)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new ForbidResult();
                    }

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