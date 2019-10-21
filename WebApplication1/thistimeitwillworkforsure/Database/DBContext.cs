using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace thistimeitwillworkforsure
{
    public class DBContext : DbContext
    {
        public DbSet<FireWorks.User> Users { get; set; }
        public DbSet<FireWorks.Vehicle> Vehicles { get; set; }
        public DbSet<FireWorks.Resource> Resources { get; set; }
        public DbSet<FireWorks.FireFighter> FireFighters { get; set; }
        public DbSet<FireWorks.Deployment> Deployments { get; set; }
    }
}