using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyHandler : MonoBehaviour
{
    Transform prevLink;
    Vector3 prevPos;
    float distance;

    Queue<Vector3> prevLinkPositions;
    Queue<Quaternion> prevLinkRotations;

    public void Initialize(Transform _prevLink, Vector3 _prevPos, float _distance)
    {
        prevLink = _prevLink;
        prevPos = _prevPos;
        distance = _distance;
        prevLinkPositions = new Queue<Vector3>();
        prevLinkRotations = new Queue<Quaternion>();
    }

    private void FixedUpdate()
    {
        UpdateTransform();
    }

    void UpdateTransform()
    {
        if (prevPos == prevLink.position) return;

        prevLinkPositions.Enqueue(prevLink.position);
        prevLinkRotations.Enqueue(prevLink.rotation);
        prevPos = prevLink.position;

        if (prevLinkPositions.Count < distance) return;

        transform.position = prevLinkPositions.Dequeue();
        transform.rotation = prevLinkRotations.Dequeue();
    }
}
