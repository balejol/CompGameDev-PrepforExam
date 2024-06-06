using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition = new Vector3(0f, 3f, -8f);

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        if (offsetPositionSpace == Space.Self)
            transform.position = target.TransformPoint(offsetPosition);
        else
            transform.position = target.position + offsetPosition;
    }

    private void UpdateRotation()
    {
        if (lookAt)
            transform.LookAt(target);
        else
            transform.rotation = target.rotation;
    }
}
