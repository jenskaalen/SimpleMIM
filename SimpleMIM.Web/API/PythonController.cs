using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleMIM.Common.MockTypes;
using SimpleMIM.PythonConfiguration.Py;
using SimpleMIM.Web.Models;

namespace SimpleMIM.Web.API
{
    public class PythonController : ApiController
    {
        [HttpPost]
        public void Compile(PythonCompilation compilation)
        {
            string func = FuncCreator.GenerateFunction(compilation.Name, "entry", compilation.Script);
            Core.RegisterFlowScript(func);
        }
        
        [HttpPost]
        public object Test(PythonFunctionTest test)
        {
            var func = Core.GetFlowFunction(test.Name);
            var mventryMock = new MockMventry(test.ObjectType);

            foreach (var attrib in test.Attribs)
            {
                mventryMock[attrib.Name].Value = attrib.Value;
            }

            return func(mventryMock);
        }
    }
}
