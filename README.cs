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

        //General Rules
        // Comment your name in the Class your working on 


        // 1) When writing code make sure you can actually explain/know what your code does (without notes), as sir Julian may have you explain it
        //if your planning to use an A.I Tools remember to only code that you actually understand. (He smells fear) 
        //
        

        // 2) Avoid using comments, only use comments for organization or a brief explaination on what a function does
        // Don't write comments on how your code works as you should be able to do it, verbally
        // There will be a doc available where you can detail your code.

        // 3) Try to stick to your own class/xaml that your are currently working or have added, if you need to edit someone else
        // class, then talk to them first


        //Naming Conventions (Not really strict but for clarity)
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
