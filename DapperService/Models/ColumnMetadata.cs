using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public class ColumnMetadata
{
    public string ColumnName { get; set; } = "";
    public string DataType { get; set; } = "";
    public int MaxLength { get; set; }
    public bool IsNullable { get; set; }
}
