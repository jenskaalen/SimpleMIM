using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMIM.PythonConfiguration.Py;
using SimpleMIM.Web.Models;

namespace MIMSimplifier.Tests.Web
{
    [TestClass]
    public class PythonTests
    {
        [TestMethod]
        public void PythonCompilation_works()
        {
            var pyCompile = new PythonCompilation();
            pyCompile.Script = "2 + 4";
            pyCompile.Name = "PlzWork";

            string func = FuncCreator.GenerateFunction(pyCompile.Name, null, pyCompile.Script);
            Core.RegisterFlowScript(func);

            Assert.AreEqual(6, Core.GetFlowFunction("PlzWork")());
        }
    }
}
