using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace SimpleMIM.Flow.Py
{
    public static class Core
    {
        private static ScriptEngine _engine;
        private static ScriptScope _flowScope;
        private static ScriptScope _provScope;

        static Core()
        {
            _engine = Python.CreateEngine();
            _flowScope = _engine.CreateScope();
            _provScope = _engine.CreateScope();
        }

        public static void RegisterFlowScript(string script)
        {
            _engine.Execute(script, _flowScope);
        }

        public static void RegisterProvisionScript(string script)
        {
            _engine.Execute(script, _provScope);
        }

        public static dynamic GetFlowFunction(string name)
        {
            return _flowScope.GetVariable(name);
        }

        public static dynamic GetProvisionFunction(string name)
        {
            return _provScope.GetVariable(name);
        }
    }
}
