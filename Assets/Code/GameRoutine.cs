using System.Collections;

namespace Project
{
    public sealed class GameRoutine
    {
        private IEnumerator routine;
        private Waiter waiter;

        public GameRoutine(IEnumerator routine_)
        {
            routine = routine_;
            Execute();
        }

        public Waiter Waiter
        {
            get => waiter;
            private set => waiter = value;
        }

        public void Execute()
        {
            if(routine.MoveNext())
            {
                object result = routine.Current;
                if(result is Waiter waiter)
                {
                    Waiter = waiter;
                }
                else
                    Waiter = new Waiter(WaitReason.None);
            }
            else
                Waiter = new Waiter(WaitReason.None);
        }
    }
}
