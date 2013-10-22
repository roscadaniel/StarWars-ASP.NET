using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarWars.Models;
using StarWars.Helpers;

namespace StarWars.Controllers
{
    public class HomeController : Controller
    {

        // GET: /Home/
        // ejdsldkj
        //one more line here
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good morning" : "Good afternoon";

            Logging logTxt = new Helpers.Logging();
            logTxt.Main("our text here");

            return View();
        }

        [HttpGet]
        public ViewResult RecruitForm()
        {
            return View();
        }

        [HttpPost]
        // public ViewResult RecruitForm(JediKnight jediKnight)
        public ViewResult RecruitForm(ThePlayer newWarrior)
        {
            // check user input
            if (ModelState.IsValid)
            {
                // Set up a game log of Fight Events
                AttackRecorder gameLog = new AttackRecorder();
                gameLog.FightEvents = new List<string>() { "..the game log was the only book in town that the Evil Emperor wanted to read." };

                // Set up a Jedi Warrior object - using the data entered in the form
                ThePlayer myWarrior = new ThePlayer();
                myWarrior.Name = newWarrior.Name;
                myWarrior.Email = newWarrior.Email;
                myWarrior.LightSaberColor = newWarrior.LightSaberColor;
                myWarrior.currentDamageLevel = JediKnight.DamageLevel.Healthy;
                myWarrior.DarkSide = newWarrior.DarkSide;
                myWarrior.Deceased = false;
                myWarrior.fightLog = new AttackRecorder();

                // Init the personal fight log
                myWarrior.fightLog.FightEvents = new List<string>()
	            {
	                myWarrior.Name + " loosens his tie",
	                myWarrior.Name + " unstraps his lightsaber and admires it's wonderfully " + myWarrior.LightSaberColor + " sheen.",
	                myWarrior.Name + " quotes Caesar: 'Jacta Alea Est', dude!",
                    myWarrior.Name + " shuffles his feet and looks a bit shyly at the other guys.. quite imposing figures, actually.."
	            };
                // start up first knight in game log
                gameLog.FightEvents.AddRange(myWarrior.fightLog.FightEvents);

                // Set up some more Jedi objects
                Darth DarthV = new Darth();
                // start up second knight in game log
                gameLog.FightEvents.AddRange(DarthV.fightLog.FightEvents);

                Luke LukeS = new Luke();
                // start up third knight in game log
                gameLog.FightEvents.AddRange(LukeS.fightLog.FightEvents);

                //Set up Obi Wam
                Obi_Wan ObiWan = new Obi_Wan();
                gameLog.FightEvents.Add(ObiWan.Encouragement);
                gameLog.FightEvents.Add(ObiWan.Dismay);

                EvilEmperor Palpatine = new EvilEmperor();
                gameLog.FightEvents.Add(Palpatine.Encouragement);
                gameLog.FightEvents.Add(Palpatine.Chew);

                List<JediKnight> myJedis = new List<JediKnight>();
                myJedis.Add(myWarrior);
                myJedis.Add(DarthV);
                myJedis.Add(LukeS);

                RandomGenerator ranGen = new Helpers.RandomGenerator();

                // Now, let's run a battle scenario umpteen times over, based on the logic in the model for each Knight.
                // That ought to be enough to kill one of our protagonists outright a few times over..
                for (int i = 1; i < 18; i++)
                {
                    // Pick a random Jedi Knight to kick into action                
                    int randomInt = ranGen.RandomInteger(myJedis.Count() - 1, 0);
                    JediKnight warrior = myJedis[randomInt];
                    // Empty the individual fight log before next batch recording of events
                    warrior.fightLog.FightEvents.Clear();
                    warrior.fightLog.FightEvents.Add("The Evil Emperor randomly points a bony finger at " + warrior.Name + " and says: 'do some nasty work for me - now!'");


                    while (Palpatine.PickJediKnight(myJedis, randomInt) == null)
                    {
                        randomInt = ranGen.RandomInteger(myJedis.Count);
                    }
                    // Attack opponents, but only if they are on the opposing side, and only if you aren't dead yourself..yet.                                      
                    switch (warrior.Name)
                    {

                        case "Darth Vader":
                            if (!warrior.Deceased)
                            {
                                warrior.AttackEnemy(warrior, LukeS);
                                if (myWarrior.DarkSide == false) { warrior.AttackEnemy(warrior, myWarrior); }
                            }
                            else // this happens if the Emperor points a bony finger at a dead Jedi Knight
                            {
                                warrior.fightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is rather actively knockin' on the Force's Door.");
                                // Darth is a mean ole bean, so we'll re-life him with a lousy medicare allowance of 'Hurting'
                                warrior.Deceased = false;
                                warrior.currentDamageLevel = JediKnight.DamageLevel.Hurting;
                                warrior.fightLog.FightEvents.Add("Wow, that door-mojo worked... " + warrior.Name + " is in a rather hurt condition, but still bouncing back for some mean ole revenge!");
                            }
                            // add player fight events to game log
                            gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                            break;

                        case "Luke Skywalker":
                            if (!warrior.Deceased)
                            {
                                warrior.AttackEnemy(warrior, DarthV);
                                if (myWarrior.DarkSide == true) { 
                                    warrior.AttackEnemy(warrior, myWarrior);
                                    ObiWan.SaveLuke(warrior);
                                }
                            }
                            else
                            {
                                warrior.fightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is feebly knockin' on the Force's Door, in a Lukey way.");
                                // give Luke an advantage when killed, by rescusiating him and by setting his health to max
                                warrior.Deceased = false;
                                warrior.currentDamageLevel = JediKnight.DamageLevel.Healthy;
                                warrior.fightLog.FightEvents.Add(warrior.Name + " apparently has mucho clout with the Force's Door and is now bouncing back for some swashbuckling revenge!");
                            }
                            // add player fight events to game log
                            gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                            break;

                        //case "Palpatine":
                        //    randomInt = ranGen.RandomInteger(myJedis.Count);
                            
                        //    while (Palpatine.PickJediKnight(myJedis, randomInt) == null)
                        //    {
                        //        randomInt = ranGen.RandomInteger(myJedis.Count);
                        //    }

                        //    break;

                        // default case will be our guy on the dance floor wanting a serious piece of the action
                        default:
                            if (!warrior.Deceased)
                            {
                                if (warrior.DarkSide == false) { warrior.AttackEnemy(warrior, DarthV); }
                                if (warrior.DarkSide == true) { warrior.AttackEnemy(warrior, LukeS); }
                            }
                            else
                            {
                                warrior.fightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is knockin' on the Force's Door.");
                                warrior.Deceased = false;
                                warrior.currentDamageLevel = JediKnight.DamageLevel.Challenged;
                                warrior.fightLog.FightEvents.Add(warrior.Name + " apparently has some clout with the Force's Door and is now bent on some challenged-level revenge!");
                            }
                            // add player fight events to game log
                            gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                            break;
                    }
                }

                // Conclude fight at this point - check who's still vital, and do the victory roll
                foreach (JediKnight warrior in myJedis)
                {
                    if (!warrior.Deceased)
                    {
                        gameLog.FightEvents.Add("Ho ho ho! " + warrior.Name + "'s still around, with a rude health of: " + warrior.currentDamageLevel);
                        gameLog.FightEvents.Add(warrior.Name+ " has died " + warrior.numOfDeaths +" times");
                    }
                    else
                    {
                        gameLog.FightEvents.Add("Awww, " + warrior.Name + " is sleeping with the fishes!");
                        gameLog.FightEvents.Add(warrior.Name + " has died " + warrior.numOfDeaths + " times");
                    }
                }

                // Set up viewbag list of event strings
                ViewBag.FightDescription = new List<string> { "A long time ago in a galaxy far, far away...." };

                // put the game log contents into the ViewBag
                foreach (string FightEvent in gameLog.FightEvents)
                {
                    ViewBag.FightDescription.Add(FightEvent);
                }

                return View("Jedi_Vs_Sith", myWarrior);
            }
            else
            {
                // validation error - redisplay form with error messages
                return View();
            }

        }
    }
}

