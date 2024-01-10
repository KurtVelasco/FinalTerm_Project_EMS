using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FinalTerm_Project_EMS
{
    internal class LogInCredentials
    {
        public static string EMPLOYEE_EMAIL = string.Empty;
        public static string EMPLOYEE_ID = string.Empty;
        public static string EMPLOYEE_NAME = string.Empty;
        public static string EMPLOYEE_DEPARTMENT = string.Empty;
        public static string EMPLOYEE_POSITION = string.Empty;
        public static void SetData(string email, string id, string name, string department, string position)
        {
            EMPLOYEE_EMAIL = email;
            EMPLOYEE_ID = id;
            EMPLOYEE_NAME = name;
            EMPLOYEE_DEPARTMENT = department;
            EMPLOYEE_POSITION = position;
        }
        public static void GiveData()
        {

        }
    }
}