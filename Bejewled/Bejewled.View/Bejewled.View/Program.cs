namespace Bejewled.View
{
    class Program : GameEnvironment
    {
        private static void Main()
        {
            using (var game = new BejeweledView())
            {
                game.Run();
            }
        }      
    }
}