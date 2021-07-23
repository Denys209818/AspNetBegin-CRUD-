using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
