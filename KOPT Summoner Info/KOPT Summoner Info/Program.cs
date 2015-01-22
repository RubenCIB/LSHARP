using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LeagueSharp;
using LeagueSharp.Common;


namespace KOPT_Summoner_Info
{
    class Program
    {

        public static string url;
        public static HtmlElementCollection data;
        List<string> Campeones = new List<string>();
        List<string> Liga = new List<string>();
        public static int intentos = 0;
        private static LeagueSharp.Common.Menu gui;



        static void Main(string[] args)
        {
            
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        

        private static void Game_OnGameLoad(EventArgs args)
        {
            Program nav = new Program();

            url = "http://www.lolnexus.com/" + GetRegion() + "/search?name=" + (ObjectManager.Player.Name).Replace(" ", "+");
           
            nav.runBrowserThread(url);
            Game.PrintChat("<font color = \"#ff052b\">LOADED:</font>  <font color = \"#00FFFF\">Kopt Summoner Info   </font> ");
            Game.PrintChat("<font color = \"#ff052b\">CONNECTING:</font>  <font color = \"#00FFFF\">Lol Nexus</font>");
            gui = new LeagueSharp.Common.Menu("KOPT Sum. Info", "", true);
            gui.AddToMainMenu();
        
        }
 
        public void runBrowserThread(string url)
        {
            var th = new Thread(() =>
            {
                var br = new WebBrowser();
                br.ScriptErrorsSuppressed = true;
                br.DocumentCompleted += browser_DocumentCompleted;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                
               // System.IO.File.WriteAllText(@"C:\web.html", br.DocumentText);
               
                foreach (HtmlElement element in br.Document.All)
                {
                    if (element.TagName.Equals("TD"))
                    {
                        if(element.InnerHtml.Contains("player-name"))
                        {
                            
                            data = br.Document.All;
                            
                            obtener_datos();
                            return;
                        }
                        
                    }



                }

                

                intentos += 1;
                br.Navigate(url);
                if (intentos== 3)
                {
                    Game.PrintChat("<font color = \"#00FFFF\">Lol Nexus </font> <font color = \"#ff052b\"> is not working.</font>");
                    Game.PrintChat("<font color = \"#00FFFF\">Try <font color = \"#ff052b\">RELOADING(F5)</font> <font color = \"#00FFFF\">in a few minutes.</font>");
                    Application.Exit();
                    
                }
                   
                
                
                

            }
        }

         void obtener_datos()
        {
            
            foreach (HtmlElement element in data)
            {


                if (element.GetAttribute("classname").Equals("champion"))
                {

                    string campeon = element.InnerText.Replace(Environment.NewLine,"");
                    campeon = campeon.Replace("(", "");
                    campeon = campeon.Replace(")", "");
                    campeon = campeon.Replace(" ", "");
                    for (int c = 0; c < 10; c++)
                    {
                       
                        campeon = campeon.Replace(c.ToString(), "");
                    }
                    
                    if (campeon == "Champion"){}else{Campeones.Add(campeon); }

                    
                
                }

                if (element.GetAttribute("classname").Equals("current-season"))
                {

                    string league = element.InnerText.Replace(Environment.NewLine, "");

                    if (element.InnerText == "Current Season") { } else { Liga.Add(league); }
                    


                }


            }


            mostrar_datos();
           
            
        }

         void mostrar_datos()
         {

             if (Campeones.Count() < 1)
             {
                 Game.PrintChat("<font color = \"#ff052b\">LOL NEXUS IS NOT WORKING ATM. TRY RELOADING IN A FEW MINUTES</font>");
                 Application.Exit();
             }


             
             for (int i = 0; i < Campeones.Count; i++)
             {

                 Game.PrintChat("<font color = \"#ff052b\">" + Campeones[i].ToUpper() + "</font><font color = \"#8a4af3\">-------></font>" + Liga[i].ToUpper());
                Application.Exit();

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
                Application.Exit();
            }
            if (Game.Region.ToLower().Contains("oc1"))
            {
                return "OCE";
            }

            return "";

        }




    }



}
