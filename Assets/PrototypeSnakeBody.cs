using UnityEngine;
using Unity.VisualScripting;
using System.Collections;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrototypeSnakeBody : MonoBehaviour
{
    Transform lastLink;

    [SerializeField]
    float distance;

#if UNITY_EDITOR
    [SerializeField]
    bool autoSpawn = false;
    public bool AutoSpawn { set => autoSpawn = value; get => autoSpawn; }

    [SerializeField]
    float autoSpawnDelay;
#endif

        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastLink = this.transform;
        StartCoroutine(AutoSpawnBody());
    }

    void SpawnBody()
    {
        GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        newSphere.transform.SetPositionAndRotation(lastLink.position, lastLink.rotation);

        PrototypeBodyLink proto = newSphere.AddComponent<PrototypeBodyLink>();
        proto.Initialize(lastLink, lastLink.position);

        lastLink = newSphere.transform;
    }

    IEnumerator AutoSpawnBody()
    {
        while (true)
        {
            if (autoSpawn)
            {
                SpawnBody();
            }
            yield return new WaitForSeconds(autoSpawnDelay);
        }
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

            if(GUILayout.Button("Auto spawn body on/off"))
            {
                script.AutoSpawn = script.AutoSpawn ? false : true;
            }
        }
    }
#endif
}