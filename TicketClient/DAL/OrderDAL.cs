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
    public class OrderDAL
    {
        /// <summary>
        /// 获取符合条件的数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的数据集</returns>
        public static List<Order> GetOrderByWhere(String where){
            List<Order> orders = new List<Order>();

            String sql = String.Format("SELECT * FROM orders WHERE {0}", where);
            using(SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql)){
                while(reader.Read()){
                    Order order = DataRowToModel(reader);
                    order.Details = OrderDetailDAL.GetDetailsByWhere("order_id = " + order.Id);
                    orders.Add(order);
                }
            }

            return orders;
        }

        /// <summary>
        /// 获取一个订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Order GetOrder(int id) {
            Order order = null;
            String sql = String.Format("SELECT * FROM orders WHERE id = {0}", id);
            using(SQLiteDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.ConnectionStringLocalTransaction, System.Data.CommandType.Text, sql)){
                if(reader.Read()){
                    order = DataRowToModel(reader);
                }
            }
            return order;
        }

        /// <summary>
        /// 创建一个订单
        /// </summary>
        /// <param name="model">订单数据</param>
        /// <returns></returns>
        public static Boolean Create(Order model)
        {
            Boolean flag = false;
            if(model == null){
                return flag;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into orders(");
            strSql.Append("order_no,order_name,order_body,order_url,payment_type,original_money,money,invoice_title,user_id,from_id,emp_id,order_type,order_status,feedback_status,transport_fee,remark,arrived_at,express_at,sign_at,close_at,refund_at,is_delete,created_at,updated_at,pay_at,feedback_at,buyer_id)");
            strSql.Append(" values (");
            strSql.Append("@order_no,@order_name,@order_body,@order_url,@payment_type,@original_money,@money,@invoice_title,@user_id,@from_id,@emp_id,@order_type,@order_status,@feedback_status,@transport_fee,@remark,@arrived_at,@express_at,@sign_at,@close_at,@refund_at,@is_delete,@created_at,@updated_at,@pay_at,@feedback_at,@buyer_id)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            List<SQLiteParameter> parameters = GetParameters(model);

            int num = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters.ToArray<SQLiteParameter>());
            flag = num > 0;
            return flag;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update orders set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("order_name=@order_name,");
            strSql.Append("order_body=@order_body,");
            strSql.Append("order_url=@order_url,");
            strSql.Append("payment_type=@payment_type,");
            strSql.Append("original_money=@original_money,");
            strSql.Append("money=@money,");
            strSql.Append("invoice_title=@invoice_title,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("from_id=@from_id,");
            strSql.Append("emp_id=@emp_id,");
            strSql.Append("order_type=@order_type,");
            strSql.Append("order_status=@order_status,");
            strSql.Append("feedback_status=@feedback_status,");
            strSql.Append("transport_fee=@transport_fee,");
            strSql.Append("remark=@remark,");
            strSql.Append("arrived_at=@arrived_at,");
            strSql.Append("express_at=@express_at,");
            strSql.Append("sign_at=@sign_at,");
            strSql.Append("close_at=@close_at,");
            strSql.Append("refund_at=@refund_at,");
            strSql.Append("is_delete=@is_delete,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("pay_at=@pay_at,");
            strSql.Append("feedback_at=@feedback_at,");
            strSql.Append("buyer_id=@buyer_id");
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
        private static Order DataRowToModel(SQLiteDataReader row)
        {
            Order model = new Order();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["order_no"] != null)
                {
                    model.No = row["order_no"].ToString();
                }
                if (row["order_name"] != null)
                {
                    model.Name = row["order_name"].ToString();
                }
                if (row["order_body"] != null)
                {
                    model.Body = row["order_body"].ToString();
                }
                if (row["order_url"] != null)
                {
                    model.Url = row["order_url"].ToString();
                }
                if (row["payment_type"] != null)
                {
                    model.Payment = (PaymentType)Enum.Parse(typeof(PaymentType), row["payment_type"].ToString());
                }
                if (row["original_money"] != null && row["original_money"].ToString() != "")
                {
                    model.OriginalMoney = decimal.Parse(row["original_money"].ToString());
                }
                if (row["money"] != null && row["money"].ToString() != "")
                {
                    model.Money = decimal.Parse(row["money"].ToString());
                }
                if (row["invoice_title"] != null)
                {
                    model.InvoiceTitle = row["invoice_title"].ToString();
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.UserId = int.Parse(row["user_id"].ToString());
                }
                if (row["from_id"] != null && row["from_id"].ToString() != "")
                {
                    model.FromId = int.Parse(row["from_id"].ToString());
                }
                if (row["emp_id"] != null && row["emp_id"].ToString() != "")
                {
                    model.EmpId = int.Parse(row["emp_id"].ToString());
                }
                if (row["buyer_id"] != null && row["buyer_id"].ToString() != "")
                {
                    model.BuyerId = int.Parse(row["buyer_id"].ToString());
                }
                if (row["order_type"] != null)
                {
                    model.OrderType = (OrderType)Enum.Parse(typeof(OrderType), row["order_type"].ToString());
                }
                if (row["order_status"] != null)
                {
                    model.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), row["order_status"].ToString());
                }
                if (row["feedback_status"] != null)
                {
                    model.FeedbackStatus = (FeedbackStatus)Enum.Parse(typeof(FeedbackStatus), row["feedback_status"].ToString());
                }
                if (row["transport_fee"] != null && row["transport_fee"].ToString() != "")
                {
                    model.TransportFee = decimal.Parse(row["transport_fee"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.Remark = row["remark"].ToString();
                }
                if (row["arrived_at"] != null && row["arrived_at"].ToString() != "")
                {
                    model.ArrivedAt = model.ConvertIntDateTime(Convert.ToInt32(row["arrived_at"].ToString()));
                }
                if (row["express_at"] != null && row["express_at"].ToString() != "")
                {
                    model.ExpressAt = model.ConvertIntDateTime(Convert.ToInt32(row["express_at"].ToString()));
                }
                if (row["sign_at"] != null && row["sign_at"].ToString() != "")
                {
                    model.SignAt = model.ConvertIntDateTime(Convert.ToInt32(row["sign_at"].ToString()));
                }
                if (row["close_at"] != null && row["close_at"].ToString() != "")
                {
                    model.CloseAt = model.ConvertIntDateTime(Convert.ToInt32(row["close_at"].ToString()));
                }
                if (row["refund_at"] != null && row["refund_at"].ToString() != "")
                {
                    model.RefundAt = model.ConvertIntDateTime(Convert.ToInt32(row["refund_at"].ToString()));
                }
                if (row["is_delete"] != null && row["is_delete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(row["is_delete"].ToString()) == 1;
                }
                if (row["created_at"] != null && row["created_at"].ToString() != "")
                {
                    model.CreatedAt = model.ConvertIntDateTime(Convert.ToInt32(row["created_at"].ToString()));
                }
                if (row["updated_at"] != null && row["updated_at"].ToString() != "")
                {
                    model.UpdatedAt = model.ConvertIntDateTime(Convert.ToInt32(row["updated_at"].ToString()));
                }
                if (row["pay_at"] != null && row["pay_at"].ToString() != "")
                {
                    model.PayAt = model.ConvertIntDateTime(Convert.ToInt32(row["pay_at"].ToString()));
                }
                if (row["feedback_at"] != null && row["feedback_at"].ToString() != "")
                {
                    model.FeedbackAt = model.ConvertIntDateTime(Convert.ToInt32(row["feedback_at"].ToString()));
                }
            }
            return model;
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static List<SQLiteParameter> GetParameters(Order model) {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>() {
					new SQLiteParameter("@order_no", DbType.String),
					new SQLiteParameter("@order_name", DbType.String),
					new SQLiteParameter("@order_body", DbType.String),
					new SQLiteParameter("@order_url", DbType.String),
					new SQLiteParameter("@payment_type", DbType.String),
					new SQLiteParameter("@original_money", DbType.Decimal,4),
					new SQLiteParameter("@money", DbType.Decimal,4),
					new SQLiteParameter("@invoice_title", DbType.String),
					new SQLiteParameter("@user_id", DbType.Int32,8),
					new SQLiteParameter("@from_id", DbType.Int32,8),
					new SQLiteParameter("@emp_id", DbType.Int32,8),
					new SQLiteParameter("@order_type", DbType.String),
					new SQLiteParameter("@order_status", DbType.String),
					new SQLiteParameter("@feedback_status", DbType.String),
					new SQLiteParameter("@transport_fee", DbType.Decimal,4),
					new SQLiteParameter("@remark", DbType.String),
					new SQLiteParameter("@arrived_at", DbType.Int64,8),
					new SQLiteParameter("@express_at", DbType.Int64,8),
					new SQLiteParameter("@sign_at", DbType.Int64,8),
					new SQLiteParameter("@close_at", DbType.Int64,8),
					new SQLiteParameter("@refund_at", DbType.Int64,8),
					new SQLiteParameter("@is_delete", DbType.Int32,8),
					new SQLiteParameter("@created_at", DbType.Int64,8),
					new SQLiteParameter("@updated_at", DbType.Int64,8),
					new SQLiteParameter("@pay_at", DbType.Int64,8),
					new SQLiteParameter("@feedback_at", DbType.Int64,8),
                    new SQLiteParameter("@buyer_id", DbType.Int32,8)};
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Body;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Payment.ToString();
            parameters[5].Value = model.OriginalMoney;
            parameters[6].Value = model.Money;
            parameters[7].Value = model.InvoiceTitle;
            parameters[8].Value = model.UserId;
            parameters[9].Value = model.FromId;
            parameters[10].Value = model.EmpId;
            parameters[11].Value = model.OrderType.ToString();
            parameters[12].Value = model.Status.ToString();
            parameters[13].Value = model.FeedbackStatus.ToString();
            parameters[14].Value = model.TransportFee;
            parameters[15].Value = model.Remark;
            parameters[16].Value = model.ConvertDateTimeInt(model.ArrivedAt);
            parameters[17].Value = model.ConvertDateTimeInt(model.ExpressAt);
            parameters[18].Value = model.ConvertDateTimeInt(model.SignAt);
            parameters[19].Value = model.ConvertDateTimeInt(model.CloseAt);
            parameters[20].Value = model.ConvertDateTimeInt(model.RefundAt);
            parameters[21].Value = model.IsDelete ? 1 : 0;
            parameters[22].Value = model.ConvertDateTimeInt(model.CreatedAt);
            parameters[23].Value = model.ConvertDateTimeInt(model.UpdatedAt);
            parameters[24].Value = model.ConvertDateTimeInt(model.PayAt);
            parameters[25].Value = model.ConvertDateTimeInt(model.FeedbackAt);
            parameters[26].Value = model.BuyerId;
            return parameters;
        }
    }
}
