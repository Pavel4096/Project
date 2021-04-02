namespace Project
{
    public interface IInteractiveItem
    {
        void Check((IPlayerController player, float time) data);
    }
}