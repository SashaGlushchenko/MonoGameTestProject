using System;

namespace TellurionTestProject
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new GameMain();
            game.Run();
        }
    }
}
