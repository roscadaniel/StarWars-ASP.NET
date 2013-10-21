using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarWars.Helpers;

namespace StarWars.Models
{
    public class Luke : JediKnight
    {
        public Luke()
        {
            this.Name = "Luke Skywalker";
            this.Email = "ls@tattooine.com";
            this.LightSaberColor = "Blue";
            this.currentDamageLevel = JediKnight.DamageLevel.Healthy;
            this.DarkSide = false;
            this.Deceased = false;
            this.fightLog = new AttackRecorder();
            this.fightLog.FightEvents = new List<string>()
	            {
	                this.Name + " loosens his tie and straps on Leia's bra for verisimilitude.",
	                this.Name + " unstraps his lightsaber. It's blue!",
	                this.Name + " looks at his Dad and quotes Groucho Marx: 'I have a mind to join a club and beat you over the head with it.'",
                    this.Name + " privately thinks of joining a different club altogether. Preferably one in a galaxy far, far away.."
	            };

            this.LastWords = "Aaaaargh, I never thought to take out life insurance! How pathetic of me in this cruel, cruel world.";
        }
    }
}