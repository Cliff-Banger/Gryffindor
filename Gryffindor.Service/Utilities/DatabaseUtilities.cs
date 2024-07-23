using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Service.Utilities
{
   public class DatabaseUtilities
   {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
   }
}
