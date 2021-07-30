using Pixicity.Data.Models.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IWebService
    {
        /// <summary>
        /// Se guarda un afiliado
        /// </summary>
        /// <param name="model">Entidad Afiliado</param>
        /// <returns></returns>
        string SaveAfiliado(Afiliado model);
    }
}
