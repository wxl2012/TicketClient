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
    public class PeopleDAL
    {
        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<People> GetPeopleByWhere(String where){
            List<People> peoples = new List<People>();

            String sql = String.Format("SELECT * FROM peoples WHERE {0}", where);
            using(SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql)){
                while(reader.Read()){
                    People people = DataRowToModel(reader);
                    peoples.Add(people);
                }
            }

            return peoples;
        }

        /// <summary>
        /// 获取一个用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static People GetPeople(int id) {
            People people = null;
            String sql = String.Format("SELECT * FROM peoples WHERE id = {0}", id);
            using(SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql)){
                if(reader.Read()){
                    people = DataRowToModel(reader);
                }
            }
            return people;
        }

        /// <summary>
        /// 获取一个用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static People GetPeopleByUserId(int id)
        {
            People people = null;
            String sql = String.Format("SELECT * FROM peoples WHERE user_id = {0}", id);
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
        /// 创建一个订单
        /// </summary>
        /// <param name="model">订单数据</param>
        /// <returns></returns>
        public static Boolean Create(People model)
        {
            Boolean flag = false;
            if(model == null){
                return flag;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into peoples(");
            strSql.Append("user_id,first_name,last_name,alias,gender,birthday,age,identity,photo,country_id,province_id,city_id,county_id,address,phone,tel,email,qq,msn,weight,height,hobby,interest,introduce,motto,created_at,updated_at)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@first_name,@last_name,@alias,@gender,@birthday,@age,@identity,@photo,@country_id,@province_id,@city_id,@county_id,@address,@phone,@tel,@email,@qq,@msn,@weight,@height,@hobby,@interest,@introduce,@motto,@created_at,@updated_at)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            List<SQLiteParameter> parameters = GetParameters(model);
            SQLiteParameter idParams = new SQLiteParameter("@id", DbType.Int32, 8);

            int num = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray<SQLiteParameter>());
            flag = num > 0;
            return flag;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(People model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update peoples set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("first_name=@first_name,");
            strSql.Append("last_name=@last_name,");
            strSql.Append("alias=@alias,");
            strSql.Append("gender=@gender,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("age=@age,");
            strSql.Append("identity=@identity,");
            strSql.Append("photo=@photo,");
            strSql.Append("country_id=@country_id,");
            strSql.Append("province_id=@province_id,");
            strSql.Append("city_id=@city_id,");
            strSql.Append("county_id=@county_id,");
            strSql.Append("address=@address,");
            strSql.Append("phone=@phone,");
            strSql.Append("tel=@tel,");
            strSql.Append("email=@email,");
            strSql.Append("qq=@qq,");
            strSql.Append("msn=@msn,");
            strSql.Append("weight=@weight,");
            strSql.Append("height=@height,");
            strSql.Append("hobby=@hobby,");
            strSql.Append("interest=@interest,");
            strSql.Append("introduce=@introduce,");
            strSql.Append("motto=@motto,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("updated_at=@updated_at");
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
        private static People DataRowToModel(SQLiteDataReader row)
        {
            People model = new People();
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
                if (row["first_name"] != null)
                {
                    model.FirstName = row["first_name"].ToString();
                }
                if (row["last_name"] != null)
                {
                    model.LastName = row["last_name"].ToString();
                }
                if (row["alias"] != null)
                {
                    model.Alias = row["alias"].ToString();
                }
                if (row["gender"] != null)
                {
                    model.Gender = row["gender"].ToString();
                }
                if (row["birthday"] != null && row["birthday"].ToString() != "")
                {
                    model.Birthday = model.ConvertIntDateTime(int.Parse(row["birthday"].ToString()));
                }
                if (row["age"] != null && row["age"].ToString() != "")
                {
                    model.Age = int.Parse(row["age"].ToString());
                }
                if (row["identity"] != null)
                {
                    model.Identity = row["identity"].ToString();
                }
                if (row["photo"] != null)
                {
                    model.Photo = row["photo"].ToString();
                }
                if (row["country_id"] != null && row["country_id"].ToString() != "")
                {
                    model.CountryId = int.Parse(row["country_id"].ToString());
                }
                if (row["province_id"] != null && row["province_id"].ToString() != "")
                {
                    model.ProvinceId = int.Parse(row["province_id"].ToString());
                }
                if (row["city_id"] != null && row["city_id"].ToString() != "")
                {
                    model.CityId = int.Parse(row["city_id"].ToString());
                }
                if (row["county_id"] != null && row["county_id"].ToString() != "")
                {
                    model.CountyId = int.Parse(row["county_id"].ToString());
                }
                if (row["address"] != null)
                {
                    model.Address = row["address"].ToString();
                }
                if (row["phone"] != null)
                {
                    model.Phone = row["phone"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.Tel = row["tel"].ToString();
                }
                if (row["email"] != null)
                {
                    model.Email = row["email"].ToString();
                }
                if (row["qq"] != null)
                {
                    model.Qq = row["qq"].ToString();
                }
                if (row["msn"] != null)
                {
                    model.Msn = row["msn"].ToString();
                }
                if (row["weight"] != null && row["weight"].ToString() != "")
                {
                    model.Weight = Double.Parse(row["weight"].ToString());
                }
                if (row["height"] != null && row["height"].ToString() != "")
                {
                    model.Height = Double.Parse(row["height"].ToString());
                }
                if (row["hobby"] != null)
                {
                    model.Hobby = row["hobby"].ToString();
                }
                if (row["interest"] != null)
                {
                    model.Interest = row["interest"].ToString();
                }
                if (row["introduce"] != null)
                {
                    model.Introduce = row["introduce"].ToString();
                }
                if (row["motto"] != null)
                {
                    model.Motto = row["motto"].ToString();
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
        /// 获取参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static List<SQLiteParameter> GetParameters(People model)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>() {
					new SQLiteParameter("@user_id", DbType.Int32,8),
					new SQLiteParameter("@first_name", DbType.String),
					new SQLiteParameter("@last_name", DbType.String),
					new SQLiteParameter("@alias", DbType.String),
					new SQLiteParameter("@gender", DbType.String),
					new SQLiteParameter("@birthday", DbType.Int64,8),
					new SQLiteParameter("@age", DbType.Int32,8),
					new SQLiteParameter("@identity", DbType.String),
					new SQLiteParameter("@photo", DbType.String),
					new SQLiteParameter("@country_id", DbType.Int32,8),
					new SQLiteParameter("@province_id", DbType.Int32,8),
					new SQLiteParameter("@city_id", DbType.Int32,8),
					new SQLiteParameter("@county_id", DbType.Int32,8),
					new SQLiteParameter("@address", DbType.String),
					new SQLiteParameter("@phone", DbType.String),
					new SQLiteParameter("@tel", DbType.String),
					new SQLiteParameter("@email", DbType.String),
					new SQLiteParameter("@qq", DbType.String),
					new SQLiteParameter("@msn", DbType.String),
					new SQLiteParameter("@weight", DbType.Decimal,4),
					new SQLiteParameter("@height", DbType.Decimal,4),
					new SQLiteParameter("@hobby", DbType.String),
					new SQLiteParameter("@interest", DbType.String),
					new SQLiteParameter("@introduce", DbType.String),
					new SQLiteParameter("@motto", DbType.String),
					new SQLiteParameter("@created_at", DbType.Int64,8),
					new SQLiteParameter("@updated_at", DbType.Int64,8)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.FirstName;
            parameters[2].Value = model.LastName;
            parameters[3].Value = model.Alias;
            parameters[4].Value = model.Gender;
            parameters[5].Value = model.ConvertDateTimeInt(model.Birthday);
            parameters[6].Value = model.Age;
            parameters[7].Value = model.Identity;
            parameters[8].Value = model.Photo;
            parameters[9].Value = model.CountryId;
            parameters[10].Value = model.ProvinceId;
            parameters[11].Value = model.CityId;
            parameters[12].Value = model.CountyId;
            parameters[13].Value = model.Address;
            parameters[14].Value = model.Phone;
            parameters[15].Value = model.Tel;
            parameters[16].Value = model.Email;
            parameters[17].Value = model.Qq;
            parameters[18].Value = model.Msn;
            parameters[19].Value = model.Weight;
            parameters[20].Value = model.Height;
            parameters[21].Value = model.Hobby;
            parameters[22].Value = model.Interest;
            parameters[23].Value = model.Introduce;
            parameters[24].Value = model.Motto;
            parameters[25].Value = model.ConvertDateTimeInt(model.CreatedAt);
            parameters[26].Value = model.ConvertDateTimeInt(model.UpdatedAt);
            return parameters;
        }
    }
}
