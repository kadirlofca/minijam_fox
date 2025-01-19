using System;
using UnityEngine;

public class CustomMouseCursor : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.nearClipPlane));
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
