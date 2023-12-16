using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTerm_Project_EMS
{
    internal class README
    {
        //This Class is stritly for our coding convention, on how were writing our code
        ////
        ///

        //Naming Conventions
        //
        //When making a file (.cs .xaml .dblml etc) = UpperCaseCamel
        //Class1.cs/
        //

        //Local Variable Name / Parameters =  lowerCaseCamel()
        // Example
        int number = 0;
        int primeNumbers = 0;

        //Global Variable Name =  UPPERCASE_UNDERSCORE()
        // Example
        int NUMBER = 0;
        int PRIME_NUMBERS = 0;

        //Method names = UpperCaseCamel()
        //Class names = UpperCaseCamel()
        //Example
        private void GetPrime(int x , int y)
        {
            number = x;
        }
    }
}
