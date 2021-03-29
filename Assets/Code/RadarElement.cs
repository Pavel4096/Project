namespace Project
{
    public sealed class RadarElement : System.IDisposable
    {
        private IController controller;
        private GameVector position;
        private UnityEngine.GameObject image;

        public RadarElement(IController controller_, RadarView radarView)
        {
            controller = controller_;
            image = radarView.CreateImage(controller_);
        }

        public IController Controller
        {
            get => controller;
        }

        public void Update(GameVector playerPosition, float sinY, float cosY)
        {
            position = (playerPosition - (controller as ICanReturnPosition).GetPosition())/5.0f;
            if(position.magSquared() <= 1.0f)
            {
                position *= -125;
                image.transform.localPosition = new UnityEngine.Vector3(position.x*cosY-position.z*sinY, position.z*cosY+position.x*sinY, 0);
                image.SetActive(true);
            }
            else
                image.SetActive(false);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(image);
        }
    }
}
