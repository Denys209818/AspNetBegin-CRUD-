using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain.Entities.Identities
{
    public class AppRole : IdentityRole<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
