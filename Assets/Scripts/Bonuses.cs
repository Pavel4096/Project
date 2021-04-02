using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public List<Vector3> bonusLocations = new List<Vector3>();
    public List<Vector3>[] bonuses = new List<Vector3>[2];
    public List<Vector3> itemBonuses;
    public List<Vector3> damagerBonuses;

    void OnDrawGizmosSelected()
    {
        foreach(var currentItems in bonuses)
            foreach(var location in currentItems)
            {
                Gizmos.DrawIcon(location + Vector3.up*0.8f, "icon");
                Gizmos.DrawSphere(location, 0.25f);
            }
    }
}
