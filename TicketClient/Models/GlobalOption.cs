using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GlobalOption : Model
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String key;

        public String Key
        {
            get { return key; }
            set { key = value; }
        }
        private String value;

        public String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private String desc;

        public String Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        private Boolean visible;

        public Boolean Visible
        {
            get { return visible; }
            set { visible = value; }
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
    }
}
