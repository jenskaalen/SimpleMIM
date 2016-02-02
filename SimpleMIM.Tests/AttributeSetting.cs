using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIMSimplifier.Tests.MockTypes;
using SimpleMIM.Provision;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class AttributeSetting
    {
        [TestMethod]
        public void AdvancedAttributeSetter_handles_multiple_attributes()
        {
            string replaceFormat = @"CN=[displayName],OU=Auto Users,OU=Employees,OU=[department],DC=TEST,DC=INT";
            const string expectedResult = @"CN=Test mannen,OU=Auto Users,OU=Employees,OU=Test Department,DC=TEST,DC=INT";

            Attrib displayName = new MockAttrib("displayName");
            displayName.Value = "Test mannen";
            Attrib department = new MockAttrib("department");
            department.Value = "Test Department";

            string result = AttributeFormatter.FormatAttribute(replaceFormat, displayName, department);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
