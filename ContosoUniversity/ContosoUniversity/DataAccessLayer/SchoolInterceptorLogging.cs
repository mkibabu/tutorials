using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;
using System.Diagnostics;

using ContosoUniversity.Logging;

namespace ContosoUniversity.DataAccessLayer
{
    public class SchoolInterceptorLogging : DbCommandInterceptor
    {
        private ILogger _logger = new Logger();
        private readonly Stopwatch _stopWatch = new Stopwatch();

        /// <summary>
        /// Intercept a scalar command (i.e. a command designed to retrieve a 
        /// single value from the database, rather than a collection of values)
        /// before it executes on the database . Starts the stopwatch, in order
        /// to stop it when the method finishes executing and therefore get its
        /// run duration.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopWatch.Restart();
        }

        /// <summary>
        /// Intercept a scalar command (i.e. a command designed to retrieve a 
        /// single value from the database, rather than a collection of values)
        /// after it executes on the database . Stops the stopwatch, in order
        /// to get its run duration, and logs the timespan or any exception.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopWatch.Stop();

            if(interceptionContext.Exception != null)
            {
                _logger.Error(interceptionContext.Exception, "Error exxecuting command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ScalarExecuted",
                    _stopWatch.Elapsed, "Command: {0}", command.CommandText);
            }

 	        base.ScalarExecuted(command, interceptionContext);
        }

        /// <summary>
        /// Intercept a non-query command (i.e. a command designed to insert or
        /// update the database) before it executes on the database . Starts the
        /// stopwatch, in order to stop it when the method finishes executing 
        /// and therefore get its run duration.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopWatch.Restart();
        }

        /// <summary>
        /// Intercept a non-query command (i.e. a command designed to insert or
        /// update the database) after it executes on the database . Stops the 
        /// stopwatch, in order to get its run duration, and logs the timespan 
        /// or any exception.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopWatch.Stop();

            if(interceptionContext.Exception != null)
            {
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "SchoolInterceptor.NonQueryExecuted",
                    _stopWatch.Elapsed, "Command: {0}", command.CommandText);
            }
            
            base.NonQueryExecuted(command, interceptionContext);
        }

        /// <summary>
        /// Intercept a reader command (i.e. a command designed to retrieve a 
        /// set of values from the database, rather than a single value)
        /// before it executes on the database . Starts the stopwatch, in order
        /// to stop it when the method finishes executing and therefore get its
        /// run duration.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopWatch.Restart();
        }

        /// <summary>
        /// Intercept a reader command (i.e. a command designed to retrieve a 
        /// set of values from the database, rather than a single value)
        /// after it executes on the database . Stops the stopwatch, in order
        /// to get its run duration, and logs the timespan or any exception.
        /// </summary>
        /// <param name="command">The SQL command to intercept</param>
        /// <param name="interceptionContext">The contextual information relating
        /// to the command being intercepted.</param>
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopWatch.Stop();

            if(interceptionContext.Exception != null)
            {
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ReaderExecuted",
                    _stopWatch.Elapsed, "Command: {0}", command.CommandText);
            }
            
            base.ReaderExecuted(command, interceptionContext);
        }
    }
}