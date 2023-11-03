using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public  class DynamicTable
{
    public string TableName { get; set; } = "";

    public string SchemaName { get; set; } = "";

    public List<ColumnMetadata> Columns { get; set; } = new();

    public List<dynamic> Data { get; set; } = new();
}
