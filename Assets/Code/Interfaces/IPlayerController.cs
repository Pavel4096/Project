namespace Project
{
    public interface IPlayerController : IController
    {
        void ProcessInput(UserInput userInput);
        void Damage(DamageType type, float value);
        GameVector GetPosition();
    }
}