using System;

namespace Project
{
    public class NoViewException : Exception
    {
        public NoViewException() : base("No view in this object.")
        {
        }

        public NoViewException(string value) : base($"No view in '{value}' object.")
        {
        }
    }
}