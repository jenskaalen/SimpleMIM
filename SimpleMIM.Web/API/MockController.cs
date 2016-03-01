using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleMIM.Web.Models;

namespace SimpleMIM.Web.API
{
    public class MockController : ApiController
    {
        [HttpGet]
        public List<MIMAttribute> GetMockAttribs()
        {
            var mockAttribs = new List<MIMAttribute>();
            mockAttribs.Add(new MIMAttribute("FirstName") { Value = "Petrus" });
            mockAttribs.Add(new MIMAttribute("LastName") { Value = "Nordtygg" });
            mockAttribs.Add(new MIMAttribute("DepartmentID") { Value = "Fabrikam" });

            return mockAttribs;
        }
    }
}
