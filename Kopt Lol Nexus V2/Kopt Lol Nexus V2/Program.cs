using System;
using System.Collections.Generic;
using System.Net;
using LeagueSharp.Common;
using LeagueSharp;

namespace Kopt_Lol_Nexus_V2
{
    class Program
    {
        private static string wb;
        private static int Intentos = 0;
        private static List<string> camp = new List<string>();
        private static string rojo = "<font color = \"#ff052b\">";
        private static string close = "</font>";
        private static string lila = "<font color = \"#39f613\">";
        private static string amarillo = "<font color = \"#f6d313\">";
        private static string azul = "<font color = \"#0cf7e4\">";
        
        private static List<string> lvl = new List<string>();
        private static List<string> liga = new List<string>();
        private static List<string> puntos = new List<string>();
        private static List<string> ataque = new List<string>();
        private static List<string> defensa = new List<string>();
        private static List<string> utilidad = new List<string>();
        private static LeagueSharp.Common.Menu gui;
       
        private static int cantidad = 0;
        static void Main(string[] args)
        {
            
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            
            
        }


        private static void Game_OnGameLoad(EventArgs args)
        {
            Game.PrintChat("<font color = \"#ff052b\">KOPT Lol Nexus</font>  <font color = \"#00FFFF\">Loaded  </font> ");
            gui = new LeagueSharp.Common.Menu("KOPT Lol Nexus V2", "", true);
            
            gui.AddItem(new MenuItem("show_level", "Show Players Level").SetValue(true));
            gui.AddItem(new MenuItem("show_masteries", "Show Masteries").SetValue(true));
           var press= gui.AddItem(new MenuItem("show", "Print Info").SetValue(new KeyBind(73, KeyBindType.Press)));
            gui.AddToMainMenu();
            Navegar();
         

            press.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {


              

                if (gui.Item("show").GetValue<KeyBind>().Active)
                {
                    if (gui.Item("show_level").GetValue<bool>() & gui.Item("show_masteries").GetValue<bool>())
                    {


                        imprimir_todo();
                        return;
                    }

                    if (gui.Item("show_level").GetValue<bool>())
                    {


                        imprimir_nivel();
                        return;
                    }
                    if (gui.Item("show_masteries").GetValue<bool>())
                    {


                        imprimir_maestrias();
                        return;
                    }

                    if (!gui.Item("show_masteries").GetValue<bool>() & !gui.Item("show_masteries").GetValue<bool>())
                    {


                        imprimir_solo();
                        return;
                    }
                }
            };

        }

       
        
          
            static void Navegar()
        {
            string url="http://www.lolnexus.com/ajax/get-game-info/"+GetRegion()+".json?name="+ObjectManager.Player.Name.Replace(" ", "++");
           
            wb = new WebClient().DownloadString(url);
            
            if (wb.Contains("player-name"))
            {
                
                Obtener_datos();
            }
            else
            {
               
                Intentos += 1;
                if (Intentos==5)
                {
                    Game.PrintChat("LOL NEXUS <font color = \"#ff052b\">IS NOT WORKING.</font> TRY RELOADING IN A FEW MINUTES");
                    return;
                }
                Navegar();
            }
        }


