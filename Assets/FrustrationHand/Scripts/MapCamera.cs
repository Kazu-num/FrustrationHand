using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MapCamera : MonoBehaviour
{
    [SerializeField]
    MazeObject target;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        StateManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        StateManager.Instance.OnStateChanged -= OnStateChanged;
    }

    void OnStateChanged(State state)
    {
        if (state == State.Play)
        {
            Adjust();
        }
    }

    void Adjust()
    {
        var maxX = target.Width;
        var maxZ = target.Height;
        var max = Mathf.Max(maxX, maxZ);

        var fov = cam.fieldOfView;
        var far = max * 0.5f / Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f);

        cam.transform.position = new Vector3(maxX * 0.5f - 0.5f, far, maxZ * 0.5f - 0.5f);
        cam.farClipPlane = far;
        cam.nearClipPlane = far - 1;
    }
}
