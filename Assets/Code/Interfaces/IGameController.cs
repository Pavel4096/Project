namespace Project
{
    public interface IGameController
    {
        event System.Action<float> gameLoop;
        event System.Action<IController> controllerAdded;
        event System.Action<IController> controllerRemoved;
        IController this[int i] { get; }
        int ControllerCount {get;}
        void AddController(IController controller);
        bool RemoveController(IController controller);
        IController GetCharacterController();
    }
}
