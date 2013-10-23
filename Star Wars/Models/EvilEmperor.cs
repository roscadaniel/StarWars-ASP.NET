using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using StarWars.Helpers;

namespace StarWars.Models
{
    // Star Warrior base class - the Evil Emperors
    public class EvilEmperor : JediKnight
    {
        public string Encouragement = "Evil Emperor encourages someone";
        public string Chew = "Evil Emperor chews someone";
        public JediKnight lastPickedJedi = null;

        public EvilEmperor()
        {
            this.Name = "Palpatine";
            this.Email = "palpatine@tattooine.com";
            this.LightSaberColor = "Pink";
            this.currentDamageLevel = JediKnight.DamageLevel.Healthy;
            this.DarkSide = true;
            this.Deceased = false;
            this.fightLog = new AttackRecorder();
            //this.fightLog.FightEvents = new List<string>()
            //{
            //    Encouragement,
            //    Chew
            //};

            this.LastWords = "Aaaaargh, I never thought to take out life insurance! How pathetic of me in this cruel, cruel world.";
        }

        public void actionDarthVader(JediKnight inst, string action = "")
        {
            switch (action)
            {
                case "encouragement":
                    this.fightLog.FightEvents = new List<string>()
                    {
                        Encouragement
                    };
                    break;
                case "chew":
                    this.fightLog.FightEvents = new List<string>()
                    {
                        Chew
                    };
                    break;
                default:
                    break;
            }
        }

        public void DarkSideBoost(List<JediKnight> JediKnights)
        {
            //Log actions
            Logging logTxt = new Helpers.Logging();

            foreach (JediKnight dude in JediKnights) 
            {
                if (dude.DarkSide == false)
                {
                    //good guy :((
                    switch (dude.currentDamageLevel)
                    {
                        case JediKnight.DamageLevel.Healthy:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Challenged;
                            logTxt.Main(dude.Name + " was taken a peg down from Healthy to Challenged by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Challenged:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Hurting;
                            logTxt.Main(dude.Name + " was taken a peg down from Challenged to Hurting by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Hurting:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Critical;
                            logTxt.Main(dude.Name + " was taken a peg down from Hurting to Critical by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Critical:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Wasted;
                            logTxt.Main(dude.Name + " was taken a peg down from Critical to Wasted by EvilEmperor");
                            break;
                    }
                }
                else
                { 
                    //bad guy :D
                    switch (dude.currentDamageLevel)
                    {
                        case JediKnight.DamageLevel.Challenged:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Healthy;
                            logTxt.Main(dude.Name + " got lifted a notch up from Challenged to Healthy by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Hurting:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Challenged;
                            logTxt.Main(dude.Name + " got lifted a notch up from Hurting to Challenged by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Critical:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Hurting;
                            logTxt.Main(dude.Name + " got lifted a notch up from Critical to Hurting by EvilEmperor");
                            break;
                        case JediKnight.DamageLevel.Wasted:
                            dude.currentDamageLevel = JediKnight.DamageLevel.Critical;
                            logTxt.Main(dude.Name + " got lifted a notch up from Wasted to Critical by EvilEmperor");
                            break;
                    }
                }
            }
        }

        public JediKnight PickJediKnight(List<JediKnight> JediKnights, int randomInt)
        {
            //Log actions
            Logging logTxt = new Helpers.Logging();

            if (this.lastPickedJedi != JediKnights[randomInt])
            {
                this.lastPickedJedi = JediKnights[randomInt];
                logTxt.Main("There's a new Jedi Knight for you");
                return this.lastPickedJedi;
            }
            else
            {
               // logTxt.Main("So sad, random number generator is not working properly");
                return null;
            }
        }
    }
}