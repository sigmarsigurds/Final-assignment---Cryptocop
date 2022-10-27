using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Cryptocop.Software.API.Profiles
{
    public class PriceResolver : IValueResolver<object, object, int>
    {


        public int Resolve(object source, object destination, int destMember, ResolutionContext context)
        {
            return destMember * 2;
        }
    }
}