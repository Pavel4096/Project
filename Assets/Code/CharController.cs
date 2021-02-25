using System;

namespace Project
{
    public class CharController : IController, IPlayerController
    {
        private CharModel model;
        private IView view;
        private float currentHealth;

        public CharController(CharModel model, IView view)
        {
            this.model = model;
            this.view = view;
            this.currentHealth = this.model.maxHealth;
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
            if(currentHealth < 0)
                currentHealth = 0.0f;
        }

        public void GameLoop(float time)
        {
        }
    }
}
