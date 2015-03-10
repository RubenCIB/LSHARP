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
        private static int Segundos = 0;
        static System.Timers.Timer timer = new System.Timers.Timer(1000);
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            
          
        }


        public static void Game_OnGameLoad(EventArgs args)
        {
           
            Game.PrintChat("KOPT RAMDON CHAT");
            timer.Elapsed += Tick;
            timer.Start();
        }

         private static void Tick(object sender, ElapsedEventArgs e)
        {
            switch (Segundos)
            {
                case 125:
                    Game.Say("lagg");
                        break;
                        case 225:
                    Game.Say("my net is best net kongo");
                        break;
                         case 300:
                    Game.Say("lol");
                        break;
                         case 400:
                    Game.Say("omnggg");
                        break;
                        case 500:
                    Game.Say("sure");
                        break;
                           case 850:
                    Game.Say("are u serious?");
                        break;
                         case 930:
                    Game.Say("sure333");
                        break;
                          case 950:
                    Game.Say("idc about you and dis team");
                        break;
                        case 1000:
                    Game.Say("Dios que malo eres");
                        break;
                        case 1140:
                        Game.Say("no,lol");
                        break;
                        case 1200:
                    Game.Say("/all gggggggggg");
                        break;
                            case 1230:
                    Game.Say("/all time to sleeeep");
                        break;
                         case 1530:
                    Game.Say("/all thi9s is not real, is part of a nightmare");
                        break;
                         case 1800:
                    Game.Say("strange game or im mad");
                        break;
                          case 2300:
                        Game.Say("/all prepare for my penta bro. free win");
                        break;
            }
            Segundos += 1;
        }
      
    }
}
