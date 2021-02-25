using System;

namespace Project
{
    public class NoAnimatorException : Exception
    {
        public NoAnimatorException() : base("No animator in this view.")
        {
        }
    }
}