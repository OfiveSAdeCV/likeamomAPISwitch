using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NanniesAPI.AccesoDatos
{
    public class Persistencia
    {
        public static IConfiguration config;

        public static string strCon //likeamom
        {
            get
            {
                string Server = config["DBConnection:Server"];
                string Database = config["DBConnection:Database"];
                string User = config["DBConnection:User"];
                string Password = config["DBConnection:Password"];

                return $"Server= {Server}; Database= {Database}; User Id= {User}; Password= {Password}; Connect Timeout=2; max pool size=10";
            }
        }
        public static string strCon1 //likeamomUsuarios
        {
            get
            {
                string Server = config["DBConnection1:Server"];
                string Database = config["DBConnection1:Database"];
                string User = config["DBConnection1:User"];
                string Password = config["DBConnection1:Password"];

                return $"Server= {Server}; Database= {Database}; User Id= {User}; Password= {Password}; Connect Timeout=2; max pool size=10";
            }
        }
        public static string strCon2 //LikeamomSuc01
        {
            get
            {
                string Server = config["DBConnection2:Server"];
                string Database = config["DBConnection2:Database"];
                string User = config["DBConnection2:User"];
                string Password = config["DBConnection2:Password"];

                return $"Server= {Server}; Database= {Database}; User Id= {User}; Password= {Password}; Connect Timeout=2; max pool size=10";
            }
        }
        public static DataTable ConvertToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i <= propertyDescriptorCollection.Count - 1; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count - 1 + 1];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}