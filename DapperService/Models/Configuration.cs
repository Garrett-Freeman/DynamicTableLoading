using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public class Configuration
{
    public string ConnectionString { get; set; } = "";

    public Configuration(string connectionString) 
    {
        ConnectionString = ConnectionString;
    }
}
