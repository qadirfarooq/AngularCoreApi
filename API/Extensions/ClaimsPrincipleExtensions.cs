using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string? GetUsername (this ClaimsPrincipal user)
        {
            return  user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}