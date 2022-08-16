using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAccessor
    {
        public string UserId { get; }
        public ClaimsPrincipal GetUserPrinciple { get;}
    }
}
