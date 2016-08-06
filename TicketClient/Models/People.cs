using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class People : Model
    {
        public People() { }

        public People(JObject json) {
            if(json == null){
                return;
            }
            this.birthday = this.ConvertIntDateTime(Convert.ToInt64(json["birthday"]));
            this.id = Convert.ToInt32(json["id"]);
            this.userId = Convert.ToInt32(json["user_id"]);
            this.nickName = json["nickname"].ToString();
            this.gender = json["gender"].ToString();
            this.identity = json["identity"].ToString();
            this.phone = json["phone"].ToString();
            this.firstName = json["first_name"].ToString();
            this.lastName = json["last_name"].ToString();
            this.photo = json["photo"].ToString();
            this.age = Convert.ToInt32(json["age"].ToString());
            this.address = json["address"].ToString();
            this.tel = json["tel"].ToString();
            this.email = json["email"].ToString();
            this.qq = json["qq"].ToString();
            this.msn = json["msn"].ToString();
            this.hobby = json["hobby"].ToString();
            this.interest = json["interest"].ToString();
            this.motto = json["motto"].ToString();
            this.createdAt = this.ConvertIntDateTime(Convert.ToInt64(json["created_at"]));
            this.updatedAt = this.ConvertIntDateTime(Convert.ToInt64(json["updated_at"]));
        }

        private Member member;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private String alias;

        public String Alias
        {
            get { return alias; }
            set { alias = value; }
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
        private DateTime birthday;

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        private String identity;

        public String Identity
        {
            get { return identity; }
            set { identity = value; }
        }
        private String photo;

        public String Photo
        {
            get { return photo; }
            set { photo = value; }
        }
        private int countryId;

        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }
        private int provinceId;

        public int ProvinceId
        {
            get { return provinceId; }
            set { provinceId = value; }
        }
        private int cityId;

        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
        private int countyId;

        public int CountyId
        {
            get { return countyId; }
            set { countyId = value; }
        }
        private Region country;

        public Region Country
        {
            get { return country; }
            set { country = value; }
        }
        private Region province;

        public Region Province
        {
            get { return province; }
            set { province = value; }
        }
        private Region city;

        public Region City
        {
            get { return city; }
            set { city = value; }
        }
        private Region county;

        public Region County
        {
            get { return county; }
            set { county = value; }
        }
        private String address;

        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private String tel;

        public String Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private String qq;

        public String Qq
        {
            get { return qq; }
            set { qq = value; }
        }
        private String msn;

        public String Msn
        {
            get { return msn; }
            set { msn = value; }
        }
        private Double weight;

        public Double Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private Double height;

        public Double Height
        {
            get { return height; }
            set { height = value; }
        }
        private String hobby;

        public String Hobby
        {
            get { return hobby; }
            set { hobby = value; }
        }
        private String interest;

        public String Interest
        {
            get { return interest; }
            set { interest = value; }
        }
        private String introduce;

        public String Introduce
        {
            get { return introduce; }
            set { introduce = value; }
        }
        private String motto;

        public String Motto
        {
            get { return motto; }
            set { motto = value; }
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

        private Wechat wechat;

        public Wechat Wechat
        {
            get { return wechat; }
            set { wechat = value; }
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
    }
}
