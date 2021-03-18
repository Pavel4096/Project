using System;

namespace Project
{
    public class CameraController : IController, IGameLoop
    {
        private CameraModel model;
        private IView view;
        private IView target;

        public CameraController(CameraModel model, IView view, IView target)
        {
            this.model = model;
            this.view = view;
            this.target = target;
        }

        public void GameLoop(float time)
        {
            view.SetPosition(target.GetForwardDirection()*-model.distance + target.GetPosition() + new GameVector(0.0f, model.height, 0.0f));
            view.RotateTo(target.GetPosition());
        }
    }
}