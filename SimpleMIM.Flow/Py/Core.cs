using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace SimpleMIM.Flow.Py
{
    public static class Core
    {
        private static ScriptEngine _engine;
        private static ScriptScope _flowScope;
        private static ScriptScope _provScope;
        private static bool _init;

        private static void Init()
        {
            _engine = Python.CreateEngine();
            _flowScope = _engine.CreateScope();
            _provScope = _engine.CreateScope();
            _init = true;
        }

        public static void RegisterFlowScript(string script)
        {
            if (_init)
                Init();

            _engine.Execute(script, _flowScope);
        }

        public static void RegisterProvisionScript(string script)
        {
            if (_init)
                Init();

            _engine.Execute(script, _provScope);
        }

        public static dynamic GetFlowFunction(string name)
        {
            if (_init)
                Init();

            return _flowScope.GetVariable(name);
        }

        public static dynamic GetProvisionFunction(string name)
        {
            if (_init)
                Init();

            return _provScope.GetVariable(name);
        }
    }
}
