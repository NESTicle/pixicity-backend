using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Web;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixicity.Web.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebService _webService;

        public TopsController(IMapper mapper, IWebService webService)
        {
            _mapper = mapper;
            _webService = webService;
        }

        [HttpGet]
        [Route(nameof(GetTopUsers))]
        public async Task<JSONObjectResult> GetTopUsers()
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                result.Data = _webService.GetTopUsuarios();
            }
            catch (Exception e)
            {
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.Errors.Add(e.Message);
            }

            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route(nameof(GetTopPosts))]
        public async Task<JSONObjectResult> GetTopPosts([FromQuery] string date)
        {
            JSONObjectResult result = new JSONObjectResult
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var data = _webService.GetTopPosts(date);
                var mapped = _mapper.Map<List<TopPostsViewModel>>(data);
                
                result.Data = mapped;
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
