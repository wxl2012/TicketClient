using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order : Model
    {
        public Order() { }

        public Order(JObject json) {
            this.id = Convert.ToInt32(json["id"]);
            this.no = json["order_no"].ToString();
            this.money = Convert.ToDecimal(json["money"].ToString());
            this.orderType = (OrderType)Enum.Parse(typeof(OrderType), json["order_type"].ToString(), true);
            this.originalMoney = Convert.ToDecimal(json["original_money"].ToString());
            this.payAt = this.ConvertIntDateTime(Convert.ToInt64(json["pay_at"].ToString()));
            this.expressAt = this.ConvertIntDateTime(Convert.ToInt64(json["express_at"].ToString()));
            this.arrivedAt = this.ConvertIntDateTime(Convert.ToInt64(json["arrived_at"].ToString()));
            this.closeAt = this.ConvertIntDateTime(Convert.ToInt64(json["close_at"].ToString()));
            this.feedbackAt = this.ConvertIntDateTime(Convert.ToInt64(json["feedback_at"].ToString()));
            this.refundAt = this.ConvertIntDateTime(Convert.ToInt64(json["refund_at"].ToString()));
            this.signAt = this.ConvertIntDateTime(Convert.ToInt64(json["sign_at"].ToString()));
            this.createdAt = this.ConvertIntDateTime(Convert.ToInt64(json["created_at"].ToString()));
            this.updatedAt = this.ConvertIntDateTime(Convert.ToInt64(json["updated_at"].ToString()));
            this.status = (OrderStatus)Enum.Parse(typeof(OrderStatus), json["order_status"].ToString(), true);
            this.buyerId = Convert.ToInt32(json["buyer_id"]);
            this.userId = Convert.ToInt32(json["user_id"]);
            this.empId = Convert.ToInt32(json["emp_id"]);

            this.details = new List<OrderDetail>();
            JArray array = json["details"] as JArray;
            if(array == null){
                return;
            }

            foreach(JObject item in array){
                this.details.Add(new OrderDetail(item));
            }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String no;

        public String No
        {
            get { return no; }
            set { no = value; }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String body;

        public String Body
        {
            get { return body; }
            set { body = value; }
        }
        private String url;

        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        private PaymentType payment;

        public PaymentType Payment
        {
            get { return payment; }
            set { payment = value; }
        }
        private Decimal originalMoney;

        public Decimal OriginalMoney
        {
            get { return originalMoney; }
            set { originalMoney = value; }
        }
        private Decimal money;

        public Decimal Money
        {
            get { return money; }
            set { money = value; }
        }
        private String invoiceTitle;

        public String InvoiceTitle
        {
            get { return invoiceTitle; }
            set { invoiceTitle = value; }
        }
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private int fromId;

        public int FromId
        {
            get { return fromId; }
            set { fromId = value; }
        }
        private int empId;

        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        private int buyerId;

        public int BuyerId
        {
            get { return buyerId; }
            set { buyerId = value; }
        }
        private OrderType orderType;

        public OrderType OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }
        private OrderStatus status;

        public OrderStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        private FeedbackStatus feedbackStatus;

        public FeedbackStatus FeedbackStatus
        {
            get { return feedbackStatus; }
            set { feedbackStatus = value; }
        }
        private Decimal transportFee;

        public Decimal TransportFee
        {
            get { return transportFee; }
            set { transportFee = value; }
        }
        private String remark;

        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private DateTime arrivedAt;

        public DateTime ArrivedAt
        {
            get { return arrivedAt; }
            set { arrivedAt = value; }
        }
        private DateTime expressAt;

        public DateTime ExpressAt
        {
            get { return expressAt; }
            set { expressAt = value; }
        }
        private DateTime signAt;

        public DateTime SignAt
        {
            get { return signAt; }
            set { signAt = value; }
        }
        private DateTime closeAt;

        public DateTime CloseAt
        {
            get { return closeAt; }
            set { closeAt = value; }
        }
        private DateTime refundAt;

        public DateTime RefundAt
        {
            get { return refundAt; }
            set { refundAt = value; }
        }
        private bool isDelete;

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }
        private DateTime createdAt;

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }
        private DateTime updatedAt;

        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }
        private DateTime payAt;

        public DateTime PayAt
        {
            get { return payAt; }
            set { payAt = value; }
        }
        private DateTime feedbackAt;

        public DateTime FeedbackAt
        {
            get { return feedbackAt; }
            set { feedbackAt = value; }
        }
        private decimal totalFee;

        public decimal TotalFee
        {
            get { return totalFee; }
            set { totalFee = value; }
        }
        private List<OrderDetail> details;

        public List<OrderDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
