using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Models;
using Tools;

namespace DAL
{
    public class PeopleTicketDAL
    {
        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<PeopleTicket> GetPeopleByWhere(String where)
        {
            List<PeopleTicket> peoples = new List<PeopleTicket>();

            String sql = String.Format("SELECT * FROM peoples_tickets WHERE {0}", where);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    PeopleTicket ticket = DataRowToModel(reader);
                    peoples.Add(ticket);
                }
            }

            return peoples;
        }

        /// <summary>
        /// 获取一个用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PeopleTicket GetPeopleTicketById(int id)
        {
            PeopleTicket people = null;
            String sql = String.Format("SELECT * FROM peoples_tickets WHERE id = {0}", id);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    people = DataRowToModel(reader);
                }
            }
            return people;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private static PeopleTicket DataRowToModel(SQLiteDataReader row)
        {
            PeopleTicket model = new PeopleTicket();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.UserId = int.Parse(row["user_id"].ToString());
                    model.People = PeopleDAL.GetPeopleByUserId(model.UserId);
                    model.Wechat = WechatDAL.GetWechatByUserId(model.UserId);
                    model.Member = MemberDAL.GetMemberByUserId(model.UserId);
                }
                if (row["order_id"] != null && row["order_id"].ToString() != "")
                {
                    model.OrderId = int.Parse(row["order_id"].ToString());
                }
                if (row["date"] != null && row["date"].ToString() != "")
                {
                    model.Date = model.ConvertIntDateTime(int.Parse(row["date"].ToString()));
                }
                if (row["num"] != null && row["num"].ToString() != "")
                {
                    model.Num = int.Parse(row["num"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.Status = row["status"].ToString();
                }
                if (row["created_at"] != null && row["created_at"].ToString() != "")
                {
                    model.CreatedAt = model.ConvertIntDateTime(int.Parse(row["created_at"].ToString()));
                }
                if (row["updated_at"] != null && row["updated_at"].ToString() != "")
                {
                    model.UpdatedAt = model.ConvertIntDateTime(int.Parse(row["updated_at"].ToString()));
                }
            }
            return model;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(PeopleTicket model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update peoples_tickets set ");
            strSql.Append("num=@num");
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
        /// 获取参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static List<SQLiteParameter> GetParameters(PeopleTicket model)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>() {
                    new SQLiteParameter("@num", DbType.Int32, 8),
                    new SQLiteParameter("@id", DbType.Int32, 8)};
            parameters[0].Value = model.Num;
            parameters[1].Value = model.Id;
            return parameters;
        }
    }
}