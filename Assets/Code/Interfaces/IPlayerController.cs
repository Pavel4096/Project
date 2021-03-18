using System;

namespace Project
{
    public interface IPlayerController : IController, ICanReturnPosition, IDisposable
    {
        void ProcessInput(UserInput userInput);
        void Damage(DamageType type, float value);
        void AddItem();
    }
}