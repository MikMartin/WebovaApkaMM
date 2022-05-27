using Microsoft.EntityFrameworkCore;

namespace WebovaApkaMM.Models
{
    public class MyDbWeb : DbContext
    {
        public MyDbWeb(DbContextOptions options) : base(options)
        {


        }
        public virtual DbSet<Employee> Employees { get; set; }


    }
}
