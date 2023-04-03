using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    Collider playerCollider;

    void Start()
    {
        transform.localScale = new Vector3(0.5f, 1, 0.5f);
    }

    public void SetPosition(Vector3 position)
    {
        position.x += 0.25f;
        position.z += 0.25f;
        transform.position = position;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
