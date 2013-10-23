using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using StarWars.Helpers;

namespace StarWars.Models
{
    // Star Warrior base class - the Obi Wan Kenobi
    public class Obi_Wan : JediKnight
    {
        public string Encouragement = "Obi-Wan: Come on, Luke. Use the fart, Luke!";
        public string Dismay = "Obi-Wan: Luke. Concentrate, boy - why did it ever come to this??? Cream him, dammit!!";

        public Obi_Wan()
        {
            this.Name = "Obi-Wan Kenobi";
            this.Email = "obiwan@tattooine.com";
            this.LightSaberColor = "Blue";
            this.currentDamageLevel = JediKnight.DamageLevel.Healthy;
            this.DarkSide = false;
            this.Deceased = false;
            this.fightLog = new AttackRecorder();
            this.fightLog.FightEvents = new List<string>()
            {
            //    Encouragement,
            //    Dismay
            };

            this.LastWords = "Aaaaargh, I never thought to take out life insurance! How pathetic of me in this cruel, cruel world.";
        }

        public void SaveLuke(JediKnight inst)
        {
            //Reset the damage level
            inst.currentDamageLevel = JediKnight.DamageLevel.Healthy;
        }

    }
}