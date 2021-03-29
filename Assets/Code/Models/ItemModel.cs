using System;

namespace Project
{
    public class ItemModel
    {
        public bool active;
        public float radius;
        public GameVector position;
        public string viewName= "Item";

        public ItemModel() : this(1.0f, new GameVector(0.0f, 0.0f, 0.0f))
        {
        }

        public ItemModel(float radius, GameVector position)
        {
            this.radius = radius;
            this.position = position;
            active = true;
        }
    }
}