using System;
using Patterns.Strategy.Interface;

namespace Patterns.Strategy.Armor
{
    public class ChainArmor: IArmorType
    {
        private int _blockDamage;

        public ChainArmor(int blockDamage)
        {
            _blockDamage = blockDamage;
        }

        public int GetBlockDamage()
        {
            return _blockDamage;
        }
    }
}