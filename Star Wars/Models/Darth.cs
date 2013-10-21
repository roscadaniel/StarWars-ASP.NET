using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarWars.Helpers;

namespace StarWars.Models
{
    public class Darth : JediKnight
    {
        public Darth()
        {
            this.Name = "Darth Vader";
            this.Email = "dv@deathstar.com";
            this.LightSaberColor = "Red";
            this.currentDamageLevel = JediKnight.DamageLevel.Healthy;
            this.DarkSide = true;
            this.Deceased = false;
            this.fightLog = new AttackRecorder();
            this.fightLog.FightEvents = new List<string>()
	            {
	                this.Name + " loosens his tie, or rather, what he *thinks* is his tie. And no, you don't want to know any more.",
	                this.Name + " unstraps his lightsaber. It's red!",
	                this.Name + " quotes Caesar: 'Jacta Alea Est', dude! Come get some, Luke..",
                    this.Name + " expels a horribly dry laugh from his asthmatic lungs and sneers at Luke." 
	            };

            this.LastWords = "Aaaaargh, I thought that little bugger was on top of the generator!";
        }
    }
}