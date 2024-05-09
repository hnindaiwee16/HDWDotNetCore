using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HDWDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T>Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if(parameters is not null && parameters.Length > 0)
            {
                var parameterArray = parameters.Select(x=>new SqlParameter(x.Name, x.Value)).ToArray();
                command.Parameters.AddRange(parameterArray);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T>lst = JsonConvert.DeserializeObject<List<T>>(json)!;
            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                var parameterArray = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
                command.Parameters.AddRange(parameterArray);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            return lst[0];
        }
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if(parameters is not null && parameters.Length > 0)
            {
                var parameterArray = parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
                command.Parameters.AddRange(parameterArray);
            }
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
    public class AdoDotNetParameter
    {
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get;set; }
        public object Value { get;set; }
    }
    public static class AdoDotNetParameterListExtension
    {
        public static List<AdoDotNetParameter> Add(this List<AdoDotNetParameter> lst, string name, object value)
        {
            lst.Add(new AdoDotNetParameter(name, value));
            return lst;
        }
    }

}
