using UnityEditor;
using UnityEngine;

public class SnakeHeadHandler : MonoBehaviour
{
    Transform lastLink;

    [SerializeField]
    [Tooltip("Distance between each part of the body. Arbitrary unit.")]
    float distance;

    [SerializeField]
    [Tooltip("Movement speed of the snake.")]
    float moveSpeed;

    [SerializeField]
    [Tooltip("Turning speed of the snake.")]
    float turnSpeed;

    [SerializeField]
    SnakeBodyHandler bodyPrefab;

    bool isMoving = false;
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    void Awake()
    {
        lastLink = transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isMoving) return;

        transform.rotation *= Quaternion.AngleAxis(InputManager.Instance.inputValues.x * turnSpeed, transform.up);
        transform.position += transform.forward * moveSpeed;
    }

    void SpawnBody()
    {
        SnakeBodyHandler newBodyLink = Instantiate(bodyPrefab);

        newBodyLink.transform.SetPositionAndRotation(lastLink.position, lastLink.rotation);

        newBodyLink.Initialize(lastLink, lastLink.position, distance);

        lastLink = newBodyLink.transform;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SnakeHeadHandler))]
    // ^ This is the script we are making a custom editor for.
    public class CustomButton : Editor
    {
        public override void OnInspectorGUI()
        {
            //Called whenever the inspector is drawn for this object.
            DrawDefaultInspector();
            //This draws the default screen.

            SnakeHeadHandler script = (SnakeHeadHandler)target;

            if (GUILayout.Button("Add body section"))
            {
                script.SpawnBody();
            }

            if(GUILayout.Button("Start/Stop movement"))
            {
                script.IsMoving = script.IsMoving ? false : true ;
            }
        }
    }
#endif
}
