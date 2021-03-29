namespace Project
{
    public class RadarModel : Model
    {
        public float radarRadius;
        public float updateInterval;

        public RadarModel()
        {
            radarRadius = 5.0f;
            updateInterval = 0.2f;
            viewName = "Radar";
        }
    }
}
