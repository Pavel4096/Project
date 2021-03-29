namespace Project
{
    public sealed class Waiter
    {
        private WaitReason reason;
        private float seconds;

        public Waiter(WaitReason reason_, float seconds_ = 0.0f)
        {
            reason = reason_;
            seconds = seconds_;
        }

        public WaitReason Reason
        {
            get => reason;
        }

        public float Seconds
        {
            get => seconds;
        }

        public float SubtractTime(float seconds_)
        {
            seconds -= seconds_;
            return seconds;
        }
    }
}
