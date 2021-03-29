using System;
using System.Collections.Generic;

namespace Project
{
    public sealed class RadarController : IController, IGameLoop, IControllerSupportFactory<RadarModel>, IDisposable
    {
        private RadarModel model;
        private IView view;
        private IGameController gameController;
        private RadarView radarView;
        private List<RadarElement> radarElements;
        private float sinceLastUpdate;

        public void Init(RadarModel model_, IView view_, IGameController gameController_)
        {
            model = model_;
            view = view_;
            gameController = gameController_;
            sinceLastUpdate = model.updateInterval;
            radarView = new RadarView();
            radarElements = new List<RadarElement>();
            gameController.controllerAdded += AddRadarElement;
            gameController.controllerRemoved += RemoveRadarElement;

            for(int i = 0, count = gameController.ControllerCount; i < count; i++)
            {
                AddRadarElement(gameController[i]);
            }
        }

        public void GameLoop(float time)
        {
            sinceLastUpdate += time;
            if(sinceLastUpdate >= model.updateInterval)
            {
                if(gameController.GetCharacterController() is ICanReturnPosition playerController)
                {
                    GameVector playerPosition = playerController.GetPosition();
                    float sinY = (float)Math.Sin(playerController.GetYAngle()*Math.PI/180);
                    float cosY = (float)Math.Cos(playerController.GetYAngle()*Math.PI/180);
                    radarView.UpdateCamera(playerPosition, playerController.GetYAngle());
                    foreach(var radarElement in radarElements)
                    {
                        radarElement.Update(playerPosition, sinY, cosY);
                    }
                }
                sinceLastUpdate = 0.0f;
            }
        }

        public void Dispose()
        {
            gameController.controllerAdded -= AddRadarElement;
            gameController.controllerRemoved -= RemoveRadarElement;
            foreach(var radarElement in radarElements)
            {
                radarElement.Dispose();
            }
        }

        private void AddRadarElement(IController controller)
        {
            if(controller is ICanReturnPosition)
                radarElements.Add(new RadarElement(controller, radarView));
        }

        private void RemoveRadarElement(IController controller)
        {
            foreach(var radarElement in radarElements)
            {
                if(radarElement.Controller == controller)
                {
                    radarElements.Remove(radarElement);
                    radarElement.Dispose();
                    break;
                }
            }
        }
    }
}
