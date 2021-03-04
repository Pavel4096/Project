using System;

namespace Project
{
    public class CharModel
    {
        public float speed;
        public float turnSpeed;
        public float maxHealth;
        public string viewName = "Ethan";

        public CharModel() : this(5.0f, 180.0f, 1000.0f)
        {
        }

        public CharModel(float speed, float turnSpeed, float maxHealth)
        {
            this.speed = speed;
            this.turnSpeed = turnSpeed;
            this.maxHealth = maxHealth;
        }
    }
}
