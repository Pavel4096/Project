using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class GameController : IGameController, IDisposable
    {
        private Game gameView;
        private List<IController> controllers;
        private List<IInteractiveItem> interactiveItems;
        private IPlayerController currentController;
        private bool gameActive;
        private RadarController radar;
        private ItemRepository itemRepository;

        private Queue<GameRoutine> endOfFrameRoutines;
        private Queue<GameRoutine> timeRoutines;

        public event Action<float> gameLoop;
        public event Action<IController> controllerAdded;
        public event Action<IController> controllerRemoved;

        public GameController(Game gameView_)
        {
            gameView = gameView_;
        }
        
        public void Init()
        {
            CharModel charModel = new CharModel();
            CameraModel cameraModel = new CameraModel();
            CameraController cameraController;
            IView charView;
            System.Random rnd = new System.Random();

            endOfFrameRoutines = new Queue<GameRoutine>(10);
            timeRoutines = new Queue<GameRoutine>(10);
            gameView.FrameEnded += ProcessEndOfFrameGameRoutines;
            controllers = new List<IController>();
            interactiveItems = new List<IInteractiveItem>();
            charView = gameView.CreateView(charModel.viewName);
            currentController = new CharController(charModel, charView);
            AddController(currentController);
            if(currentController is IGameEnder gameEnder)
                gameEnder.OnGameEnded += EndGame;

            cameraController = new CameraController(cameraModel, gameView.CreateView(cameraModel.viewName), charView);
            AddController(cameraController);

            for(var i = 0; i < 10; i++)
            {
                DamagerModel model = new DamagerModel(rnd.Next(30, 61), 1.0f, new GameVector(rnd.Next(-800, 801)/100.0f, 0.0f, rnd.Next(-800, 801)/100.0f));
                IInteractiveItem item = new Damager(model, gameView.CreateView(model.viewName, model.position));
                AddController(item as IController);
                interactiveItems.Add(item);
            }

            itemRepository = new ItemRepository("data");

            for(var i = 0; i < 8; i++)
            {
                ItemModel model = new ItemModel(0.5f, new GameVector(rnd.Next(-800, 801)/100.0f, 0.0f, rnd.Next(-800, 801)/100.0f));
                IInteractiveItem item = new Item(model, gameView.CreateView(model.viewName, model.position));
                AddController(item as IController);
                interactiveItems.Add(item);
                itemRepository.Save("item" + i, item as Item);
            }
            radar = (new ControllerFactory<RadarController, RadarModel>(new RadarModel(), gameView)).Controller;
            gameActive = true;

            SampleGameRoutine().Start();
            CreateGameRoutine(SampleGameRoutine2());
        }

        public IController this[int i]
        {
            get => controllers[i];
        }

        public int ControllerCount
        {
            get => controllers.Count;
        }

        public void AddController(IController controller)
        {
            if(controller == null)
                return;
            if(controller is IGameLoop gameLoopController)
                gameLoop += gameLoopController.GameLoop;
            controllers.Add(controller);
            controllerAdded?.Invoke(controller);
        }

        public bool RemoveController(IController controller)
        {
            bool result;

            if(controller == null)
                return false;
            if(controller is IGameLoop gameLoopController)
                gameLoop -= gameLoopController.GameLoop;
            result = controllers.Remove(controller);
            controllerRemoved?.Invoke(controller);

            return result;
        }

        public IController GetCharacterController() => currentController;

        public void GameLoop(UserInput userInput, float frameTime)
        {
            for(var i = timeRoutines.Count; i > 0; i--)
            {
                GameRoutine routine = timeRoutines.Dequeue();
                if(routine.Waiter.SubtractTime(frameTime) > 0)
                {
                    timeRoutines.Enqueue(routine);
                }
                else
                {
                    routine.Execute();
                    ProcessWaiter(routine);
                }
            }
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

        public void CreateGameRoutine(System.Collections.IEnumerator routine)
        {
            ProcessWaiter(new GameRoutine(routine));
        }

        public void ProcessWaiter(GameRoutine routine)
        {
            switch(routine.Waiter.Reason)
            {
                case WaitReason.EndOfFrame:
                    endOfFrameRoutines.Enqueue(routine);
                    break;
                case WaitReason.Time:
                    timeRoutines.Enqueue(routine);
                    break;
            }
        }

        public System.Collections.IEnumerator SampleGameRoutine()
        {
            while(true)
            {
                Debug.Log("Message from SampleGameRoutine()");
                yield return new Waiter(WaitReason.Time, 5.0f);
            }
        }

        public System.Collections.IEnumerator SampleGameRoutine2()
        {
            while(true)
            {
                yield return new Waiter(WaitReason.EndOfFrame);
                if((Time.frameCount % 50) == 0)
                    Debug.Log("Frame count: " + Time.frameCount);
            }
        }

        private void ProcessEndOfFrameGameRoutines()
        {
            for(var i = endOfFrameRoutines.Count; i > 0; i--)
            {
                GameRoutine routine = endOfFrameRoutines.Dequeue();
                routine.Execute();
                ProcessWaiter(routine);
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
            gameView.FrameEnded -= ProcessEndOfFrameGameRoutines;
            foreach(IController controller in controllers)
                if(controller is IGameLoop gameLoopController)
                    gameLoop -= gameLoopController.GameLoop;
        }
    }
}
