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

        private static string[] early = new string[19] { "laggg", "i hav tons of lag ", "lol", "/all do u have lag?", "omg, dis ping", "is my bro account", "follow u to the end", "/all i have best net congo", "wtff", "sureee", "im so baddd", "/all sure", "/all okk", "/all lov ya","pff",":O","gsdasd","op","free win incoming" };
       private static string[] mid = new string[19] { "lol", "/all wp", "ok, report me", "are u serious?", "sure sureee", "ok", "yeeesss", "just play", "easy win", "/all surrender", "i suck, thats all","uffff","/all wtf","go drake","/all road to diamd","road to challenger",":D","lets do this","i cant be dat bad" };
       private static string[] late = new string[19] {"strange game or im mad", "/all end dis plz", "/all not real", "idc", "nashor", "ok", "yaaa", "dafuq", "/all i have a good team", "/all this game is not real, is part of a nightmare or something", "this is not reaalll", "uffff", "wtf", "/all prepare for my penta bro. free win","pamplinas","this is ggwp or what","fuck this","chill","i like pizza" };
      
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
           
            Game.PrintChat("KOPT RAMDON CHAT");


          


            for (int i = 0; i < 6; i++)
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

                if (Segundos==r_mid[i])
                {
                    
                    Game.Say(mid[rnd.Next(0, mid.Count())].ToString());
                }

                if (Segundos==r_late[i])
                {
                    
                    Game.Say(late[rnd.Next(0, late.Count())].ToString());
                }
            }
            
          
            Segundos += 1;
        }
      
    }
}
