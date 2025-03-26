using UnityEngine;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrototypeSnakeBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBody()
    {
        Debug.Log("Works");
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PrototypeSnakeBody))]
    // ^ This is the script we are making a custom editor for.
    public class CustomButton : Editor
    {
        public override void OnInspectorGUI()
        {
            //Called whenever the inspector is drawn for this object.
            DrawDefaultInspector();
            //This draws the default screen.

            PrototypeSnakeBody script = (PrototypeSnakeBody)target;

            if (GUILayout.Button("Add body section"))
            {
                script.SpawnBody();
            }
        }
    }
#endif
}
