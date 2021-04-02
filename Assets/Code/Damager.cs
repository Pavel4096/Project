using System;

namespace Project
{
    public class Damager : IController, IInteractiveItem
    {
        private DamagerModel model;
        private IView view;

        public Damager(DamagerModel model, IView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Check((IPlayerController player, float time) data)
        {
            if(model.position.DistanceTo(data.player.GetPosition()) < model.radius)
                data.player.Damage(DamageType.Damager, model.damage*data.time);
        }
    }
}