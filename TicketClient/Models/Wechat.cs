using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Wechat : Model
    {
        private int userId;
        private DateTime createdAt;
        private DateTime updatedAt;
        private String headimgurl;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String nickName;

        public String NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
        private String gender;

        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return createdAt;
            }

            set
            {
                createdAt = value;
            }
        }

        public DateTime UpdatedAt
        {
            get
            {
                return updatedAt;
            }

            set
            {
                updatedAt = value;
            }
        }

        public string Headimgurl
        {
            get
            {
                return headimgurl;
            }

            set
            {
                headimgurl = value;
            }
        }
    }
}
