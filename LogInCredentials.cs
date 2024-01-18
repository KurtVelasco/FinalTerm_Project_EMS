using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FinalTerm_Project_EMS
{
    internal static class LogInCredentials 
    {
        public static string EMPLOYEE_EMAIL = string.Empty;
        public static int EMPLOYEE_ID = -1;
        public static string EMPLOYEE_NAME = string.Empty;
        public static string EMPLOYEE_DEPARTMENT = string.Empty;
        public static string EMPLOYEE_POSITION = string.Empty;
        public static bool ATTENDANCE { get; set; } = false;

        public static void SetData(string email, int id, string name, string department, string position)
        {
            EMPLOYEE_EMAIL = email;
            EMPLOYEE_ID = id;
            EMPLOYEE_NAME = name;
            EMPLOYEE_DEPARTMENT = department;
            EMPLOYEE_POSITION = position;
        }
        public static void ResetData()
        {
            EMPLOYEE_EMAIL = string.Empty;
            EMPLOYEE_ID = -1;
            EMPLOYEE_NAME = string.Empty;
            EMPLOYEE_DEPARTMENT = string.Empty;
            EMPLOYEE_POSITION = string.Empty;
        }
        public static void GiveData()
        {

        }
    }
}