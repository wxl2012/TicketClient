using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Models;
using Tools;

namespace DAL
{
    public class WechatDAL
    {
        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<Wechat> GetPeopleByWhere(String where)
        {
            List<Wechat> wechats = new List<Wechat>();

            String sql = String.Format("SELECT * FROM wechats WHERE {0}", where);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    Wechat wechat = DataRowToModel(reader);
                    wechats.Add(wechat);
                }
            }

            return wechats;
        }

        /// <summary>
        /// 获取一个用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Wechat GetWechatById(int id)
        {
            Wechat people = null;
            String sql = String.Format("SELECT * FROM wechat WHERE id = {0}", id);
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
        /// 获取某用户微信资料
        /// </summary>
        /// <param name="id">登录用户ID</param>
        /// <returns></returns>
        public static Wechat GetWechatByUserId(int id)
        {
            Wechat wechat = null;
            String sql = String.Format("SELECT * FROM wechat WHERE user_id = {0}", id);
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    wechat = DataRowToModel(reader);
                }
            }
            return wechat;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private static Wechat DataRowToModel(SQLiteDataReader row)
        {
            Wechat model = new Wechat();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.UserId = int.Parse(row["user_id"].ToString());
                }
                if (row["nickname"] != null && row["nickname"].ToString() != "")
                {
                    model.NickName = row["nickname"].ToString();
                }
                if (row["headimgurl"] != null && row["headimgurl"].ToString() != "")
                {
                    model.Headimgurl = row["headimgurl"].ToString();
                }
                if (row["sex"] != null && row["sex"].ToString() != "")
                {
                    model.Gender = "未知";
                    if (Convert.ToInt32(row["sex"].ToString()) == 1) {
                        model.Gender = "男";
                    }else if (Convert.ToInt32(row["sex"].ToString()) == 2) {
                        model.Gender = "女";
                    }
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
    }
}