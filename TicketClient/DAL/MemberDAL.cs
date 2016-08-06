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
    public class MemberDAL
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Debug");

        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<Member> GetDetailsByWhere(String where)
        {
            List<Member> members = new List<Member>();

            String sql = String.Format("SELECT * FROM members WHERE {0}", where);
            log.Debug("SQL:" + sql);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    Member member = DataRowToModel(reader);
                    members.Add(member);
                }
            }

            return members;
        }

        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static Member GetMember(int id)
        {
            Member member = null;

            String sql = String.Format("SELECT * FROM members WHERE user_id = {0}", id);
            log.Debug("SQL:" + sql);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    member = DataRowToModel(reader);
                }
            }

            return member;
        }

        /// <summary>
        /// 获取某登录用户的会员信息
        /// </summary>
        /// <param name="id">登录用户ID</param>
        /// <returns>符合条件的数据</returns>
        public static Member GetMemberByUserId(int id)
        {
            Member member = null;

            String sql = String.Format("SELECT * FROM members WHERE user_id = {0}", id);

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    member = DataRowToModel(reader);
                }
            }

            return member;
        }

        /// <summary>
        /// 根据关键字获取会员信息
        /// </summary>
        /// <param name="no">会员手机号、会员卡号、会员身份证号</param>
        /// <returns>符合条件的数据集</returns>
        public static Member GetMember(String no)
        {
            log.Debug("GetMember:" + no);
            Member member = new Member();

            String sql = String.Format("SELECT * FROM members WHERE no = '{0}'", no);
            log.Debug("SQL:" + sql);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    member = DataRowToModel(reader);
                    member.People = PeopleDAL.GetPeopleByUserId(member.UserId);
                }
            }

            return member;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static Boolean Create(Member model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into members(");
            strSql.Append("id,seller_id,user_id,no,real_no,level_id,score,money,gift_money,reg_from,remark,status,is_new,is_delete,recommend_id,created_at,updated_at,last_at)");
            strSql.Append(" values (");
            strSql.Append("@id,@seller_id,@user_id,@no,@real_no,@level_id,@score,@money,@gift_money,@reg_from,@remark,@status,@is_new,@is_delete,@recommend_id,@created_at,@updated_at,@last_at)");
            strSql.Append(";select LAST_INSERT_ROWID()");

            List<SQLiteParameter> parameters = GetParameters(model);
            SQLiteParameter idParams = new SQLiteParameter("@id", DbType.Int32, 8);
            idParams.Value = model.Id;
            parameters.Add(idParams);

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
        public static Boolean Update(Member model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update members set ");
            strSql.Append("seller_id=@seller_id,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("no=@no,");
            strSql.Append("real_no=@real_no,");
            strSql.Append("level_id=@level_id,");
            strSql.Append("score=@score,");
            strSql.Append("money=@money,");
            strSql.Append("gift_money=@gift_money,");
            strSql.Append("reg_from=@reg_from,");
            strSql.Append("remark=@remark,");
            strSql.Append("status=@status,");
            strSql.Append("is_new=@is_new,");
            strSql.Append("is_delete=@is_delete,");
            strSql.Append("recommend_id=@recommend_id,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("last_at=@last_at");
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
        private static Member DataRowToModel(SQLiteDataReader row)
        {
            Member model = new Member();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["seller_id"] != null && row["seller_id"].ToString() != "")
                {
                    model.SellerId = int.Parse(row["seller_id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.UserId = int.Parse(row["user_id"].ToString());
                }
                if (row["no"] != null)
                {
                    model.No = row["no"].ToString();
                }
                if (row["real_no"] != null)
                {
                    model.RealNo = row["real_no"].ToString();
                }
                if (row["level_id"] != null && row["level_id"].ToString() != "")
                {
                    model.LevelId = int.Parse(row["level_id"].ToString());
                }
                if (row["score"] != null && row["score"].ToString() != "")
                {
                    model.Score = int.Parse(row["score"].ToString());
                }
                if (row["money"] != null && row["money"].ToString() != "")
                {
                    model.Money = decimal.Parse(row["money"].ToString());
                }
                if (row["gift_money"] != null && row["gift_money"].ToString() != "")
                {
                    model.GiftMoney = decimal.Parse(row["gift_money"].ToString());
                }
                if (row["reg_from"] != null)
                {
                    model.From = (RegFrom)Enum.Parse(typeof(RegFrom), row["reg_from"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.Remark = row["remark"].ToString();
                }
                if (row["status"] != null)
                {
                    model.Status = (Status)Enum.Parse(typeof(Status), row["status"].ToString());
                }
                if (row["is_new"] != null && row["is_new"].ToString() != "")
                {
                    model.IsNew = int.Parse(row["is_new"].ToString()) == 1;
                }
                if (row["is_delete"] != null && row["is_delete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(row["is_delete"].ToString()) == 1;
                }
                if (row["recommend_id"] != null && row["recommend_id"].ToString() != "")
                {
                    model.RecommendId = int.Parse(row["recommend_id"].ToString());
                }
                if (row["created_at"] != null && row["created_at"].ToString() != "")
                {
                    model.CreatedAt = model.ConvertIntDateTime(Convert.ToInt32(row["created_at"].ToString()));
                }
                if (row["updated_at"] != null && row["updated_at"].ToString() != "")
                {
                    model.UpdatedAt = model.ConvertIntDateTime(Convert.ToInt32(row["updated_at"].ToString()));
                }
                if (row["last_at"] != null && row["last_at"].ToString() != "")
                {
                    model.LastAt = model.ConvertIntDateTime(Convert.ToInt32(row["last_at"].ToString()));
                }
            }
            return model;
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static List<SQLiteParameter> GetParameters(Member model)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>() {
					new SQLiteParameter("@seller_id", DbType.Int32,8),
					new SQLiteParameter("@user_id", DbType.Int32,8),
					new SQLiteParameter("@no", DbType.String),
					new SQLiteParameter("@real_no", DbType.String),
					new SQLiteParameter("@level_id", DbType.Int32,8),
					new SQLiteParameter("@score", DbType.Int32,8),
					new SQLiteParameter("@money", DbType.Decimal,4),
					new SQLiteParameter("@gift_money", DbType.Decimal,4),
					new SQLiteParameter("@reg_from", DbType.String),
					new SQLiteParameter("@remark", DbType.String),
					new SQLiteParameter("@status", DbType.String),
					new SQLiteParameter("@is_new", DbType.Int32,8),
					new SQLiteParameter("@is_delete", DbType.Int32,8),
					new SQLiteParameter("@recommend_id", DbType.Int32,8),
					new SQLiteParameter("@created_at", DbType.Int64,8),
					new SQLiteParameter("@updated_at", DbType.Int64,8),
					new SQLiteParameter("@last_at", DbType.Int64,8)};
            parameters[0].Value = model.SellerId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.No;
            parameters[3].Value = model.RealNo;
            parameters[4].Value = model.LevelId;
            parameters[5].Value = model.Score;
            parameters[6].Value = model.Money;
            parameters[7].Value = model.GiftMoney;
            parameters[8].Value = model.From.ToString();
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.IsNew;
            parameters[12].Value = model.IsDelete;
            parameters[13].Value = model.RecommendId;
            parameters[14].Value = model.ConvertDateTimeInt(model.CreatedAt);
            parameters[15].Value = model.ConvertDateTimeInt(model.UpdatedAt);
            parameters[16].Value = model.ConvertDateTimeInt(model.LastAt);
            return parameters;
        }
    }
}
