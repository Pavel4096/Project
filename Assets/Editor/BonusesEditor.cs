using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bonuses))]
public class BonusesEditor : Editor
{
    private Bonuses targetObj;

    void OnEnable()
    {
        targetObj = (Bonuses)target;
    }

    void OnSceneGUI()
    {
        if(Event.current.type == EventType.MouseDown && (Event.current.button == 0) && Event.current.clickCount == 2)
        {
            RaycastHit hit;
            Camera camera = SceneView.currentDrawingSceneView.camera;
            Vector2 position = Event.current.mousePosition;

            position.x = position.x/0.7954f;
            position.y = position.y/0.7885f;
            Debug.Log(position + " / " + camera.pixelWidth + "x" + camera.pixelHeight);
            Debug.Log(Event.current.mousePosition + " / " + camera.pixelWidth + "x" + camera.pixelHeight);

            if(Physics.Raycast(camera.ScreenPointToRay(new Vector3(position.x, camera.pixelHeight - position.y, 0)), out hit))
            {
                int i = 0;
                bool addItem = true;
                List<Vector3> bonusLocations = targetObj.bonusLocations;

                for(i = 0; i < bonusLocations.Count; i++)
                {
                    if((bonusLocations[i] - hit.point).sqrMagnitude < 0.25)
                    {
                        bonusLocations.RemoveAt(i);
                        addItem = false;
                        break;
                    }
                }
                if(addItem)
                    targetObj.bonusLocations.Add(hit.point);
                EditorUtility.SetDirty(targetObj);
                Event.current.Use();
            }
        }
    }
}
