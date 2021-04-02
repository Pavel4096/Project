namespace Project
{
    public sealed class Damager2 : IController, IInteractiveItem, IControllerSupportFactory<DamagerModel2>
    {
        private DamagerModel2 model;
        private IView view;

        public Damager2()
        {
        }

        public void Check((IPlayerController player, float time) data)
        {
            if(model.position.DistanceTo(data.player.GetPosition()) < model.radius)
                data.player.Damage(DamageType.Damager, model.damage * data.time);
        }

        public void Init(DamagerModel2 model_, IView view_, IGameController gameController_)
        {
            model = model_;
            view = view_;
        }
    }
}
