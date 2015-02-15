using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Message;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;
using LoLLauncher.RiotObjects;

using LoLLauncher.RiotObjects.Platform.Game.Practice;
using LoLLauncher.RiotObjects.Platform.Harassment;
using LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto;
using LoLLauncher.RiotObjects.Platform.Login;
using LoLLauncher.RiotObjects.Platform.Reroll.Pojo;
using LoLLauncher.RiotObjects.Platform.Statistics.Team;
using LoLLauncher.RiotObjects.Platform.Summoner;
using LoLLauncher.RiotObjects.Platform.Summoner.Boost;
using LoLLauncher.RiotObjects.Platform.Summoner.Masterybook;
using LoLLauncher.RiotObjects.Platform.Summoner.Runes;
using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;

using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using LoLLauncher.RiotObjects.Platform.Game.Map;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using LoLLauncher.RiotObjects.Platform.Summoner.Icon;
using LoLLauncher.RiotObjects.Platform.Catalog.Icon;
using System.Timers;

namespace RitoBot
{
    internal class RiotBot
    {
        public LoginDataPacket loginPacket = new LoginDataPacket();
        public GameDTO currentGame = new GameDTO();
        public LoLConnection connection = new LoLConnection();
        public List<ChampionDTO> availableChamps = new List<ChampionDTO>();
        public LoLLauncher.RiotObjects.Platform.Catalog.Champion.ChampionDTO[] availableChampsArray;
        public string Accountname;
        public string Password;
        public int threadID;
        public double sumLevel { get; set; }
        public double archiveSumLevel { get; set; }
        public double rpBalance { get; set; }
   

        public string region { get; set; }
        public string regionURL;
        public bool QueueFlag;
        public int LastAntiBusterAttempt = 0;
        private MatchMakerParams mMParams;

        public RiotBot(string username, string password, string reg)
        {

           
            Accountname = username;
            Password = password;
           
           
            region = reg;
            connection.OnConnect += new LoLConnection.OnConnectHandler(this.connection_OnConnect);
            connection.OnDisconnect += new LoLConnection.OnDisconnectHandler(this.connection_OnDisconnect);
            connection.OnError += new LoLConnection.OnErrorHandler(this.connection_OnError);
            connection.OnLogin += new LoLConnection.OnLoginHandler(this.connection_OnLogin);
            connection.OnLoginQueueUpdate += new LoLConnection.OnLoginQueueUpdateHandler(this.connection_OnLoginQueueUpdate);
           
            switch (region)
            {
                case "EUW":
                    connection.Connect(username, password, Region.EUW, Program.cversion);
                    break;
                case "EUNE":
                    connection.Connect(username, password, Region.EUN, Program.cversion);
                    break;
                case "NA":
                    connection.Connect(username, password, Region.NA, Program.cversion);
                    regionURL = "NA1";
                    break;
                case "KR":
                    connection.Connect(username, password, Region.KR, Program.cversion);
                    break;
                case "BR":
                    connection.Connect(username, password, Region.BR, Program.cversion);
                    break;
                case "OCE":
                    connection.Connect(username, password, Region.OCE, Program.cversion);
                    break;
                case "RU":
                    connection.Connect(username, password, Region.RU, Program.cversion);
                    break;
                case "TR":
                    connection.Connect(username, password, Region.TR, Program.cversion);
                    break;
                case "LAS":
                    connection.Connect(username, password, Region.LAS, Program.cversion);
                    break;
                case "LAN":
                    connection.Connect(username, password, Region.LAN, Program.cversion);
                    break;
            }
        }

      private void updateStatus(string status, string accname)
        {
           
            Console.WriteLine(string.Concat(new object[7]
              {
                (object) "[",
                (object) DateTime.Now,
                (object) "] ",        
                (object) "[",
                (object) accname,
                (object) "]: ",
                (object) status
              }));
        }        
        
        private async void RegisterNotifications()
        {
            object obj1 = await this.connection.Subscribe("bc", this.connection.AccountID());
            object obj2 = await this.connection.Subscribe("cn", this.connection.AccountID());
            object obj3 = await this.connection.Subscribe("gn", this.connection.AccountID());
        }
        
        private void connection_OnLoginQueueUpdate(object sender, int positionInLine)
        {
            if (positionInLine <= 0)
                return;
            this.updateStatus("Position to login: " + (object)positionInLine, Accountname);
        }

        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
           
              
                this.RegisterNotifications();
                this.loginPacket =  this.connection.GetLoginDataPacketForUser();

                sumLevel = loginPacket.AllSummonerData.SummonerLevel.Level;
                updateStatus("LEVEL " + sumLevel, Accountname);
            var ipoint = loginPacket.IpBalance;
            var rp = loginPacket.RpBalance;
        
                if (sumLevel >= int.Parse(Program.cutlevel))
                {

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"config\\VALID-ACCOUNTS.txt", true))
                    {
                        file.WriteLine(Accountname.ToString() + "-->" + Password.ToString() + "-->  LEVEL: " + sumLevel.ToString() + "-->" + ipoint + " Ipoints" + "-->" + rp + " RP");
                        Program.Validas += 1;

                    }
                }

               
               connection.Disconnect();
            
       
           
          
        }
        
        private void connection_OnError(object sender, LoLLauncher.Error error)
        {
            if (error.Message.Contains("is not owned by summoner"))
            {
                return;
            }
            else if (error.Message.Contains("Your summoner level is too low to select the spell"))
            {
                var random = new Random();
                var spellList = new List<int> { 13, 6, 7, 10, 1, 11, 21, 12, 3, 14, 2, 4 };

                int index = random.Next(spellList.Count);
                int index2 = random.Next(spellList.Count);

                int randomSpell1 = spellList[index];
                int randomSpell2 = spellList[index2];

                if (randomSpell1 == randomSpell2)
                {
                    int index3 = random.Next(spellList.Count);
                    randomSpell2 = spellList[index3];
                }

                int Spell1 = Convert.ToInt32(randomSpell1);
                int Spell2 = Convert.ToInt32(randomSpell2);
                return;
            }
            this.updateStatus( error.Message, Accountname);
        }
        
        private void connection_OnDisconnect(object sender, EventArgs e)
        {
            Program.total += 1;
            if (Program.total == Program.totalacc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(Program.totalacc +" ACOUNTS CHECKED. " + Program.Validas +  " ACCOUNTS > LEVEL  " + Program.cutlevel +" SAVED IN /CONFIG/VALID-ACCOUNTS.txt");
                Console.ReadKey();
             
            }
        }
       
        private void connection_OnConnect(object sender, EventArgs e)
        {
            
            
        }
 
       
       

   
}
}