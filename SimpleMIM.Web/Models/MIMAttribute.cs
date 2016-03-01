using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMIM.Web.Models
{
    public class MIMAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public MIMAttribute()
        {
            
        }

        public MIMAttribute(string name)
        {
            Name = name;
        }
    }
}