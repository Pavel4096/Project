using System;

namespace Project
{
    public class DamagerModel
    {
        public float damage;
        public float radius;
        public GameVector position;
        public string viewName = "Damager";

        public DamagerModel() : this(50.0f, 1.0f, new GameVector(0.0f, 0.0f, 0.0f))
        {
        }

        public DamagerModel(float damage, float radius, GameVector position)
        {
            this.damage = damage;
            this.radius = radius*radius;
            this.position = position;
        }
    }
}