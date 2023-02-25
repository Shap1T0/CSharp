using System;
using Patterns.Decorator.Interface;

namespace Patterns.Decorator.Decorators
{
    public class AmusementPark: WalkDecorator
    {
        public override Clothing Clothing { get; }

        public AmusementPark(IWalk walk, Clothing clothing) : base(walk)
        {
            Clothing = clothing;
        }

        public override string TakeWalk()
        {
           Walk.AddMoneyToWallet(100);
           Console.WriteLine("А здесь нам нужно взять немного денег");
           Console.WriteLine("Мы можем использовать состояние компонента и добавлять логику без дублиирования кода");

           return Walk.TakeWalk();
        }
    }
}