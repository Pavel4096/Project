using UnityEngine;
using UnityEditor;

namespace Project
{
    public sealed class SimpleWindow : EditorWindow
    {
        private GameObject obj;
        private float radius = 5.0f;
        private float stepU =0.5f;
        private float stepV = 0.5f;
        private bool randomizeRotation = false;

        private bool toggle = false;
        private bool showError = false;

        [MenuItem("Window/Simple Window %q")]
        private static void Init()
        {
            SimpleWindow window = GetWindow<SimpleWindow>(false, "Simple Window", true);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Добавить объекты:");
            obj = (GameObject)EditorGUILayout.ObjectField("Объект: ", obj, typeof(GameObject), true);
            if(obj != null)
                showError = false;
            radius = EditorGUILayout.Slider("Радиус: ", radius, 1.0f, 10.0f);
            stepU = EditorGUILayout.Slider("Шаг U: ", stepU, 0.1f, 10.0f);
            stepV = EditorGUILayout.Slider("Шаг V: ", stepV, 0.1f, 10.0f);
            toggle = EditorGUILayout.BeginToggleGroup("А также:", toggle);
            randomizeRotation = EditorGUILayout.ToggleLeft("Случайный поворт объектов", randomizeRotation);
            EditorGUILayout.EndToggleGroup();

            if(GUILayout.Button("Добавить."))
            {
                if(obj == null)
                {
                    showError = true;
                }
                else
                {
                    GameObject root = new GameObject("New Object");
                    GameObject currentObject;
                    Quaternion rotation = Quaternion.identity;
                    for(float u = 0; u < 2*Mathf.PI; u += stepU)
                        for(float v = -Mathf.PI; v < Mathf.PI; v += stepV)
                        {
                            if(randomizeRotation)
                                rotation = Random.rotation;
                            currentObject = Object.Instantiate(obj, new Vector3(Mathf.Sin(v)*Mathf.Sin(u)*radius,Mathf.Sin(v)*Mathf.Cos(u)*radius,Mathf.Cos(v)*radius), rotation, root.transform);
                        }
                    Undo.RegisterCreatedObjectUndo(root, "Create objects");
                }
            }

            if(showError)
                EditorGUILayout.HelpBox("Объект не выбран.", MessageType.Error);
        }
    }
}
