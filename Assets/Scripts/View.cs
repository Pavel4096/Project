using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class View : MonoBehaviour, IView
    {
        private Transform viewTransform;
        private Animator viewAnimator;

        public GameVector GetPosition()
        {
            Vector3 position = viewTransform.position;

            return new GameVector(position.x, position.y, position.z);
        }

        public float GetYAngle()
        {
            return viewTransform.eulerAngles.y;
        }

        public void SetPosition(GameVector position)
        {
            viewTransform.position = new Vector3(position.x, position.y, position.z);
        }

        public void SetAnimatorParameter(string name, float value)
        {
            if(viewAnimator == null)
                throw new NoAnimatorException();

            viewAnimator.SetFloat(name, value);
        }

        public GameVector GetForwardDirection()
        {
            Vector3 direction = viewTransform.forward;

            return new GameVector(direction.x, direction.y, direction.z);
        }

        public void RotateTo(GameVector position)
        {
            Vector3 targetPosition = new Vector3(position.x, position.y, position.z);
            viewTransform.rotation = Quaternion.LookRotation(targetPosition - viewTransform.position, Vector3.up);
        }

        private void Start()
        {
            viewTransform = gameObject.GetComponent<Transform>();
            viewAnimator = gameObject.GetComponent<Animator>();
        }
    }
}
