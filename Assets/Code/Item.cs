using System;

namespace Project
{
    public sealed class Item : IInteractiveItem
    {
        private ItemModel model;
        private IView view;

        public Item(ItemModel model, IView view)
        {
            this.model = model;
            this.view = view;
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