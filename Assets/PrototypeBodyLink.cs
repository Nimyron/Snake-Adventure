using System.Collections.Generic;
using UnityEngine;

public class PrototypeBodyLink : MonoBehaviour
{
    Transform prevLink;
    Vector3 prevPos;
    float distance = 20f;

    Queue<Vector3> prevLinkPositions;
    Queue<Quaternion> prevLinkRotations;

    public void Initialize(Transform prevLink, Vector3 prevPos)
    {
        this.prevLink = prevLink;
        this.prevPos = prevPos;
        prevLinkPositions = new Queue<Vector3>();
        prevLinkRotations = new Queue<Quaternion>();
    }

    private void FixedUpdate()
    {
        if (prevPos == prevLink.position) return;

        prevLinkPositions.Enqueue(prevLink.position);
        prevLinkRotations.Enqueue(prevLink.rotation);
        prevPos = prevLink.position;

        if (prevLinkPositions.Count < distance) return;

        transform.rotation = prevLinkRotations.Dequeue();
        Debug.DrawRay(transform.position, transform.forward);
        transform.position = prevLinkPositions.Dequeue();
    }
}
