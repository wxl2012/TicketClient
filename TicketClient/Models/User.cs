using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : Model
    {

        public User() { }

        public User(JObject json) {
            this.username = json["username"].ToString();
            this.password = json["password"].ToString();
            this.email = json["email"].ToString();
            this.groupId = Convert.ToInt32(json["group_id"].ToString());
            this.id = Convert.ToInt32(json["id"].ToString());
            this.updatedAt = this.ConvertIntDateTime(Convert.ToInt32(json["updated_at"]));
            this.createdAt = this.ConvertIntDateTime(Convert.ToInt32(json["created_at"]));
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String username;

        public String Username
        {
            get { return username; }
            set { username = value; }
        }
        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private int groupId;

        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
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

        private People people;

        public People People
        {
            get { return people; }
            set { people = value; }
        }
    }
}
