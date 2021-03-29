using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public List<Vector3> bonusLocations = new List<Vector3>();

    void OnDrawGizmosSelected()
    {
        foreach(var location in bonusLocations)
        {
            Gizmos.DrawIcon(location + Vector3.up*0.8f, "icon");
        }
    }
}
