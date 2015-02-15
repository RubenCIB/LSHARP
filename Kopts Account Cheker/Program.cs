

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ini;
using System.Collections;
using System.IO;
using System.Threading;
using System.Net;
using System.Management;



namespace RitoBot
{
    public class Program
    {
       


        public static string Region;
        public static string cutlevel;
        public static int total = 0;
        public static int totalacc = 0;
        public static int Validas = 0;
        public static ArrayList accounts = new ArrayList();
        public static ArrayList accounts2 = new ArrayList();
        public static string cversion = "5.3.15_02_06_18_00";
       
        

        static void Main(string[] args)
        {
            InitChecks();
            loadVersion();
            Console.Title = "KOPT´S ACCOUNT CHECKER";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetWindowSize(Console.WindowWidth + 5, Console.WindowHeight);
            Console.WriteLine("==================================================================================");
            Console.WriteLine("KOPT´S ACCOUNT CHECKER  based on Maufeat´s Volibot");
            Console.WriteLine("==================================================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(getTimestamp() + "LOADING REGION");
            loadConfiguration();
        
            
            Console.WriteLine(getTimestamp() + "LOADING ACCOUNTS LIST");
            loadAccounts();
            Console.WriteLine(getTimestamp() + "ACCOUNTS TO CHECK: "+ totalacc);
            Console.WriteLine(getTimestamp() + "CUT LEVEL: " + cutlevel);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            ReloadAccounts:
            
            
           
                foreach (string acc in accounts)
                {
                    try
                    {
                        accounts2.RemoveAt(0);
                        string Accs = acc;
                        string[] stringSeparators = new string[] { ":" };
                        var result = Accs.Split(stringSeparators, StringSplitOptions.None);

                        Console.Title = "Kopt´s Account Checker | WORKING..";
                            
                            RiotBot ritoBot = new RiotBot(result[0], result[1], Region);
                        
                        
                        
                    }
                    catch (Exception esc)
                    {
                        Console.WriteLine("Please use a correct format (user:password), and try dont use empty lines");
                        totalacc -= 1;

                    }
                }

             
            Console.ReadKey();
        }
        
        public static void loadVersion()
        {

            var versiontxt = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"config\\version.txt");
            cversion = versiontxt.ReadLine();               
        }
     
        public static void loadConfiguration()
        {
            try
            {
                IniFile iniFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config\\settings.ini");
              
                Region = iniFile.IniReadValue("Account", "Region").ToUpper();
                cutlevel = iniFile.IniReadValue("Account", "Cutlevel");
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(10000);
              
            }
        }
        public static void loadAccounts()
        {
            var accountsTxtPath = AppDomain.CurrentDomain.BaseDirectory + "config\\accounts.txt";
            TextReader tr = File.OpenText(accountsTxtPath);
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                if (line.Length > 3)
                {
                    accounts.Add(line);
                    accounts2.Add(line);
                }
                
            }
            tr.Close();
            totalacc = accounts.Count;
            if (totalacc > 1300)
            {
                Console.WriteLine("TOO MUCH ACCOUNTS. DONT USE MORE THAN 1000 ACCOUNTS PER FILE");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        public static String getTimestamp()
        {
           return "[" + DateTime.Now + "] ";
        }
        public static void getColor(ConsoleColor color)
        {
           Console.ForegroundColor = color;
        }
        
       
        private static void InitChecks()
        {
            var theFolder = AppDomain.CurrentDomain.BaseDirectory + @"config\\";
            var accountsTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\accounts.txt";
            var configTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\settings.ini";
            var versionTxtLocation = AppDomain.CurrentDomain.BaseDirectory + @"config\\version.txt";

            if (!Directory.Exists(theFolder))
            {
                Directory.CreateDirectory(theFolder);
            }

            if (!File.Exists(configTxtLocation))
            {
                
                var newfile = File.Create(configTxtLocation);
                newfile.Close();
                var content = "[Account]\nRegion=EUW\nCutlevel=20";
                var separator = new string[] { "\n" };
                string[] contentlines = content.Split(separator,StringSplitOptions.None);
                File.WriteAllLines(configTxtLocation, contentlines);
            }
            if (!File.Exists(versionTxtLocation))
            {
                var newfile = File.CreateText(versionTxtLocation);
                newfile.Close();
                string[] content = { cversion };
                File.WriteAllLines(versionTxtLocation, content);
            }
            if (!File.Exists(accountsTxtLocation))
            {
                var newfile = File.CreateText(accountsTxtLocation);
                newfile.Close();
                string[] content = { "account123:secretpassword" };
                File.WriteAllLines(accountsTxtLocation, content);
            }
        }
   }
}
