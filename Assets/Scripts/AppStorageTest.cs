using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AppStorageTest : MonoBehaviour
    {
        private void Start()
        {
            float value = 0;
            if(AppStorage.KeyExists("someValue"))
                value = AppStorage.GetFloat("someValue");
            value += 1.5f;
            AppStorage.AddFloat("someValue", value);
        }
    }
}
