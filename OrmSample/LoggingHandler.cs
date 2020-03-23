using ECode.Logging;

namespace Sample1
{
    public static class LoggingHandler
    {
        static Logger   DbLog   = LogManager.GetLogger("DB");


        public static void AddDbLog(ECode.Data.LogEntry entry)
        {
            if (entry.Exception == null)
            {
                DbLog.Info(entry.CommandText, new { entry.Server, entry.Database, entry.TableName, entry.SessionID, entry.TransactionID });
            }
            else
            {
                DbLog.Error(entry.CommandText, new { entry.Server, entry.Database, entry.TableName, entry.SessionID, entry.TransactionID }, entry.Exception);
            }
        }
    }
}