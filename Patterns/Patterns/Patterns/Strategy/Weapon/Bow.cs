using System;
using Patterns.Strategy.Interface;

namespace Patterns.Strategy.Weapon
{
    public class Bow: IWeaponType
    {
        public int DamageDealt { get; }


        public Bow(int damageDealt)
        {
            DamageDealt = damageDealt;
        }
    }
}