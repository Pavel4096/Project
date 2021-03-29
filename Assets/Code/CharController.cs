using System;

namespace Project
{
    public class CharController : IPlayerController, IGameEnder
    {
        public event Action<int> OnHPChange;
        public event Action<GameEndReason, int> OnGameEnded;

        private CharModel model;
        private IView view;
        private float currentHealth;
        private int collectedItemsCount;
        private HPDisplayer hpDisplayer;
        private ResultDisplayer resultDisplayer;

        public CharController(CharModel model, IView view)
        {
            this.model = model;
            this.view = view;
            currentHealth = this.model.maxHealth;
            collectedItemsCount = 0;
            hpDisplayer = new HPDisplayer();
            resultDisplayer = new ResultDisplayer();

            OnHPChange += hpDisplayer.UpdateText;
            OnGameEnded += resultDisplayer.GameEnded;
        }

        public void ProcessInput(UserInput userInput)
        {
            float forwardSpeed = userInput.vertical;
            float turnSpeed = -userInput.horizontal;

            if(forwardSpeed < 0)
                forwardSpeed = 0.0f;
            view.SetAnimatorParameter("Speed", forwardSpeed);
            view.SetAnimatorParameter("TurnSpeed", turnSpeed);
        }

        public GameVector GetPosition()
        {
            return view.GetPosition();
        }

        public void Damage(DamageType type, float value)
        {
            currentHealth -= value;
            if(currentHealth <= 0)
            {
                currentHealth = 0.0f;
                OnGameEnded?.Invoke(GameEndReason.ZeroHP, collectedItemsCount);
            }
            OnHPChange?.Invoke((int)currentHealth);
        }

        public void AddItem()
        {
            collectedItemsCount++;
            if(collectedItemsCount >= 5)
            {
                OnGameEnded?.Invoke(GameEndReason.ItemsCollected, collectedItemsCount);
            }
        }

        public void GameLoop(float time)
        {
        }

        public void Dispose()
        {
            OnHPChange -= hpDisplayer.UpdateText;
            OnGameEnded -= resultDisplayer.GameEnded;
            view.SetAnimatorParameter("Speed", 0.0f);
            view.SetAnimatorParameter("TurnSpeed", 0.0f);
        }
    }
}
