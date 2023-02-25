using System;
using Patterns.Decorator.Interface;

namespace Patterns.Decorator
{
    class Elf: IWalk
    {
        public Clothing Clothing { get; }
        public int Wallet { get; private set; }

        public Elf(Clothing clothing, int wallet)
        {
            Clothing = clothing;
            Wallet = wallet;
        }

        public string TakeWalk()
        {
            return "Эльф прогуливается";
        }

        public void AddMoneyToWallet(int money)
        {
            Wallet += money;
        }
    }
}
