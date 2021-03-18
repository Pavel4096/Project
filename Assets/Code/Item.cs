using System;

namespace Project
{
    public sealed class Item : IController, ICanReturnPosition, IInteractiveItem
    {
        private ItemModel model;
        private IView view;

        public Item(ItemModel model, IView view)
        {
            this.model = model;
            this.view = view;
        }

        public GameVector GetPosition()
        {
            return view.GetPosition();
        }

        public float GetYAngle()
        {
            return view.GetYAngle();
        }

        public void Check(IPlayerController player, float time)
        {
            if(model.active && model.position.DistanceTo(player.GetPosition()) < model.radius)
            {
                player.AddItem();
                model.active = false;
            }
        }
    }
}