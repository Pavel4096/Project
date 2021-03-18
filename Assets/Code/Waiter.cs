namespace Project
{
    public sealed class Waiter
    {
        private WaitReason reason;
        private float seconds;

        public WaitReason Reason
        {
            get => reason;
        }

        public float Seconds
        {
            get => seconds;
        }
    }
}
