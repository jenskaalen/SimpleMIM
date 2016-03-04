using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMIM.PythonConfiguration.Py;

namespace MIMSimplifier.Tests.Web
{
    [TestClass]
    public class PythonTests
    {
        [TestMethod]
        public void PythonCompilation_works()
        {
            throw new NotImplementedException();
            //var pyCompile = new PythonCompilation();
            //pyCompile.Script = "2 + 4";
            //pyCompile.Name = "PlzWork";

            //string func = FuncCreator.GenerateFunction(pyCompile.Name, null, pyCompile.Script);
            //Core.RegisterFlowScript(func);

            //Assert.AreEqual(6, Core.GetFlowFunction("PlzWork")());
        }
    }
}
