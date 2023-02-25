using System;
using Patterns.Strategy.Interface;

namespace Patterns.Strategy.Weapon
{
    public class Sword: IWeaponType
    {
        public Sword(int damageDealt)
        {
            DamageDealt = damageDealt;
        }

        public int DamageDealt { get; }

    }
}