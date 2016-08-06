using Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DAL
{
    public class GlobalOptionDAL
    {
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static GlobalOption GetOption(String key)
        {
            GlobalOption model = null;
            String sql = String.Format("SELECT * FROM global_options WHERE key = '{0}'", key);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    model = new GlobalOption();
                    if (reader["id"] != null && reader["id"].ToString() != "")
                    {
                        model.Id = int.Parse(reader["id"].ToString());
                    }
                    if (reader["key"] != null && reader["key"].ToString() != "")
                    {
                        model.Key = reader["key"].ToString();
                    }
                    if (reader["value"] != null && reader["value"].ToString() != "")
                    {
                        model.Value = reader["value"].ToString();
                    }
                    if (reader["desc"] != null)
                    {
                        model.Desc = reader["desc"].ToString();
                    }
                    if (reader["visible"] != null)
                    {
                        model.Visible = reader["visible"].ToString() == "1";
                    }
                    if (reader["created_at"] != null && reader["created_at"].ToString() != "")
                    {
                        model.CreatedAt = model.ConvertIntDateTime(Convert.ToInt32(reader["created_at"].ToString()));
                    }
                    if (reader["updated_at"] != null && reader["updated_at"].ToString() != "")
                    {
                        model.UpdatedAt = model.ConvertIntDateTime(Convert.ToInt32(reader["updated_at"].ToString()));
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 设置配置项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Boolean SetOption(String key, String value)
        {
            GlobalOption model = null;
            String sql = String.Format("UPDATE global_options SET value = '{1}' WHERE key = '{0}'", key, value);
            int num = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql);
            return num > 0;
        }
    }
}
