﻿using Roguelike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  RogueLike

{
    internal class Medic: Profession
    {
        public Medic(string nameprofession, int strength, int medicine, int agility)
        {
            this.NameProfession = nameprofession;
            this.Strength = strength;
            this.Medicine = medicine;
            this.Agility = agility;
        }
    }
}
