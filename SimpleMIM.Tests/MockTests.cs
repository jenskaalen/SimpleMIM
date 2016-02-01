using Microsoft.MetadirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class MockTests
    {
        [TestMethod]
        public void MockMventry_has_settable_attributes()
        {
            MVEntry mventry = new MockMventry();
            mventry["uid"].Value = "testman";
            mventry["fafa"].Value = "fafa";

            Assert.AreEqual("testman", mventry["uid"].Value);
        }
    }
}
