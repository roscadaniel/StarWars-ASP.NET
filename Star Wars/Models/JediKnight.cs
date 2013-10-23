using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using StarWars.Helpers;

namespace StarWars.Models
{
    // Star Warrior base class - the Jedi Knight
    public abstract class JediKnight
    {
        // enum for damage level
        public enum DamageLevel
        {
            Healthy = 95,
            Challenged = 50,
            Hurting = 25,
            Critical = 10,
            Wasted = 0
        }

        // enum for academy approved light saber tricks
        public enum LightSaberAction
        {
            Italian_Kneecap_Twister = 0,
            Tickle_Enemy_Nose = 1,
            Swashbuckle_Slammer = 2,
            Sizzle_Enemy_Boots = 3,
            Cut_Enemy_Hand_Off = 4,
            Cut_Enemy_Leg_Off = 5,
            Wound_Enemy_Privates = 6,
            Scorch_Enemy_Butt = 7,
            Pierce_Enemy_Chest = 8,
            Nick_Enemy_Ear = 9,
            Cut_Hole_In_Floor = 10
        }

        // some jedi knight properties we will want to get and set from the method running the fight      
        public DamageLevel currentDamageLevel { get; set; }
        public LightSaberAction currentLightSaberAction { get; set; }
        public bool Deceased { get; set; }
        public AttackRecorder fightLog { get; set; }
        public string LastWords { get; set; }
        public int numOfDeaths {get; set; }

        

        // info properties we need to set up a new recruit
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your preferred light saber color")]
        public string LightSaberColor { get; set; }

        // using a nullable bool for the DarkSide property allows us to validate a 'no choice'
        [Required(ErrorMessage = "Please specify if you are drawn to the dark side or not!")]
        public bool? DarkSide { get; set; }


        // some default enemy slanders here        
        private string[] enemySlanders = {"Avast, ye nasty lookin' landlubber - come get some!", 
                                          "I'll have you set up real nice real soon with a silly-looking toothless grin.",
                                          "Dude, you fight like a guy who experiments way too much with alcohol-brain osmosis.", 
                                          "Arrrrr! I'll have your arm off before long, too!",
                                          "You'll soon be very sorry that you ever set foot in this town.",
                                          "Dis mean ole worl' gone' have me punched both black and blue, but it also gonna do the same to you - scumbag!",
                                          "I'll kick your butt to Tatooine and back - you miserable meat popsicle pumped up on hydrogen derivates!",
                                          "Hey, mr. two-brain-cells-kissing... watch this!" };

        
        // attack another jedi knight with the help of magic random numbers       
        public void AttackEnemy(JediKnight jediKnight, JediKnight opponent)
        {
            try
            {
                // set up an attack using a standard lightsaber action, registering opponent's resulting damage level, and slandering him                
                if (opponent.currentDamageLevel == DamageLevel.Wasted)
                {   
                    // He's already finito.. see if we can make him join the game again!
                    jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " takes a poke trying to off " + opponent.Name + " who is already at the Rigor Mortis stage. Tsk, tsk.. a bit necro-phixated, are we?");
                    // Rescusiate..
                    jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " gets all squishy-feely at seeing " + opponent.Name + " so horribly pale, and takes him - ok, drags him.. along to the Jawa sandcrawler for recycling.");
                    opponent.currentDamageLevel = DamageLevel.Critical;
                    opponent.Deceased = false;
                    jediKnight.fightLog.FightEvents.Add(opponent.Name + " didn't bring in much of a recycle fee for " + jediKnight.Name + " - but the Jawas were real nice. Our hero was kicked out in the desert again alive, minus only a few insignificant internal body parts and sporting a damage level of: "
                            + opponent.currentDamageLevel);
                }
                else
                {
                    //Get the instance of the log class
                    Logging logTxt = new Helpers.Logging();

                    RandomGenerator ranGen = new Helpers.RandomGenerator();
                    int randomInt = ranGen.RandomInteger(10);
                    // record which lightsaber maneouver was randomly chosen
                    currentLightSaberAction = (LightSaberAction)randomInt;
                    jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " attacks " + opponent.Name
                                             + " by deftly applying the Light Saber Academy approved maneouver "
                                             + currentLightSaberAction);

                    if (currentLightSaberAction == LightSaberAction.Pierce_Enemy_Chest)
                    {
                        jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " scored a hole-in " + opponent.Name
                                             + " by Piercing the Enemy Chest");

                        opponent.currentDamageLevel = DamageLevel.Wasted;

                        //Log action in the log file
                        logTxt.Main(jediKnight.Name + " killed " + opponent.Name + " by Piercing his chest");
                    }

                    // Make a stab using the current DamageLevel cast to an int. Record which new damage level resulted from the attack.
                    System.Threading.Thread.Sleep(1000); // skip a heartbeat here, so we get a new random seed number
                    randomInt = ranGen.RandomInteger((int)(opponent.currentDamageLevel));

                    logTxt.Main("Jedi Knight random health level is: " + randomInt);

                    // Series of if statements, to determine which damage level is closest to the generated number
                    if (randomInt >= 50)
                        opponent.currentDamageLevel = DamageLevel.Healthy;
                    else if (randomInt < 50 && randomInt > 25)
                        opponent.currentDamageLevel = DamageLevel.Challenged;
                    else if (randomInt <= 25 && randomInt > 10)
                        opponent.currentDamageLevel = DamageLevel.Critical;
                    else // less than or equal to 10, we's long gone dude
                    {
                        opponent.currentDamageLevel = DamageLevel.Wasted;
                        opponent.Deceased = true;
                        jediKnight.numOfDeaths++;
                    }

                    jediKnight.fightLog.FightEvents.Add(opponent.Name + " now has a damage level of: " + opponent.currentDamageLevel + " based on a randomInt of " + randomInt);

                    // Pick a random slander expression for posterity, except if he is dead as a result of that last attack of yours.
                    if (opponent.currentDamageLevel > DamageLevel.Wasted)
                    {
                        randomInt = ranGen.RandomInteger(enemySlanders.Length - 1);
                        string slander = enemySlanders[randomInt];
                        jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " says: " + slander);
                    }
                    else if (opponent.currentDamageLevel == DamageLevel.Wasted)
                    {
                        if (opponent.Name == "Luke Skywalker")
                        {
                            Obi_Wan ObiWan = new Obi_Wan();
                            jediKnight.fightLog.FightEvents.Add(ObiWan.Encouragement);
                            jediKnight.fightLog.FightEvents.Add(ObiWan.Dismay);
                            ObiWan.SaveLuke(opponent);
                            logTxt.Main("Obi-Wan helps Luke Skywalker regain his health");
                            jediKnight.fightLog.FightEvents.Add(opponent.Name + " now has a damageeeee level of: " + opponent.currentDamageLevel);
                        }
                        else
                        {
                            jediKnight.fightLog.FightEvents.Add(opponent.Name + " hoarsely whispers: " + opponent.LastWords);
                            jediKnight.fightLog.FightEvents.Add(jediKnight.Name + " says: Rest in pieces, " + opponent.Name + ".");
                            jediKnight.fightLog.FightEvents.Add(opponent.Name + " has bought the big farm in the sky.");
                        }
                        opponent.numOfDeaths++;
                        logTxt.Main(opponent.Name + " has died " + opponent.numOfDeaths + " times");
                        logTxt.Main(jediKnight.Name + " has died " + jediKnight.numOfDeaths + " times");
                    }

                }
            }
            catch (Exception e)
            {
                // TODO: log the exception
                string msg = e.Message;
            }
        }
    }
}
