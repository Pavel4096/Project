namespace Project
{
    public static class Extensions
    {
        public static void Start(this System.Collections.IEnumerator enumerator)
        {
            Game game = UnityEngine.Object.FindObjectOfType<Game>();
            if(game == null)
                throw new System.NullReferenceException("Game object was not found.");
            game.ProcessWaiter(new GameRoutine(enumerator));
        }
    }
}
