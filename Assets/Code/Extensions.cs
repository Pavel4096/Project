namespace Project
{
    public static class Extensions
    {
        public static void Start(this System.Collections.IEnumerator enumerator)
        {
            if(enumerator.MoveNext())
            {
                object result = enumerator.Current;
                if(result is Waiter waiter)
                {
                    Game game = UnityEngine.Object.FindObjectOfType<Game>();
                    if(game != null)
                        game.ProcessWaiter(enumerator, waiter);
                }
            }
        }
    }
}
