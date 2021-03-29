using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class Game : MonoBehaviour
    {
        private GameController gameController;

        public event System.Action FrameEnded;

        public IGameController GameController
        {
            get => (IGameController) gameController;
        }

        public IView CreateView(string name, GameVector position = new GameVector())
        {
            GameObject go = Resources.Load<GameObject>(name);
            GameObject go2 = Object.Instantiate(go);
            IView view = go2.GetComponent<IView>();
            go2.transform.position = new Vector3(position.x, position.y, position.z);

            if(view == null)
                throw new NoViewException(name);
            return view;
        }

        public void ProcessWaiter(GameRoutine routine)
        {
            gameController.ProcessWaiter(routine);
        }

        private IEnumerator ProcessGameRoutines()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                FrameEnded?.Invoke();
            }
        }

        private void Awake()
        {
            StartCoroutine(ProcessGameRoutines());
            gameController = new GameController(this);
            gameController.Init();
            //Object.DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            float frameTime = Time.deltaTime;
            UserInput userInput = new UserInput(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetButton("Fire1"));
            gameController.GameLoop(userInput, frameTime);
        }
    }
}
