using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using LeagueSharp.Common;
using LeagueSharp;
using SharpDX;
using Color = System.Drawing.Color;

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
        private static string huevo = "<font color = \"#fbe600\">";
        private static List<string> liga = new List<string>();
        private static List<string> puntos = new List<string>();
        private static List<string> ataque = new List<string>();
        private static List<string> defensa = new List<string>();
        private static List<string> utilidad = new List<string>();
        private static List<System.Drawing.Color> colores = new List<Color>();
        private static List<string> runas = new List<string>();
        private static LeagueSharp.Common.Menu gui;
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private static int cantidad = 0;
        static void Main(string[] args)
        {

            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            colores.Add(System.Drawing.Color.White);
            colores.Add(System.Drawing.Color.Red);
            colores.Add(System.Drawing.Color.Yellow);
            colores.Add(System.Drawing.Color.SandyBrown);
            colores.Add(System.Drawing.Color.DodgerBlue);
        }


        private static void Game_OnGameLoad(EventArgs args)
        {
            Game.PrintChat("<font color = \"#ff052b\">KOPT Lol Nexus</font>  <font color = \"#00FFFF\">Loaded  </font> ");

            if (GetRegion() == "fail")
            {
                Game.PrintChat("KOREA IS NOT SUPPORTED BY LOLNEXUS");
                return;
            }
            gui = new LeagueSharp.Common.Menu("KOPT Lol Nexus V2", "", true);
            if (Navegar() == false)
            {
                return;
            };
            
            
            gui.AddItem(new MenuItem("dibujar", "Draw INFO UNDER you").SetValue(new KeyBind(75, KeyBindType.Press)));
            var press = gui.AddItem(new MenuItem("show", "Print Chat Info (NO RUNES)").SetValue(new KeyBind(73, KeyBindType.Press)));
            gui.AddToMainMenu();
            
            Drawing.OnDraw += Drawing_OnDraw;

            press.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {
                

                

                if (gui.Item("show").GetValue<KeyBind>().Active)
                {
                   
                        

                        imprimir_todo();
                       
                    
                }
            };

        }


        static void Drawing_OnDraw(EventArgs args)
        {

            if ((gui.Item("dibujar").GetValue<KeyBind>().Active))
            {
                Obj_AI_Hero Player = ObjectManager.Player;
                Vector2 worldtoscr = Drawing.WorldToScreen(Player.Position);
                int a = -20;
                int c = 0;
                for (int i = 0; i < camp.Count; i++)
                {


                    Drawing.DrawText(worldtoscr[0] - 500, worldtoscr[1] - a, colores[c], camp[i].ToUpper() + "-->" + liga[i].ToUpper() + " (" + puntos[i] + ")" + "--->" + ataque[i] + defensa[i] + utilidad[i] +"--->"+ runas[i]);
                    a = a - 20;

                    if (camp.Count == 10 && i == 4)
                    {
                        Drawing.DrawText(worldtoscr[0] - 500, worldtoscr[1] - a, System.Drawing.Color.SeaGreen, "<--------------------------------------------------------------------------------------------------------->");
                        a = a - 20;
                        Drawing.DrawText(worldtoscr[0] - 500, worldtoscr[1] - a, System.Drawing.Color.SeaGreen, "<--------------------------------------------------------------------------------------------------------->");
                        a = a - 20;
                    }

                    if (camp.Count == 6 && i == 2)
                    {
                        Drawing.DrawText(worldtoscr[0] - 500, worldtoscr[1] - a, System.Drawing.Color.SeaGreen, "<--------------------------------------------------------------------------------------------------------->");
                        a = a - 20;
                        Drawing.DrawText(worldtoscr[0] - 500, worldtoscr[1] - a, System.Drawing.Color.SeaGreen, "<--------------------------------------------------------------------------------------------------------->");
                        a = a - 20;
                    }

                    c = c+1;
                    if (c >= 5)
                    {
                        c = 0;
                    }

                }





            }

        }

        static bool Navegar()
        {
            string url = "http://www.lolnexus.com/ajax/get-game-info/" + GetRegion() + ".json?name=" + ObjectManager.Player.Name.Replace(" ", "++");

            wb = new WebClient().DownloadString(url);

            if (wb.Contains("player-name"))
            {

                Obtener_datos();
                return true;
            }
            else
            {

                Intentos += 1;
                if (Intentos == 5)
                {
                    Game.PrintChat("LOL NEXUS <font color = \"#ff052b\">IS NOT WORKING.</font> TRY RELOADING (F5) IN A FEW MINUTES");
                    return false;
                }
                Navegar();
                return true;
            }
        }


        static void Obtener_datos()
        {

            
          


            while (wb.Contains("<td class=\\\"champion\\\">"))
            {
                string champ = (GetBetween(wb, "<td class=\\\"champion\\\">\\r\\n        <i class=\\\"icon champions-lol-28", "\\\">"));
                wb = wb.Replace("<td class=\\\"champion\\\">\\r\\n        <i class=\\\"icon champions-lol-28" + champ, " ");
                champ = champ.Replace("\\", "");
                champ = champ.Trim();

                camp.Add(champ);

                string rank = (GetBetween(wb, "class=\\\"champion-ranks", "\\\">"));
                wb = ReplaceFirst(wb, "class=\\\"champion-ranks", "");
                rank = rank.Trim();
                liga.Add(rank);

                string points = (GetBetween(wb, "(<b>", "</b>)"));
                wb = ReplaceFirst(wb, "(<b>", "");
                points = points.Trim();
                points = points.Replace(" ", "");
                puntos.Add(points);

                string offense = (GetBetween(wb, "<span class=\\\"offense\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"offense\\\">", "");
                offense = offense.Trim();
                ataque.Add(offense + "/");

                string defense = (GetBetween(wb, "<span class=\\\"defense\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"defense\\\">", "");
                defense = defense.Trim();

                defensa.Add(defense + "/");

                string utility = (GetBetween(wb, "<span class=\\\"utility\\\">", "</span>"));
                wb = ReplaceFirst(wb, "<span class=\\\"utility\\\">", "");
                utility = utility.Trim();
                utilidad.Add(utility);


                string run = (GetBetween(wb, "<div><h2>", "</span>\\r\\n"));
                wb = ReplaceFirst(wb, "<div><h2>", " ");
                run = (GetBetween(run, "h2>", "</div>"));
                run = format_runes(run);
               runas.Add(run);
               
                
             
            }


            Game.PrintChat("<font color = \"#ff052b\">DATA READY!!.</font>"); 
               Game.PrintChat(rojo +"-->Print info in CHAT" + close + huevo+" (I) BY DEFAULT"+ close);
               Game.PrintChat(rojo + "-->Draw runes UNDER your player" + close + huevo + " (K) BY DEFAULT" + close);
               

           
            cantidad = camp.Count;




        }

        static string format_runes(string runas)
        {

            runas = runas.Replace("magic penetration", "MAGIC.PEN");
            runas = runas.Replace("ability power", "AP");
            runas = runas.Replace("attack damage", "AD");
            runas = runas.Replace("magic resist per level", "MAGIC.RES LVL");
            runas = runas.Replace("at level 18", "AT LVL 18");
            runas = runas.Replace("attack speed", "ATCK SPEED");
            runas = runas.Replace("life steal.", "LIFE STEAL");
            runas = runas.Replace("critical damage", "CRIT DMG");
            runas = runas.Replace("magic resist", "MR");
            runas = runas.Replace("armor", "ARMOR");
            runas = runas.Replace("cooldowns", "CDR");
            runas = runas.Replace("armor penetration", "ARMOR PEN");
            runas = runas.Replace("magic penetration", "MAGIC PEN");
            runas = runas.Replace("mana regen / 5 sec. per level.", "MANA.REG.LVL");
            runas = runas.Replace("mana regen / 5 sec.", "MANA.REG");
            runas = runas.Replace("health regen / 5 sec.", "HEALTH REG/5");
            runas = runas.Replace("movement speed", "MOV.SPEED");
            
            runas = runas.Replace(Environment.NewLine, "");
            runas= runas.Replace("<br />", "");
            runas = runas.Replace("+", "  +");
            runas = runas.Replace("-", "  -");
            runas = runas.ToUpper();
            return runas;

        }

        static void imprimir_todo()
        {



            for (int i = 0; i < camp.Count; i++)
            {
                if (Player.ChampionName.ToUpper() == camp[i].ToUpper())
                {

                       Game.PrintChat( huevo+"--ME--" +close +"--->"+close + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);
               continue;
                }
                if (cantidad == 10)
                {
                    
                 
                    if (i >= 5)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper() + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close );

                        continue;
                    }
                }


                if (cantidad == 6)
                {
                    if (i >= 3)
                    {
                        Game.PrintChat(azul + camp[i].ToUpper() + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close);
                        continue;
                    }
                }
                Game.PrintChat(rojo + camp[i].ToUpper() + close + "--->" + lila + liga[i].ToUpper() + " (" + puntos[i] + ")" + close + "-->" + amarillo + ataque[i] + defensa[i] + utilidad[i] + close );

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

                return "fail";
            }
            if (Game.Region.ToLower().Contains("oc1"))
            {
                return "OCE";
            }

            return "";

        }
    }


}
