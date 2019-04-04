﻿/*
 *    Author Trevor Garrity
 *    September 2018
 *    Created in Jetbrains Rider IDE
 *    Version 1.0
 *    About: A savage new dystopian TV show that pitches celebrities against each other in brutal knife combat led us to
 *    create an application that can predict the outcome of the battles.
 *    Celebrities are given random weapons and are made to fight in random arenas.
 *    Winners are selected upon initial strengths and weaknesses with variables from random weapons and arenas taken into account
 *    There are also professional fighters added to the mix who contain their own properties such as the ability to reduce their opponents
 *    eye of the tiger, often through a pre-match cold hard stare.
 *
 * 
 * Notes: Please be aware that I know the program is incomplete.
 * Things could be better written or broken into smaller methods.
 * Next: Validation of input, break up to smaller methods, create method for user built Celebrities.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace celebrityKnifefight

{
    internal class Program

    {
 // Selects and returns 2 Celebrity class instances

        private static (Celebrity, Celebrity) ShowAll()
        {
            // These variables are to keep track of the user input state
            var count1 = 0;
            var count2 = 0;
            var showRecords = "";
            // make an array of instances
            var myCelebs = new Celebrity[4];
            myCelebs[0] = new Celebrity(0, "Britney Spears", 1.63, 57, 45, 25, false);

            myCelebs[1] = new ProFighter(1, "Chuck Norris", 1.83, 77.1, 70, 60, true);

            myCelebs[2] = new Celebrity(2, "Barak Obama", 1.85, 81.6, 56, 35, false);

            myCelebs[3] = new Celebrity(3, "Donald Trump", 1.90, 107, 63, 12, false);

            // User input
            // Missing validation methods
            while (count1 == 0 && count2 == 0 && showRecords == "")
            {
                Console.WriteLine("Select your first fighter by number or enter 'r' for records of previous fights.");
                // Display fighters
                foreach (var celeb in myCelebs) Console.WriteLine(celeb.Name + " with a " + celeb.BladeName + " Press: " + celeb.Id );
                
               
                var celebString = Console.ReadLine();
                // If the user does not want to view previous fight records
                if (celebString != "r" && count1 == 0 && count2 == 0)
                {
                    var celebNum = Convert.ToInt32(celebString);
                    // Assign first choice 
                    var c1 = myCelebs[celebNum];
                    // change state for the next choice

                    count1 = 1;
                    c1.ShowStats();
                   
                    if (count1 == 1 && count2 == 0)
                    {
                        
                        Console.WriteLine("Select your second fighter!");
                        celebString = Console.ReadLine();
                        var celebNum1 = Convert.ToInt32(celebString);
                        var c2 = myCelebs[celebNum1];
                        c2.ShowStats();
                        showRecords = "";
                        count2 = 1;
                        return (c1, c2);

                    }
                }
                var myReader = new StreamReader("fightrecords.txt");
                // Read from file 
                if (celebString == "r")
                {
                    
                    var consoleLine = " ";
                    while (consoleLine != null)
                    {
                        consoleLine = myReader.ReadLine();
                        if (consoleLine != null)
                            Console.WriteLine(consoleLine);
                        if (consoleLine == null)
                        {
                            Console.WriteLine("Press enter to return to selection!");

                            showRecords = "";
                        }

                    }
                    
                    Console.ReadLine(); 
                }
                myReader.Close();

            }
            
            return (null, null);
        }

        

        // Select a random Arena. Note: the ID matches it to a celebrity as a home ground
        public static Arena SelectArena()
        {
            var arenaChoice = new Random();

            var arenaNum = arenaChoice.Next(0, 3);

            if (arenaNum == 0)
            {
                var a1 = new Arena(0, "Mean Streets", 20);
                return a1;
            }

            if (arenaNum == 1)
            {
                var a1 = new Arena(1, "Hot Jungle", 36);
                return a1;
            }

            if (arenaNum == 2)
            {
                var a1 = new Arena(2, "Siberian Steppe", -35);
                return a1;
            }

            return null;
        }

        // Celebrities have a victory action selected at random
        private static string MakeVictory(Celebrity c1)
        {
            string[,] allVictoryStrings =
            {
                {
                    "having a breakdown and shaving her head.",
                    "polluting radio station airwaves with garbage music.",
                    "creating soft pornography as a music video clip.",
                    "joining ISIS as a Jihadi bride.",
                    "hunting down Christina Agullera with her blade."
                },
                {
                    "disappointing fans by being a scared weird evangelical nutter.",
                    "shaving off his beard and joining a satanic cult.",
                    "licking the American flag.",
                    "finally coming out of the closet and freeing the little boys held captive in his basement.",
                    "lynching some African Americans."
                },
                {
                    "drone striking innocent civilians, and receiving a nobel peace prize for the effort. ",
                    "giving Michelle a terrorist fist bump.",
                    "paying Billions to bankers for destroying the economy.",
                    "sending conservatives into hysteria for being black.",
                    "shooting some hoops with his opponents severed head."
                },
                {
                    "grabbing the nearest Pu&$y.",
                    "picking up the phone and colluding with Vladimir Putin.",
                    "tweeting some unhinged crap about Hillary Clinton",
                    "by just being a fat orange bastard.",
                    "heading off for his spray tan."
                }
            };

            var stringChoice = new Random();

            var stringNum = stringChoice.Next(0, 5);
            // Iterate through the arrays, match the user, select a random index as a victory message

            foreach (var i in allVictoryStrings)
            {
                var victoryAction = allVictoryStrings[c1.Id, stringNum];

                return victoryAction;
            }

            return null;
        }


        // Most of the program logic. Calls various methods modifies parameters and calculates winner 

        private static void SetUp()
        {
            var (c1, c2) = ShowAll();

            var a1 = SelectArena();
            PreFightDetails(c1, c2, a1);
            DoBattle(c1, c2, a1);
            
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();

            // Show basic pre fight details ask user to confirm
            // Needs input validation methods
        }

        private static void PreFightDetails(Celebrity c1, Celebrity c2, Arena a1)
        {
            var battleDetailsC1 = c1.Name + " with the " + c1.BladeName;
            Console.WriteLine("In the " + a1.ArenaName);
            Console.WriteLine(battleDetailsC1);
            Console.WriteLine("VS");
            var battleDetailsC2 = c2.Name + " with the " + c2.BladeName;
            Console.WriteLine(battleDetailsC2);
            Console.WriteLine("-----");
            var readyToRumble = "";
            // Needs input validation
            if (readyToRumble == "")
            {
                Console.WriteLine("Are you ready to Rumble? Enter y or n");
                readyToRumble = Console.ReadLine();
            }

            if (readyToRumble == "n") Console.WriteLine("Too bad runty wimp, the fight goes ahead anyway");
        }

        // method could be broken down further
        // read comments inside
        private static void DoBattle(Celebrity c1, Celebrity c2, Arena a1)
        {
            // Check if the arena matches the celebrity, (by ID's) calculate a home advantage, added to EyeOfTheTiger, gives a little story to the battle
            if (a1.ArenaId == c1.Id)
            {
                c1.EyeOfTheTiger += 25;
                c1.FightPoints += 25;
                Console.WriteLine(c1.Name + " Has the home advantage so their eye of the tiger increases to : " +
                                  c1.EyeOfTheTiger + "/100");
            }

            if (a1.ArenaId == c2.Id)
            {
                c2.EyeOfTheTiger += 25;
                c2.FightPoints += 25;
                Console.WriteLine(c2.Name + " Has the home advantage so their eye of the tiger increases to : " +
                                  c2.EyeOfTheTiger + "/100");
            }


            //Logic that reduces EyeOfTheTiger of opponent if one fighter isPro
            if (c1.IsPro && !c2.IsPro)
            {
                Console.WriteLine(c2.Name + "'s eye of the tiger before the fight: " + c2.EyeOfTheTiger);
                c2.FightPoints = c2.FightPoints - c2.EyeOfTheTiger;

                c2.EyeOfTheTiger = 0;
                Console.WriteLine(c1.Name + " gives " + c2.Name + " a cold hard stare reducing " + c2.Name +
                                  "'s eye of the tiger to " + c2.EyeOfTheTiger + "/100");
            }

            if (c2.IsPro && !c1.IsPro)
            {
                Console.WriteLine(c1.Name + "'s eye of the tiger before the fight: " + c1.EyeOfTheTiger);

                c1.FightPoints = c1.FightPoints - c1.EyeOfTheTiger;

                c1.EyeOfTheTiger = 0;
                Console.WriteLine(c2.Name + " gives " + c1.Name + " a cold hard stare reducing " + c1.Name +
                                  "'s eye of the tiger to " + c1.EyeOfTheTiger + "/100");
            }

            // Prepare to write to file
            var toFile = new StreamWriter("fightrecords.txt", true);
            // Calculate winner, write the results to console and to file
            if (c1.fightPoints > c2.fightPoints)
            {
                // Example of where a new method could be inserted
                MakeVictory(c1);
                var vicMsg = MakeVictory(c1);
                var margin = c1.fightPoints - c2.fightPoints;
                Console.WriteLine(c1.Name + " Fight points: " + c1.fightPoints);
                Console.WriteLine(c2.Name + " Fight points: " + c2.fightPoints);
                Console.WriteLine(c1.Name + " Wins by : " + margin + " fight points.");
                Console.WriteLine(c1.Name + " celebrates the victory by " + vicMsg);

                DateTime now = DateTime.Now;
                string time = now.ToString();
                Console.WriteLine(time);
                // New method
                string[] record =
                {
                   " ",  c1.Name + " with a " + c1.BladeName, "VS", c2.Name + " with a " + c2.BladeName,
                    "In " + a1.ArenaName, time,
                    c1.Name + " won by " + margin + " fight points.", c1.Name + " celebrated the victory by " + vicMsg,
                    " ",
                    "<----------*--*--*---------->"
                };

                foreach (var line in record) toFile.WriteLine(line);
                toFile.Close();
            }
            else
            {
                MakeVictory(c2);
                var vicMsg = MakeVictory(c2);
                var margin = c2.fightPoints - c1.fightPoints;
                Console.WriteLine(c1.Name + " Fight points: " + c1.fightPoints);
                Console.WriteLine(c2.Name + " Fight points: " + c2.fightPoints);
                Console.WriteLine(c2.Name + " Wins by : " + margin + " fight points.");
                Console.WriteLine(c2.Name + " celebrates the victory by " + vicMsg);
                DateTime now = DateTime.Now;
                string time = now.ToString();
                Console.WriteLine(time);
                string[] record =
                {
                    " ",  c2.Name + " with a " + c2.BladeName, "VS", c1.Name + " with a " + c1.BladeName,
                    "In " + a1.ArenaName, time,
                    c2.Name + " won by " + margin + " fight points.", c2.Name + " celebrated the victory by " + vicMsg,
                    " ",
                    "<----------*--*--*---------->"
                };

                foreach (var line in record) toFile.WriteLine(line);
                toFile.Close();
            }
        }


        private static void Main(string[] args)
        {
            //

            SetUp();
        }


        public class Arena
        {
            public Arena(int arenaId, string arenaName, double temperature)
            {
                ArenaId = arenaId;
                ArenaName = arenaName;
                Temperature = temperature;
            }

            public int ArenaId { get; }
            public string ArenaName { get; }
            private double Temperature { get; }
        }
    }


    public class Blade
    {
        public Blade(int id, string name, double length, double speed)
        {
            Id = id;
            Name = name;
            Length = length;
            Speed = speed;
        }


        public int Id { get; }
        public string Name { get; }
        public double Length { get; }
        public double Speed { get; }
    }


    public class Celebrity
    {
        private readonly double WeightAdvantage;
        private double eyeOfTheTiger;


        public double fightPoints;
        private double speed;

        public Celebrity(string name)
        {
            Name = name;
        }

        public Celebrity(int id, string name, double height, double weight, double speed, double eyeOfTheTiger,
            bool isPro)
        {
            Id = id;
            Name = name;
            Height = height;
            Weight = weight;
            this.speed = speed;
            IsPro = isPro;
            EyeOfTheTiger = eyeOfTheTiger;

            var x = SelectBlade();
            BladeName = x.Name; // Why can't I access this from outside or its returned value blade1?
            var bladeLen = x.Length;
            var bladeSpeed = x.Speed;
            WeightAdvantage = BodyMass(height, weight);
            fightPoints = height + weight + eyeOfTheTiger + bladeLen + bladeSpeed / 2 + WeightAdvantage;
            CalcFightPoints(height, weight, eyeOfTheTiger, bladeLen, bladeSpeed, WeightAdvantage);
        }

        public int Id { get; }
        public string Name { get; }
        private double Height { get; }
        private double Weight { get; }
        public virtual bool IsPro { get; }

        public string BladeName { get; }


        public double Speed
        {
            get => speed;
            set
            {
                speed = value;
                if (speed > 100) speed = 100;
            }
        }

        public double EyeOfTheTiger
        {
            get => eyeOfTheTiger;
            set
            {
                eyeOfTheTiger = value;
                if (eyeOfTheTiger > 100) eyeOfTheTiger = 100;
            }
        }

        public double FightPoints
        {
            get => fightPoints;
            set => fightPoints = value;
        }

        // Calculate fight points from initial celebrity and selected blade properties
        private void CalcFightPoints(double height, double weight, double EyeOfTheTiger, double bladeLen,
            double bladeSpeed, double adv)
        {
            fightPoints = height + weight + EyeOfTheTiger + bladeLen + bladeSpeed / 2 + adv;
        }

        // Determine fighters Body Mass Index return advantage or disadvantage
        private double BodyMass(double height, double weight)
        {
            double adv;
            var x = weight / height;
            var y = x / height;

            if (y < 18.5)
                adv = -10;
            else if (y > 18.5 && y <= 29.4)
                adv = +10;
            else if (y > 25.0 && y < 29.9)
                adv = -10;
            else
                adv = -20;

            return adv;
        }


        // Show fighters stats before being modified according to Arena and ProFighter properties.
        public void ShowStats()
        {
            Console.WriteLine("Fighter: " + Name);
            Console.WriteLine("Height : " + Height + "m");
            Console.WriteLine("Weight : " + Weight + "kg");
            Console.WriteLine("Speed : " + speed + "/100");
            Console.WriteLine("Eye of the tiger : " + eyeOfTheTiger + "/100");

            Console.WriteLine("Weapon : " + BladeName);
            // Console.WriteLine("Blade length : " + c1.BladeLen + " cm");
            Console.WriteLine("Weight advantage: " + WeightAdvantage);
            Console.WriteLine("Fight points before battle : " + FightPoints);
            Console.WriteLine("");
        }

        // Random selection of blade, called inside the Celebrity class
        public Blade SelectBlade()
        {
            Thread.Sleep(40);
            var bladeChoice = new Random();
            var bladeNum = bladeChoice.Next(0, 4);
            var myBlades = new Blade[4];
            myBlades[0] = new Blade(0, "Katana", 60, 68);
            
            myBlades[1] = new Blade(1, "Swiss Army knife", 6, 95);
           
            myBlades[2] = new Blade(2, "Kitchen Knife", 15, 72);
            
            myBlades[3] = new Blade(3, "Broad sword", 80, 35);

            var blade1 = myBlades[bladeNum];
          
            return blade1;
        }
    }

    // Inherited class of ProFighter 
    internal class ProFighter : Celebrity
    {
        public ProFighter(int id, string name, double height, double weight, double speed, double eyeOfTheTiger,
            bool isPro) : base(id, name, height, weight, speed, eyeOfTheTiger, isPro)
        {
            Speed = speed;
            EyeOfTheTiger = eyeOfTheTiger;
            IsPro = isPro;
            ValidatePro(speed, eyeOfTheTiger);
        }

        public override bool IsPro { get; }


        // This is for when a user may choose to input their own Celebrities
        // To check that their speed and eye of the tiger are high enough values
        private string ValidatePro(double speed, double eyeOfTheTiger)
        {
            var warning = "";

            if (IsPro && eyeOfTheTiger < 60) warning = "Seriously?... Pro fighters eye of the tiger must be over 60!";
            if (IsPro && speed < 70) warning = "What are you doing? Pro fighters speed must be over 70!";
            if (IsPro && speed < 70 && eyeOfTheTiger < 60)

                warning = "A pro fighter cannot possibly be slower than 70/100 and eye of the tiger must be over 60. ";
            Console.WriteLine(warning);
            return warning;
        }
    }
}