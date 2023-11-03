using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public class DynamicDatabase
{
    public string Name { get; set; } = "";
    public List<DynamicSchema>? Schemas { get; set; }
    public string ConnectionString { get; set; } = "";
}
