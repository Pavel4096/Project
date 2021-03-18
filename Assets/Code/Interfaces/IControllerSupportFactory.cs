namespace Project
{
    public interface IControllerSupportFactory<TModel>
    {
        void Init(TModel model, IView view, IGameController gameController);
    }
}
