namespace Project
{
    public sealed class DamagerModel2 : Model, IModel
    {
        public float damage;
        public float radius;
        public GameVector position;
        public Game gameView;

        private Damager2 controller;

        public DamagerModel2(Game gameView_) : this(50.0f, 1.0f, new GameVector(0.0f, 0.0f, 0.0f), gameView_)
        {

        }

        public DamagerModel2(float damage_, float radius_, GameVector position_, Game gameView_)
        {
            damage = damage_;
            radius = radius_*radius_;
            position = position_;
            viewName = "Damager";
            gameView = gameView_;
        }

        public IController GetController()
        {
            if(controller == null)
                controller = new ControllerFactory2<Damager2, DamagerModel2>(this, gameView, false).Controller;
            return controller;
        }
    }
}
