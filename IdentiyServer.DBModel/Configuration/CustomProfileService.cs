using IdentityServer.DBModel.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentiyServer.DBModel.Configuration
{
    public class CustomProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = InMemoryConfiguration.GetUsers()
                .Find(u => u.SubjectId.Equals(sub));
            context.IssuedClaims.AddRange(user.Claims);
            return Task.CompletedTask;
        }
        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = InMemoryConfiguration.GetUsers()
                .Find(u => u.SubjectId.Equals(sub));
            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
