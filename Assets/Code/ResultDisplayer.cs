using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Project
{
    public sealed class ResultDisplayer
    {
        private GameObject displayer;
        private GameObject restarter;
        private Canvas canvas;
        private Text resultText;
        private string[] words;

        public ResultDisplayer()
        {
            GameObject gameObject = Resources.Load<GameObject>("Result");
            displayer = Object.Instantiate(gameObject, Canvas.transform);
            resultText = displayer.GetComponentInChildren<Text>();
            displayer.SetActive(false);
            words = new string[]{"предметов", "предмет", "предмета"};
        }

        public Canvas Canvas
        {
            get
            {
                if(canvas == null)
                    canvas = Object.FindObjectOfType<Canvas>();
                return canvas;
            }
        }

        public void GameEnded(GameEndReason reason, int collectedItemsCount)
        {
            switch(reason)
            {
                case GameEndReason.ItemsCollected:
                    resultText.text = $"Вы собрали {collectedItemsCount} {CorrectWord(collectedItemsCount, words)}!";
                    break;
                case GameEndReason.ZeroHP:
                    resultText.text = $"HP = 0. Вы успели собрать только {collectedItemsCount} {CorrectWord(collectedItemsCount, words)}.";
                    break;
                default:
                    resultText.text = $"Произошла ошибка. Вы успели собрать толькл {collectedItemsCount} {CorrectWord(collectedItemsCount, words)}.";
                    break;
            }
            displayer.SetActive(true);
            GameObject gameObject = Resources.Load<GameObject>("Restart");
            restarter = Object.Instantiate(gameObject, Canvas.transform);
            restarter.GetComponentInChildren<Button>().onClick.AddListener(Restart);
        }

        private void Restart()
        {
            SceneManager.LoadScene("Game");
        }

        private string CorrectWord(int value, string[] words)
        {
            if(words.Length != 3)
                throw new System.ArgumentException("В массиве должно быть 3 слова.");

            if(value >= 11 && value <= 14)
                return words[0];
            value = value % 10;
            if(value == 1)
                return words[1];
            else if(value >= 2 && value <= 4)
                return words[2];
            else
                return words[0];
        }
    }
}