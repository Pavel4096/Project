using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class GameController : UnityEngine.Object, IDisposable
    {
        private Game gameView;
        private List<IController> controllers;
        private List<IInteractiveItem> interactiveItems;
        private IPlayerController currentController;
        private bool gameActive;

        public event System.Action<float> gameLoop;

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
            gameLoop += this.currentController.GameLoop;
            if(currentController is IGameEnder gameEnder)
                gameEnder.OnGameEnded += EndGame;

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

            for(var i = 0; i < 8; i++)
            {
                ItemModel model = new ItemModel(0.5f, new GameVector(rnd.Next(-800, 801)/100.0f, 0.0f, rnd.Next(-800, 801)/100.0f));
                IInteractiveItem item = new Item(model, gameView.CreateView(model.viewName, model.position));
                this.interactiveItems.Add(item);
            }
            gameActive = true;
        }

        public void GameLoop(UserInput userInput, float frameTime)
        {
            if(gameActive)
            {
                currentController.ProcessInput(userInput);
                gameLoop?.Invoke(frameTime);
                foreach(IInteractiveItem item in interactiveItems)
                {
                    item.Check(currentController, frameTime);
                }
            }
        }

        private void EndGame(GameEndReason reason, int collectedItemsCount)
        {
            gameActive = false;
            if(currentController is IGameEnder gameEnder)
                gameEnder.OnGameEnded -= EndGame;
            currentController.Dispose();
        }

        public void Dispose()
        {
            foreach(IController controller in controllers)
                gameLoop -= controller.GameLoop;
        }
    }
}
