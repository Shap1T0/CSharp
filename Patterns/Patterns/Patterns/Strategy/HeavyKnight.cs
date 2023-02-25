using Patterns.Strategy.Armor;
using Patterns.Strategy.Weapon;

namespace Patterns.Strategy
{
    public class HeavyKnight: Hero
    {
        /// <summary>
        /// Вопрос с композицией! (Агрегация)
        /// Автор книги утверждает "Предпочитайте композицию наследованию"
        /// Композиция это когда один объект не может существовать без другого Больше похоже на хардкод!
        /// </summary>
        public HeavyKnight()
        {
            ArmorType = new ChainArmor(13);
            WeaponType = new TwoHandedSword(14);
        }
    }
}