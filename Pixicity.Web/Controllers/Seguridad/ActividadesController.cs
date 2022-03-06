using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixicity.Domain.Extensions;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Web.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly ISeguridadService _seguridadService;
        private readonly IMapper _mapper;

        public ActividadesController(ISeguridadService seguridadService, IMapper mapper)
        {
            _seguridadService = seguridadService;
            _mapper = mapper;
        }
    }
}
