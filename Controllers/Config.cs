using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Controllers
{
    public static class Config
    {
        public static string ConnectionString
        {
            get { return "Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123"; }
        }
    }
}
