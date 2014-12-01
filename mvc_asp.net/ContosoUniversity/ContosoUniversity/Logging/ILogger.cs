using System;


namespace ContosoUniversity.Logging
{
    public interface ILogger
    {
        // three tracing levels
        void Information(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);
        
        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);
      
        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        // tracing level for latency information for external service calls, e.g. db queries
        void TraceApi(string componentName, string method, TimeSpan timeSpan);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string fmt, params object[] vars);
       
    }
}