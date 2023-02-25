
namespace Patterns.Decorator.Interface
{
    public interface IWalk
    {
        Clothing Clothing { get; }
        string TakeWalk();
        void AddMoneyToWallet(int money);
    }
}