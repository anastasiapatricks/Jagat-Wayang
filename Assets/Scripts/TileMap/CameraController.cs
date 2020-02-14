using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerUnit player;

    public float mouseScale;
    public float dragScale;
    public float minAngle;
    public float maxAngle;
    public float minDistance;
    public float maxDistance;

    private float zoomLevel = 0.5f;
    private Vector3 dragOrigin;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var drag = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            transform.Translate(dragScale * new Vector3(-drag.x, 0, -drag.y), Space.World);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            zoomLevel = Mathf.Clamp01(zoomLevel + mouseScale * -Input.mouseScrollDelta.y);
            Adjust(player.transform.position);
        }
    }

    public void Adjust(Vector3 playerPos)
    {
        float angle = minAngle + zoomLevel * (maxAngle - minAngle);
        float distance = minDistance + zoomLevel * (maxDistance - minDistance);

        Vector3 rotation = new Vector3(angle, 0, 0);
        Vector3 pos = playerPos - Quaternion.Euler(rotation) * Vector3.forward * distance;
        transform.LeanMove(pos, 0.5f);
        transform.LeanRotate(rotation, 0.5f);
    }
}
