using BasicORM.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicORM
{
    public class Query<T>
    {
        string baseQuery = "SELECT {0} FROM {0}";

        List<Criteria<T>> _criterias;

        public Query(string tableNames):this ("*",tableNames)
        { 
        }

        public Query(List<Criteria<T>> criterias, string tableNames):this("*", tableNames)
        {
            _criterias = criterias; 
        }

        public Query(string columnNames, string tableNames)
        {
            _criterias = new List<Criteria<T>>();
            baseQuery=string.Format(baseQuery,columnNames,tableNames); 
        }
        public Query(Expression<Func<T,object>> method,string tableNames)
        {
            this._criterias = new List<Criteria<T>>();
            NewExpression expression=method.Body as NewExpression;
            List<Expression> arguments = expression.Arguments.ToList();

            StringBuilder columns=new StringBuilder();
            for (int i = 0; i < arguments.Count; i++)
            {
                Expression argument = arguments[i];
                columns.Append(argument.ToString().Remove(0,argument.ToString().IndexOf(".")+1));
                if (i!=arguments.Count-1)
                {
                    columns.Append(", ");
                }

            }
            baseQuery = string.Format(baseQuery, columns, tableNames);

        }
        public void Add(Criteria<T> criteria)
        {
            this._criterias.Add(criteria);
        }
        public string GenerateWhereClause()
        {
            StringBuilder whereClause=new StringBuilder();
            whereClause.Append("Where ");

            for (int i = 0; i < _criterias.Count; i++)
            {
                Criteria<T> criteria = _criterias[i];
                whereClause.Append(criteria.GenerateSql());
                if (i!=_criterias.Count-1)
                {
                    whereClause.Append(criteria.QueryLogicalOperator switch
                    {
                        QueryLogicalOperator.Or => " OR ",
                        QueryLogicalOperator.And => " AND "
                    });
                }
               
            }
            return $"{baseQuery} {whereClause.ToString()}";
        }
    }
}
