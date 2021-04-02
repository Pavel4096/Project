using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bonuses))]
public class BonusesEditor : Editor
{
    private Bonuses targetObj;
    private int typeId = 0;
    private string[] typeStrings = new string[] {"Item", "Damager"};

    public override void OnInspectorGUI()
    {
        typeId = GUILayout.Toolbar(typeId, typeStrings);
        DrawDefaultInspector();
    }

    void OnEnable()
    {
        targetObj = (Bonuses)target;
        if(targetObj.bonuses[0] == null)
        {
            targetObj.bonuses[0] = targetObj.itemBonuses;
            targetObj.bonuses[1] = targetObj.damagerBonuses;
        }
    }

    void OnSceneGUI()
    {
        if(Event.current.type == EventType.MouseDown && (Event.current.button == 0) && Event.current.clickCount == 2)
        {
            RaycastHit hit;
            Camera camera = SceneView.currentDrawingSceneView.camera;
            Vector2 position = Event.current.mousePosition;

            position.x = position.x*Screen.dpi/96;
            position.y = position.y*Screen.dpi/96;
            Debug.Log(position + " / " + camera.pixelWidth + "x" + camera.pixelHeight);
            Debug.Log(Event.current.mousePosition + " / " + camera.pixelWidth + "x" + camera.pixelHeight);

            if(Physics.Raycast(camera.ScreenPointToRay(new Vector3(position.x, camera.pixelHeight - position.y, 0)), out hit))
            {
                bool addItem = true;
                List<Vector3> bonusLocations;

                for(int j = 0; j < 2; j++)
                {
                    bonusLocations = targetObj.bonuses[j];
                    for(int i = 0; i < bonusLocations.Count; i++)
                    {
                        if((bonusLocations[i] - hit.point).sqrMagnitude < 0.25)
                        {
                            bonusLocations.RemoveAt(i);
                            addItem = false;
                            break;
                        }
                    }
                }
                if(addItem)
                {
                    targetObj.bonuses[typeId].Add(hit.point);
                }
                EditorUtility.SetDirty(targetObj);
                Event.current.Use();
            }
        }
    }
}
