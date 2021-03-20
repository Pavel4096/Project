using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AppStorageTest : MonoBehaviour
    {
        private void Start()
        {
            AppStorage storage = new AppStorage("data.xml");
            float value = 0;
            if(storage.KeyExists("someValue"))
                value = storage.GetFloat("someValue");
            value += 1.5f;
            storage.AddFloat("someValue", value);
        }
    }
}
