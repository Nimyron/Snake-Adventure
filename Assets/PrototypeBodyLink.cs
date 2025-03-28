using System.Collections.Generic;
using UnityEngine;

public class PrototypeBodyLink : MonoBehaviour
{
    Transform prevLink;
    Transform speedRef;
    int distance = 20;

    Queue<Vector3> prevLinkPositions;

    public void Initialize(Transform prevLink, Transform speedRef)
    {
        this.prevLink = prevLink;
        this.speedRef = speedRef;
        prevLinkPositions = new Queue<Vector3>();
    }

    private void FixedUpdate()
    {
        prevLinkPositions.Enqueue(prevLink.position - prevLink.forward * distance);

        if (prevLinkPositions.Count < distance) return;

        transform.rotation = Quaternion.LookRotation(prevLink.position - transform.position);
        transform.position = prevLinkPositions.Dequeue();
    }
}
