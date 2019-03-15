using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliMood.Web.Infrastructure.Mapper
{
    public interface ICustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}
