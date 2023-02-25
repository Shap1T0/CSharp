using System;
using Patterns.Decorator.Interface;

namespace Patterns.Decorator.Decorators
{
    public class Park: WalkDecorator
    {
        public override Clothing Clothing { get; }

        public Park(IWalk walk, Clothing clothing) : base(walk)
        {
            Clothing = clothing;
        }

        public override string TakeWalk()
        {
            TakeOutTrash();
            ChangeClothes();
            return Walk.TakeWalk();
        }

        public void TakeOutTrash()
        {
            Console.WriteLine("Перед прогулкой мы можем вынести мусор");
        }

        public void ChangeClothes()
        {
            Walk.Clothing.Color = Clothing.Color;
            Console.WriteLine("И надеть одежду другого цвета");
        }
    }
}