using System;

namespace Open.Core
{
    internal static class BrowserConsole
    {
        public static void WriteSeverity(object message, LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Success:
                case LogSeverity.Info:
                    Script.Literal("if (typeof(console) != 'undefined' && typeof(console.info) == 'function') console.info({0})", message);
                    break;
                case LogSeverity.Debug:
                    Script.Literal("if (typeof(console) != 'undefined' && typeof(console.debug) == 'function') console.debug({0})", message);
                    break;
                case LogSeverity.Warning:
                    Script.Literal("if (typeof(console) != 'undefined' && typeof(console.warn) == 'function') console.warn({0})", message);
                    break;
                case LogSeverity.Error:
                    Script.Literal("if (typeof(console) != 'undefined' && typeof(console.error) == 'function') console.error({0})", message);
                    break;
            }
        }
    }
}
