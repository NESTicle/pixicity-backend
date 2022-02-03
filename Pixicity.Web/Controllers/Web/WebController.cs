using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebService _webService;

        public WebController(IMapper mapper, IWebService webService)
        {
            _mapper = mapper;
            _webService = webService;
        }

        [HttpGet]
        [Route(nameof(GetAdsByType))]
        public async Task<JSONObjectResult> GetAdsByType([FromQuery] string type)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetAdsByType(type);
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
