using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperService.Services;

public class QueryBuilder
{
    private readonly SqlQuery _query;

    public QueryBuilder(SqlQuery query)
    {
        _query = query;
    }

    //public string GenerateQuery()
    //{
    //    var sql = "";

    //    switch (_query.QueryType)
    //    {
    //        case QueryType.Insert:
    //            sql = GenerateInsert();
    //            break;
    //        case QueryType.Update:
    //            sql = GenerateUpdate();
    //            break;
    //        case QueryType.Delete:
    //            sql = GenerateDelete();
    //            break;
    //        default:
    //            sql = string.Empty;
    //            break;
    //    };

    //    return sql;
    //}

    //private string GenerateInsert()
    //{

    //}

    //private string GenerateUpdate()
    //{

    //}

    //private string GenerateDelete()
    //{

    //}
}
