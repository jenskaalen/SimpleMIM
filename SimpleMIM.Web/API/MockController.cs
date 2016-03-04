using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Common.MockTypes;
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
            mockAttribs.Add(new MIMAttribute("Address") { Value = "Eksempelveien 12" });
            mockAttribs.Add(new MIMAttribute("DepartmentID") { Value = "Fabrikam" });

            return mockAttribs;
        }

        [HttpGet]
        public MockMventry GetMVEntryMock()
        {
            var mock = new MockMventry();
            mock["FirstName"].Value = "Petrus";
            mock["LastName"].Value = "Nordtygg";
            mock["Address"].Value = "Eksempelveien 12";
            mock["DepartmentID"].Value = "Fabrikam";

            return mock;
        }
    }
}
