using System;
using System.Collections.Generic;
using System.Data.Entity;
using HW7.Models;
using System.Linq;
using System.Web;

namespace HW7.DAL
{
    public class RequestDBContext : DbContext
    {
        public RequestDBContext() : base("name=RequestDBContext")
        { }

        public virtual DbSet<Request> Requests { get; set; }
    }
}