        static void Obtener_datos()
        {
            
            while (wb.Contains("icon champions-lol-28"))
            {
                string champ=(GetBetween(wb, "icon champions-lol-28", "\">"));
                wb = wb.Replace("icon champions-lol-28" + champ, " ");
                champ= champ.Replace("\\","");
                champ = champ.Trim();
                
                camp.Add(champ);
            

           
                string level = (GetBetween(wb, "<td class=\\\"level\\\">", "</td>"));
                wb = ReplaceFirst(wb, "<td class=\\\"level\\\">","");
               level = level.Trim();
                lvl.Add(level);


                string rank = (GetBetween(wb, "class=\\\"champion-ranks", "\\\">"));
                wb = ReplaceFirst(wb, "class=\\\"champion-ranks", "");
                rank = rank.Trim();
                liga.Add(rank);
            
                string points= (GetBetween(wb, "(<b>", "</b>)"));
                wb = ReplaceFirst(wb, "(<b>", "");
               points = points.Trim();
                points = points.Replace(" ", "");
               puntos.Add(points);

                string offense = (GetBetween(wb, "<span class=\\\"offense\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"offense\\\">", "");
                offense = offense.Trim();
                ataque.Add(offense+"/");

                string defense = (GetBetween(wb, "<span class=\\\"defense\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"defense\\\">", "");
                defense = defense.Trim();
                
               defensa.Add(defense+"/");

                string utility = (GetBetween(wb, "<span class=\\\"utility\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"utility\\\">", "");
                utility = utility.Trim();
                utilidad.Add(utility);

            }


            Game.PrintChat("<font color = \"#ff052b\">DATA READY!!.</font>  <font color = \"#00FFFF\">Press key to print (i by default) </font> ");
            cantidad = camp.Count;




        }

        static void imprimir_todo()
        {

            
            
           for (int i = 0; i<camp.Count ; i++)
            {
               if (cantidad== 10)
                {
                    if (i >= 5) {
                        Game.PrintChat(azul + camp[i].ToUpper() + " (" + lvl[i] + ")" + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")"+ close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);
                
                        continue;
                    }
                }


               if (cantidad == 6)
               {
                   if (i >= 3)
                   {
                       Game.PrintChat(azul + camp[i].ToUpper() + " (" + lvl[i] + ")" + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] +")"+ close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);
                       continue;
                   }
               }
               Game.PrintChat(rojo+camp[i].ToUpper() + " (" + lvl[i] + ")" + close+"--->" +lila +liga[i].ToUpper() + " (" + puntos[i] +")" +close + "-->" + amarillo+ ataque[i] + defensa[i] + utilidad[i]+close);
                
            }
          
        }
        
        static void imprimir_nivel()
        {


           for (int i = 0; i<camp.Count ; i++)
            {
               if (cantidad== 10)
                {
                    if (i >= 5) {
                        Game.PrintChat(azul + camp[i].ToUpper() + " (" + lvl[i] + ")" + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")"+ close);
                
                        continue;
                    }
                }


               if (cantidad == 6)
               {
                   if (i >= 3)
                   {
                       Game.PrintChat(azul + camp[i].ToUpper() + " (" + lvl[i] + ")" + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] +")"+ close);
                       continue;
                   }
               }
               Game.PrintChat(rojo+camp[i].ToUpper() + " (" + lvl[i] + ")" + close+"--->" +lila +liga[i].ToUpper() + " (" + puntos[i] +")" +close);
                
            }

        }

        static void imprimir_maestrias()
        {


            for (int i = 0; i < camp.Count; i++)
            {
                if (cantidad == 10)
                {
                    if (i >= 5)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper()  + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);

                        continue;
                    }
                }


                if (cantidad == 6)
                {
                    if (i >= 3)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper()  + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);
                        continue;
                    }
                }
                Game.PrintChat(rojo + camp[i].ToUpper()  + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);

            }

        }


        static void imprimir_solo()
        {


            for (int i = 0; i < camp.Count; i++)
            {
                if (cantidad == 10)
                {
                    if (i >= 5)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper()  + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")"  + close);

                        continue;
                    }
                }


                if (cantidad == 6)
                {
                    if (i >= 3)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper() + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close);
                        continue;
                    }
                }
                Game.PrintChat(rojo + camp[i].ToUpper() + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close);

            }

        }

       static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
      

         static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
         public static string GetRegion()
         {
             if (Game.Region.ToLower().Contains("na"))
             {
                 return "NA";
             }
             if (Game.Region.ToLower().Contains("euw"))
             {
                 return "EUW";
             }
             if (Game.Region.ToLower().Contains("eun"))
             {
                 return "EUNE";
             }
             if (Game.Region.ToLower().Contains("la1"))
             {
                 return "LAN";
             }
             if (Game.Region.ToLower().Contains("la2"))
             {
                 return "LAS";
             }
             if (Game.Region.ToLower().Contains("tr"))
             {
                 return "TR";
             }
             if (Game.Region.ToLower().Contains("br"))
             {
                 return "BR";
             }
             if (Game.Region.ToLower().Contains("ru"))
             {
                 return "RU";
             }
             if (Game.Region.ToLower().Contains("kr"))
             {
                 Game.PrintChat("KOREA IS NOT SUPPORTED BY LOLNEXUS");
                
             }
             if (Game.Region.ToLower().Contains("oc1"))
             {
                 return "OCE";
             }

             return "";

         }
    }


}
