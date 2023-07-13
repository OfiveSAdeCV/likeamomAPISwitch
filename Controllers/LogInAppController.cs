using LoginAPP.AccesoDatos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LoginAPP.Entidades.EntidadesNegocio;

namespace NanniesAPI.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LogInAppController : ControllerBase
    {
        [HttpPost]
        public ActionResult LogInApp(LogInAppCredentials pLoginCredentials)
        {
            try
            {
                return Ok(ad.LoginInApp(pLoginCredentials));
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new { Mensaje = Ex.Message, result = 1 });
            }
        }
    }
}