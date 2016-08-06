using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DAL
{
    public class OrderDetailDAL
    {
        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<OrderDetail> GetDetailsByWhere(String where)
        {
            List<OrderDetail> orders = new List<OrderDetail>();

            String sql = String.Format("SELECT * FROM orders_details WHERE {0}", where);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    OrderDetail detail = DataRowToModel(reader);
                    orders.Add(detail);
                }
            }

            return orders;
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrderDetail GetDetail(int id)
        {
            OrderDetail detail = null;

            String sql = String.Format("SELECT * FROM orders_details WHERE id = {0}", id);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    detail = DataRowToModel(reader);
                }
            }

            return detail;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static Boolean Create(OrderDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into orders_details(");
            strSql.Append("order_id,goods_id,spec_id,price,num,size,color,set_meal,remark,use_flag,created_at,updated_at, last_updated_at)");
            strSql.Append(" values (");
            strSql.Append("@order_id,@goods_id,@spec_id,@price,@num,@size,@color,@set_meal,@remark,@use_flag,@created_at,@updated_at, @last_updated_at)");
            strSql.Append(";select LAST_INSERT_ROWID()");

            List<SQLiteParameter> parameters = GetParameters(model);
            int num = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray<SQLiteParameter>());
            if (num > 0) {
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static Boolean Update(OrderDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update orders_details set ");
            strSql.Append("order_id=@order_id,");
            strSql.Append("goods_id=@goods_id,");
            strSql.Append("spec_id=@spec_id,");
            strSql.Append("price=@price,");
            strSql.Append("num=@num,");
            strSql.Append("size=@size,");
            strSql.Append("color=@color,");
            strSql.Append("set_meal=@set_meal,");
            strSql.Append("remark=@remark,");
            strSql.Append("use_flag=@use_flag,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("last_updated_at=@last_updated_at");
            strSql.Append(" where id=@id");
            List<SQLiteParameter> parameters = GetParameters(model);
            SQLiteParameter idParams = new SQLiteParameter("@id", DbType.Int32, 8);
            idParams.Value = model.Id;
            parameters.Add(idParams);

            int num = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray<SQLiteParameter>());
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private static OrderDetail DataRowToModel(SQLiteDataReader row)
        {
            OrderDetail model = new OrderDetail();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["order_id"] != null && row["order_id"].ToString() != "")
                {
                    model.OrderId = int.Parse(row["order_id"].ToString());
                }
                if (row["goods_id"] != null && row["goods_id"].ToString() != "")
                {
                    model.GoodsId = int.Parse(row["goods_id"].ToString());
                }
                if (row["spec_id"] != null && row["spec_id"].ToString() != "")
                {
                    model.SpecId = int.Parse(row["spec_id"].ToString());
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["price"].ToString());
                }
                if (row["num"] != null && row["num"].ToString() != "")
                {
                    model.Num = int.Parse(row["num"].ToString());
                }
                if (row["size"] != null)
                {
                    model.Size = row["size"].ToString();
                }
                if (row["color"] != null)
                {
                    model.Color = row["color"].ToString();
                }
                if (row["set_meal"] != null)
                {
                    model.SetMeal = row["set_meal"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.Remark = row["remark"].ToString();
                }
                if (row["use_flag"] != null && row["use_flag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(row["use_flag"].ToString()) == 1;
                }
                if (row["created_at"] != null && row["created_at"].ToString() != "")
                {
                    model.CreatedAt = model.ConvertIntDateTime(Convert.ToInt64(row["created_at"].ToString()));
                }
                if (row["updated_at"] != null && row["updated_at"].ToString() != "")
                {
                    model.UpdatedAt = model.ConvertIntDateTime(Convert.ToInt64(row["updated_at"].ToString()));
                }
                if (row["last_updated_at"] != null && row["last_updated_at"].ToString() != "")
                {
                    model.UpdatedAt = model.ConvertIntDateTime(Convert.ToInt64(row["last_updated_at"].ToString()));
                }
            }
            return model;
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static List<SQLiteParameter> GetParameters(OrderDetail model)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>() {
					new SQLiteParameter("@order_id", DbType.Int32,8),
					new SQLiteParameter("@goods_id", DbType.Int32,8),
					new SQLiteParameter("@spec_id", DbType.Int32,8),
					new SQLiteParameter("@price", DbType.Decimal,4),
					new SQLiteParameter("@num", DbType.Int32,8),
					new SQLiteParameter("@size", DbType.String),
					new SQLiteParameter("@color", DbType.String),
					new SQLiteParameter("@set_meal", DbType.String),
					new SQLiteParameter("@remark", DbType.String),
					new SQLiteParameter("@use_flag", DbType.Int32,8),
					new SQLiteParameter("@created_at", DbType.Int64,8),
					new SQLiteParameter("@updated_at", DbType.Int64,8),
                    new SQLiteParameter("@last_updated_at", DbType.Int64,8),};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.GoodsId;
            parameters[2].Value = model.SpecId;
            parameters[3].Value = model.Price;
            parameters[4].Value = model.Num;
            parameters[5].Value = model.Size;
            parameters[6].Value = model.Color;
            parameters[7].Value = model.SetMeal;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.UseFlag;
            parameters[10].Value = model.ConvertDateTimeInt(model.CreatedAt);
            parameters[11].Value = model.ConvertDateTimeInt(model.UpdatedAt);
            parameters[11].Value = model.ConvertDateTimeInt(model.LastUpdatedAt);
            return parameters;
        }
    }
}
