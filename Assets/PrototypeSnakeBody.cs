using UnityEngine;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrototypeSnakeBody : MonoBehaviour
{
    Transform lastLink;

    [SerializeField]
    float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastLink = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBody()
    {
        Debug.Log("Works");

        GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        newSphere.transform.SetPositionAndRotation(lastLink.position, lastLink.rotation);
        newSphere.transform.position -= lastLink.forward * distance;

        lastLink = newSphere.transform;

        newSphere.AddComponent<BodyLink>();
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

public class BodyLink : MonoBehaviour
{
    BodyLink prevLink;

    public void Initialize(BodyLink prevLink)
    {
        this.prevLink = prevLink;
    }

    private void Update()
    {
        
    }
}
