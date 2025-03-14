using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace StudentManagement.Infrastructures.Interceptors
{
    public class SqlQueriesLoggingInterceptor : DbCommandInterceptor
    {
        private Stopwatch stopwatch = new();

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            stopwatch.Start();

            // using ở đây là chạy xong cái hàm thì lấy lại vùng nhớ đã cấp

            return base.ReaderExecuting(command, eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            stopwatch.Stop();
            var millisecond = stopwatch.ElapsedMilliseconds;
            if (millisecond > 1)
            {
                using StreamWriter writer =
                new("D:\\LearningMaterial\\MyOwnCarrerPath\\C#\\StudentManagement\\sql-queries.txt", append: true);
                writer.WriteLine("[" + millisecond + " ms] " + command.CommandText);
            }
            return base.ReaderExecuted(command, eventData, result);
        }
    }
}