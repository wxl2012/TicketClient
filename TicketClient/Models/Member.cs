using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Member : Model
    {
        public Member() { }

        public Member(JObject json) {
            if(json == null){
                return;
            }
            this.id = Convert.ToInt32(json["id"].ToString());
            this.sellerId = Convert.ToInt32(json["seller_id"].ToString());
            this.userId = Convert.ToInt32(json["user_id"].ToString());
            this.no = json["no"].ToString();
            this.realNo = json["real_no"].ToString();
            this.levelId = Convert.ToInt32(json["level_id"].ToString());
            this.score = Convert.ToInt32(json["score"].ToString());
            this.money = Convert.ToDecimal(json["money"].ToString());
            this.remark = json["remark"].ToString();
            this.from = (RegFrom)Enum.Parse(typeof(RegFrom), json["reg_from"].ToString());
            this.status = (Status)Enum.Parse(typeof(Status), json["status"].ToString());
            this.isNew = json["is_new"].ToString() == "1";
            this.isDelete = json["is_delete"].ToString() == "1";
            this.recommendId = Convert.ToInt32(json["recommend_id"]);
            this.createdAt = this.ConvertIntDateTime(Convert.ToInt32(json["created_at"]));
            this.updatedAt = this.ConvertIntDateTime(Convert.ToInt32(json["updated_at"]));
            this.lastAt = this.ConvertIntDateTime(Convert.ToInt64(json["last_at"]));
            if(json["people"] != null){
                this.people = new People(json["people"] as JObject);
            }
            this.orders = new List<Order>();
            JArray items = json["orders"] as JArray;
            if(items != null){
                foreach (JObject item in items) {
                    this.orders.Add(new Order(item));
                }
            }
            
            
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int sellerId;

        public int SellerId
        {
            get { return sellerId; }
            set { sellerId = value; }
        }
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private String no;

        public String No
        {
            get { return no; }
            set { no = value; }
        }
        private String realNo;

        public String RealNo
        {
            get { return realNo; }
            set { realNo = value; }
        }
        private int levelId;

        public int LevelId
        {
            get { return levelId; }
            set { levelId = value; }
        }
        private int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        private Decimal money;

        public Decimal Money
        {
            get { return money; }
            set { money = value; }
        }
        private Decimal giftMoney;

        public Decimal GiftMoney
        {
            get { return giftMoney; }
            set { giftMoney = value; }
        }
        private RegFrom from;

        public RegFrom From
        {
            get { return from; }
            set { from = value; }
        }
        private String remark;

        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private Boolean isNew;

        public Boolean IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
        private Boolean isDelete;

        public Boolean IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }
        private int recommendId;

        public int RecommendId
        {
            get { return recommendId; }
            set { recommendId = value; }
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

        private DateTime lastAt;

        public DateTime LastAt
        {
            get { return lastAt; }
            set { lastAt = value; }
        }
        
        private People people;

        public People People
        {
            get { return people; }
            set { people = value; }
        }

        private Level level;

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        private People recommend;

        public People Recommend
        {
            get { return recommend; }
            set { recommend = value; }
        }

        private List<Order> orders;

        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
    }
}
