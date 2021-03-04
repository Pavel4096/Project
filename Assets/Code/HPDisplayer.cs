using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public sealed class HPDisplayer
    {
        private Text hpText;

        public HPDisplayer()
        {
            GameObject gameObject = Resources.Load<GameObject>("HP");
            GameObject hpDisplayer = Object.Instantiate(gameObject, (Object.FindObjectOfType<Canvas>()).transform);
            hpText = hpDisplayer.GetComponentInChildren<Text>();
        }

        public void UpdateText(int value)
        {
            hpText.text = "HP: " + value;
        }
    }
}