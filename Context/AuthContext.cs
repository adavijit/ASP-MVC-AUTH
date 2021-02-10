using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LoginSignupMVC.Models;

namespace LoginSignupMVC.Context
{
    public class AuthContext : DbContext
    {
        public DbSet<Register> RegsDbSet { get; set; }
    }
}