using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public sealed class RadarView
    {
        private GameObject radar;
        private GameObject radarCamera;
        private GameObject radarDotResource;
        private Vector3 angles;

        public RadarView()
        {
            GameObject radarResource = Resources.Load<GameObject>("Radar");
            radarDotResource = Resources.Load<GameObject>("RadarDot");
            radar = Object.Instantiate(radarResource, Object.FindObjectOfType<Canvas>().transform);
            radarResource = Resources.Load<GameObject>("RadarCamera");
            radarCamera = Object.Instantiate(radarResource);
            angles = new Vector3(90.0f, 0.0f, 0.0f);
        }

        public GameObject CreateImage(IController controller)
        {
            GameObject radarDot = Object.Instantiate(radarDotResource, radar.transform);
            radarDot.SetActive(false);
            return radarDot;
        }

        public void UpdateCamera(GameVector playerPosition, float playerYAngle)
        {
            radarCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y + 8, playerPosition.z);
            angles.y = playerYAngle;
            radarCamera.transform.eulerAngles = angles;
        }
    }
}
