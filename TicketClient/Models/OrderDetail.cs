using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderDetail : Model
    {
        public OrderDetail() { }

        public OrderDetail(JObject json) {
            this.id = Convert.ToInt32(json["id"]);
            this.color = json["color"].ToString();
            this.price = Convert.ToDecimal(json["price"].ToString());
            this.num = Convert.ToInt32(json["num"].ToString());
            this.goodsId = Convert.ToInt32(json["goods_id"].ToString());
            this.orderId = Convert.ToInt32(json["order_id"].ToString());
            this.remark = json["remark"].ToString();
            this.setMeal = json["set_meal"].ToString();
            this.size = json["size"].ToString();
            this.specId = Convert.ToInt32(json["spec_id"].ToString());
            this.useFlag = Convert.ToInt32(json["use_flag"].ToString()) == 1;
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int orderId;

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        private int goodsId;

        public int GoodsId
        {
            get { return goodsId; }
            set { goodsId = value; }
        }
        private int specId;

        public int SpecId
        {
            get { return specId; }
            set { specId = value; }
        }
        private Decimal price;

        public Decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        private String size;

        public String Size
        {
            get { return size; }
            set { size = value; }
        }
        private String color;

        public String Color
        {
            get { return color; }
            set { color = value; }
        }
        private String setMeal;

        public String SetMeal
        {
            get { return setMeal; }
            set { setMeal = value; }
        }
        private String remark;

        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private Boolean useFlag;

        public Boolean UseFlag
        {
            get { return useFlag; }
            set { useFlag = value; }
        }
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
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

        private DateTime lastUpdatedAt;

        public DateTime LastUpdatedAt
        {
            get { return lastUpdatedAt; }
            set { lastUpdatedAt = value; }
        }
    }
}
