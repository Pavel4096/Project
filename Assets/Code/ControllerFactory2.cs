namespace Project
{
    public sealed class ControllerFactory2<TController, TModel> where TModel: Model
                                                                where TController: IController, IControllerSupportFactory<TModel>, new()
    {
        private TModel model;
        private IView view;
        private TController controller;

        public ControllerFactory2(TModel model_, Game gameView_, bool addToGameController_ = true)
        {
            model = model_;
            view = gameView_.CreateView(model.viewName);
            controller = new TController();
            controller.Init(model, view, gameView_.GameController);
            if(addToGameController_)
                gameView_.GameController.AddController(controller);
        }

        public TController Controller
        {
            get => controller;
            private set => controller = value;
        }
    }
}
