using HomeWork_07_SKP.Data.Repositories;
using HomeWork_07_SKP.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CM = HomeWork_07_SKP.Data.Models.UserConsoleMethods;

namespace HomeWork_07_SKP
{
    class Program
    {
        static void Main(string[] args)
        {               
            //var path = SetPath(ConsoleMethods.WelcomeScreen());
            var path = CM.SetPath(CM.WelcomeScreen());
            Diary myDiary = new Diary(path);            
            CM.ShowMenu(myDiary);                       
        }
        
    }
}
