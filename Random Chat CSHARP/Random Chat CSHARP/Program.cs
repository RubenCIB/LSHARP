using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using System.Threading;
using System.Timers;

namespace Random_Chat_CSHARP
{
    class Program
    {
/* MESSAGES: I did not write this. I told a friend who wrote what he wanted. WRITE WHAT YOU WANT */
        private static string[] early = new string[] { "laggg", "lol", "/all do u have lag?", "omg, dis ping", "is my bro account", "follow u to the end", "/all i have best net congo", "wtff", "sureee", "im so baddd", "/all sure", "/all okk", "/all lov ya","pff",":O","gsdasd","op","free win incoming","/all me corro en tu zepa","/all si te cojo te hago un manojo soplaguindas","pamplinas siempre pamplinas","/all me cago en sanpicopaco","hijo d una mosca","saltimbanki puta pepa me corro en tu oido","give me money for pizza","hijo d un sapo","me corro en tu pantorilla hijo d un duende","te follo el higadillo" };
       private static string[] mid = new string[] { "lol", "/all wp", "ok, report me", "are u serious?", "sure sureee", "ok", "yeeesss", "just play", "easy win", "/all surrender", "i suck, thats all","uffff","/all wtf","go drake","/all road to diamd","road to challenger",":D","lets do this","i cant be dat bad","/all tragador de semen kongoniense","te follo el tuetano","me cago en mi sangre","esto es una gran pamplina","esto es una pantomima","me cago en zeus","/all eres un saltamontes", };
       private static string[] late = new string[] {"strange game or im mad", "/all end dis plz", "/all not real", "idc", "nashor", "ok", "yaaa", "dafuq", "/all i have a good team", "/all this game is not real, is part of a nightmare or something", "this is not reaalll", "uffff", "wtf", "/all prepare for my penta bro. free win","pamplinas","this is ggwp or what","fuck this","chill","i like pizza","/all esto es una pmaplina", "/all me cago en tu zepa", "me tiro a tu perro","/all comprame una nave" };
      
        private static List<int> r_early = new List<int>();
        private static List<int> r_mid = new List<int>();
        private static List<int> r_late = new List<int>();

        private static int Segundos = 0;
       private static Random rnd = new Random();
        static System.Timers.Timer timer = new System.Timers.Timer(1000);


        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            
          
        }


        public static void Game_OnGameLoad(EventArgs args)
        {
           
            Game.PrintChat("KOPTS RANDOM CHAT ");


          


            for (int i = 0; i < 10; i++)   /* 10 means the number messages that will write in each phase of the game. Modify at your own. In this case,33.*/
            {
               
               
                r_early.Add(rnd.Next(100, 600));
                r_mid.Add(rnd.Next(620, 1140));
                r_late.Add(rnd.Next(1200, 2100));
               
            }

           

            timer.Elapsed += Tick;
            timer.Start();
        }

         private static void Tick(object sender, ElapsedEventArgs e)
        {
            

            if (Segundos == 1263 )
            {
                Game.Say("/ff");
            }
                                        /*Will try to surrender */
            if (Segundos == 1800)
            {
                Game.Say("/ff");
            }

            if (Segundos == 2220)
            {
                Game.Say("/ff");
            }

            for (int i = 0; i < r_early.Count(); i++)
            {
                if (Segundos==r_early[i])
                {
                   

                    Game.Say(early[rnd.Next(0, early.Count())].ToString());
                }

            
            }



            for (int i = 0; i < r_mid.Count(); i++)
            {
             

                if (Segundos == r_mid[i])
                {

                    Game.Say(mid[rnd.Next(0, mid.Count())].ToString());
                }

              
            }


            for (int i = 0; i < r_late.Count(); i++)
            {
             

                if (Segundos == r_late[i])
                {

                    Game.Say(late[rnd.Next(0, late.Count())].ToString());
                }
            }
            
          
            Segundos += 1;
        }
      
    }
}
