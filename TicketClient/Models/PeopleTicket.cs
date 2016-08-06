using System;

namespace Models
{
    public class PeopleTicket : Model
    {
        private int id;
        private int userId;
        private int orderId;
        private DateTime date;
        private int num;
        private String status;
        private DateTime createdAt;
        private DateTime updatedAt;
        private People people;
        private Member member;
        private Wechat wechat;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
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

        public int OrderId
        {
            get
            {
                return orderId;
            }

            set
            {
                orderId = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
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

        public People People
        {
            get
            {
                return people;
            }

            set
            {
                people = value;
            }
        }

        public Member Member
        {
            get
            {
                return member;
            }

            set
            {
                member = value;
            }
        }

        public Wechat Wechat
        {
            get
            {
                return wechat;
            }

            set
            {
                wechat = value;
            }
        }
    }
}