using Patterns.Decorator.Interface;

namespace Patterns.Decorator.Decorators
{
    public abstract class WalkDecorator: IWalk
    {
        public abstract Clothing Clothing { get; }

        public IWalk Walk;

        protected WalkDecorator(IWalk walk)
        {
            Walk = walk;
        }

        public abstract string TakeWalk();
        public void AddMoneyToWallet(int money) { }
    }
}