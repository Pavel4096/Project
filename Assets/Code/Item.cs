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

        public ItemSavedData GetSavedData()
        {
            var savedData = new ItemSavedData();

            savedData.active = model.active;
            savedData.radius = model.radius;
            savedData.position = model.position;

            return savedData;
        }

        public void LoadSavedData(ItemSavedData data)
        {
            model.active = data.active;
            model.radius = data.radius;
            model.position = data.position;
            view.SetPosition(model.position);
        }
    }
}