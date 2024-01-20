using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTerm_Project_EMS
{

    public static class InsertLogs
    {
        public static void AddLogs(int employeeID, string logs, int logType )
        {
            EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);
            db.USP_INSERT_LOGS(employeeID, logs, logType);
        }
    }
}
