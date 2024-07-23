using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IGeneralClientService
    {
        IList<Profession> GetProfessions();
    }
}