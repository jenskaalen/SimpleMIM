using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMIM.Web.Models
{
    public class PythonFunctionTest
    {
        public string Name { get; set; }
        public List<MIMAttribute> Attribs { get; set; }
        public string ObjectType { get; set; }
    }
}