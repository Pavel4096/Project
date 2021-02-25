using System;

namespace Project
{
    public class CameraModel
    {
        public float distance;
        public float height;
        public string viewName = "Camera";

        public CameraModel() : this(5.0f, 4.0f)
        {
        }

        public CameraModel(float distance, float height)
        {
            this.distance = distance;
            this.height = height;
        }
    }
}