using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.AnnouncementCRUD.Dtos;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace Application.Helpers
{
    public class AnnouncementRequirement: IAuthorizationRequirement
    {
    }

    public class AnnnouncemntCompanyAuthorizationHandler : 
        AuthorizationHandler<AnnouncementRequirement, Announcement>
    {

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AnnouncementRequirement requirement,
            Announcement resource)
        {
            if(context.User.Identity?.Name == resource.UserName)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;;
        }
    }
}
