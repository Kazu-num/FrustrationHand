using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    MazeObject mazeObject;

    [SerializeField]
    Collider goalCollider;

    [SerializeField]
    float mouseSensitive = -1;

    [SerializeField]
    Material activeMat;

    [SerializeField]
    Material inActiveMat;

    Rigidbody rigidbody;
    MeshRenderer meshRenderer;
    bool isActive;
    Vector2 start;
    Vector3 dst;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inActiveMat;
        isActive = false;
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            rigidbody.velocity = dst * mouseSensitive / Time.deltaTime;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == mazeObject.gameObject)
        {
            Debug.LogWarning("OnCollisionEnter: wall");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other == goalCollider)
        {
            Debug.LogWarning("OnColliderEnter: goal");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            isActive = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isActive = false;
        }

        if (Input.GetMouseButton(0))
        {
            dst.x = (start.x - Input.mousePosition.x) / Screen.width;
            dst.y = (start.y - Input.mousePosition.y) / Screen.height;
            dst.z = 0;
        }

        if (isActive)
        {
            meshRenderer.material = activeMat;
        }
        else
        {
            meshRenderer.material = inActiveMat;
        }
    }
}
