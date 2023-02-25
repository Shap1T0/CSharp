using System;
using Patterns.Strategy.Interface;

namespace Patterns.Strategy.Weapon
{
    public class TwoHandedSword: IWeaponType
    {
        public int DamageDealt { get; }

        public TwoHandedSword(int damageDealt)
        {
            DamageDealt = damageDealt;
        }
    }
}