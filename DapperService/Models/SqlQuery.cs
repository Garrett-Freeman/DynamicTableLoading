using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Models;

public class SqlQuery
{
    public QueryType QueryType { get; set; }
    public DynamicTable? Table { get; set; }
    public dynamic? Object { get; set; }
}

public enum QueryType
{
    Insert, 
    Update, 
    Delete
}
