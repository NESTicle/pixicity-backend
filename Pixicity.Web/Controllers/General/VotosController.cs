using Microsoft.AspNetCore.Mvc;
using Pixicity.Data.Models.Posts;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Threading.Tasks;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Web.Controllers.General
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly IPostService _postService;

        public VotosController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route(nameof(GetAvailableVotos))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> GetAvailableVotos([FromQuery] VotosTypeEnum type)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.GetAvailableVotos(type);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        [Route(nameof(SetVoto))]
        [TypeFilter(typeof(PixicitySecurityFilter), Arguments = new[] { "Jwt" })]
        public async Task<JSONObjectResult> SetVoto([FromBody] Voto model)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _postService.SetVoto(model);
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }
    }
}
