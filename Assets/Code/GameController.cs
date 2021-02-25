using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class GameController : UnityEngine.Object
    {
        private Game gameView;
        private List<IController> controllers;
        private List<IInteractiveItem> interactiveItems;
        private IPlayerController currentController;

        private event System.Action<float> gameLoop;

        public GameController(Game gameView)
        {
            CharModel charModel = new CharModel();
            CameraModel cameraModel = new CameraModel();
            CameraController cameraController;
            IView charView;
            System.Random rnd = new System.Random();

            this.controllers = new List<IController>();
            this.interactiveItems = new List<IInteractiveItem>();
            this.gameView = gameView;
            charView = gameView.CreateView(charModel.viewName);
            this.currentController = new CharController(charModel, charView);

            cameraController = new CameraController(cameraModel, gameView.CreateView(cameraModel.viewName), charView);
            gameLoop += cameraController.GameLoop;
            this.controllers.Add(this.currentController);
            this.controllers.Add(cameraController);

            for(var i = 0; i < 10; i++)
            {
                DamagerModel model = new DamagerModel(rnd.Next(30, 61), 1.0f, new GameVector(rnd.Next(-800, 801)/100.0f, 0.0f, rnd.Next(-800, 801)/100.0f));
                IInteractiveItem item = new Damager(model, gameView.CreateView(model.viewName, model.position));
                this.interactiveItems.Add(item);
            }
        }

        public void GameLoop(UserInput userInput, float frameTime)
        {
            currentController.ProcessInput(userInput);
            gameLoop?.Invoke(frameTime);
            foreach(IInteractiveItem item in interactiveItems)
            {
                item.Check(currentController, frameTime);
            }
        }
    }
}
