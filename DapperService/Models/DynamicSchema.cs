using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public class DynamicSchema
{
    public int SchemaId { get; set; }

    public string SchemaName { get; set; } = "";

    public List<DynamicTable>? Tables { get; set; }
}
