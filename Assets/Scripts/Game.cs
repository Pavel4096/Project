using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class Game : MonoBehaviour
    {
        private GameController gameController;

        public void Log<T>(T message) where T: struct
        {
            Debug.Log(message);
        }

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

        public void ProcessWaiter(IEnumerator enumerator, Waiter waiter)
        {

        }

        private void Awake()
        {
            gameController = new GameController(this);
            gameController.Init();
            //Object.DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            UserInput userInput = new UserInput(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetButton("Fire1"));
            gameController.GameLoop(userInput, Time.deltaTime);
        }
    }
}
