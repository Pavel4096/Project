using System;

namespace Project
{
    public struct GameVector
    {
        public float x;
        public float y;
        public float z;

        public GameVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float DistanceTo(GameVector vector)
        {
            return (float)(Math.Pow(vector.x - x, 2) + Math.Pow(vector.y - y, 2) + Math.Pow(vector.z - z, 2));
        }

        public static GameVector operator *(GameVector vector, float multiplier)
        {
            vector.x *= multiplier;
            vector.y *= multiplier;
            vector.z *= multiplier;

            return vector;
        }

        public static GameVector operator +(GameVector vector, GameVector vector2)
        {
            vector.x += vector2.x;
            vector.y += vector2.y;
            vector.z += vector2.z;

            return vector;
        }
    }
}