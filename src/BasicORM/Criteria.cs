using BasicORM.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicORM
{
    public class Criteria<TModel>
    {
        string _operator;
        string _field;
        object _value;
        QueryLogicalOperator _queryOperator;

        public QueryLogicalOperator QueryLogicalOperator => _queryOperator;

        public Criteria(string @operator, string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            _operator=@operator; 
            _field=field;
            _value=value;
            _queryOperator = queryOperator;
        }
        /// <summary>
        /// DebugField Generic Method ise Tüm Func<typeparamref name="TKey"/>  parametreli şart 
        /// methodlarında lambda ile seçilen propery'inin kolon adı olarak kullanbilmesi için 
        /// string olarak ayırt edilebilmesi işlevini gerçekleştirmektedir.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        static string DebugField<TKey>(Expression<Func<TModel,TKey>> method)
        {
            string field = method.Body.ToString();
            field = field.Remove(0, field.IndexOf(".") + 1);
            return field;
        }
        /// <summary>
        /// Sorguya büyüktür işlemi eklemektedir.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="queryLogicalOperator"></param>
        /// <returns></returns>
        public static Criteria<TModel> GreaterThan(string field, object value, QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
            => new Criteria<TModel>(">", field, value, queryLogicalOperator);

        public static Criteria<TModel> GreaterThan<TKey>(Expression<Func<TModel,TKey>> method, object value,QueryLogicalOperator queryLogicalOperator=QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new Criteria<TModel>(">", field, queryLogicalOperator);
        }

        public static Criteria<TModel> GreaterThanOrEqual(string field,object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            return new Criteria<TModel> (">=", field, value, queryOperator);
        }

        public static Criteria<TModel> GreaterThanOrEqueal<TKey>(Expression<Func<TModel,TKey>> method, object value,QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
        {
            string field=DebugField(method);
            return new Criteria<TModel>(">=", field, queryLogicalOperator);
        }

        public static Criteria<TModel> LessThan(string field,object value, QueryLogicalOperator queryLogicalOperator=QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("<", field, value, queryLogicalOperator);
        }
        public static Criteria<TModel> LessThan<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("<", field, value, queryOperator);
        }
        public static Criteria<TModel> LessThanOrEqual(string field, object value, QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("<=",field,value, queryLogicalOperator);
        }
        public static Criteria<TModel> LessThanOrEqual<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("<=", field, value, queryOperator);
        }

        public static Criteria<TModel> Equal(string field,object value,QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("=", field, value, queryLogicalOperator);
        }
        public static Criteria<TModel> Equal<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("=", field, value, queryOperator);
        }
        public static Criteria<TModel> Contains(string field, object value,QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("Like",field, $"%{value}%", queryLogicalOperator);
        }
        public static Criteria<TModel> Contains<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("Like", field, $"%{value}%", queryOperator);
        }
        public static Criteria<TModel> StartsWith(string field, object value, QueryLogicalOperator queryLogicalOperator = QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("Like", field, $"{value}%", queryLogicalOperator);
        }

        public static Criteria<TModel> StartsWith<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("Like", field, $"{value}%", queryOperator);
        }
        public static Criteria<TModel> EndsWith(string field,object value,QueryLogicalOperator queryLogicalOperator=QueryLogicalOperator.None)
        {
            return new Criteria<TModel>("Like", field, $"{value}", queryLogicalOperator);
        }
        public static Criteria<TModel> EndsWith<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = method.Body.ToString();
            return new("Like", field, $"%{value}", queryOperator);
        }
        public string GenerateSql()
        {
            return $"{_field} {_operator} {(_value is int or long or float or decimal ? _value: $"'{_value}'")}";
        }

    }
}
