using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    delegate TReturn SomeName<in TArg, out TReturn>(TArg data);

    public class Base
    {
        public string someString = "Message";
    }

    public class Derived : Base
    {

    }

    public class CovarianceContravariance : MonoBehaviour
    {
        void Start()
        {
            SomeName<Derived, Base> callIt = SomeMethod;
            callIt(new Derived());
        }

        public static Derived SomeMethod(Base data)
        {
            Debug.Log(data.someString);
            return new Derived();
        }
    }
}
