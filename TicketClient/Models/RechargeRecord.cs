using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RechargeRecord
    {
        public RechargeRecord() { }

        //public RechargeRecord() { }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        private String body;

        public String Body
        {
            get { return body; }
            set { body = value; }
        }
        private int fee;

        public int Fee
        {
            get { return fee; }
            set { fee = value; }
        }
        private int balance;

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        private int createdAt;

        public int CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }
        private int updatedAt;

        public int UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }
        
    }
}
