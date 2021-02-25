using System;

namespace Project
{
    public struct UserInput
    {
        public float vertical;
        public float horizontal;
        public bool fire;

        public UserInput(float vertInput, float horInput, bool fireInput)
        {
            vertical = vertInput;
            horizontal = horInput;
            fire = fireInput;
        }
    }
}