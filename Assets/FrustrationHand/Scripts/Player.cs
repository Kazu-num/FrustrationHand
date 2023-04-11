using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    MazeObject mazeObject;

    [SerializeField]
    Collider goalCollider;

    [SerializeField]
    Material activeMat;

    [SerializeField]
    Material inActiveMat;

    [SerializeField]
    float power = 20;

    [SerializeField]
    float keyWeight = 2;

    [SerializeField]
    ControlMode controlMode = ControlMode.Hand;

    public enum ControlMode
    {
        Hand,
        Key,
    }

    Rigidbody rb;
    MeshRenderer meshRenderer;
    bool isActive;
    Vector3 targetVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        meshRenderer.material = inActiveMat;
        isActive = false;
    }

    void FixedUpdate()
    {
        if (StateManager.Instance.State != State.Play)
        {
            return;
        }

        rb.AddForce((targetVelocity - rb.velocity) * power, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == mazeObject.gameObject)
        {
            StateManager.Instance.State = State.GameOver;
            targetVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;

            //var hitPos = collision.contacts[0].point;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == goalCollider)
        {
            StateManager.Instance.State = State.Clear;
            targetVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (StateManager.Instance.State != State.Play)
        {
            return;
        }

        switch(controlMode)
        {
            case ControlMode.Hand:
                HandControl();
                break;
            default:
                KeyControl();
                break;
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

    void HandControl()
    {

    }

    void KeyControl()
    {
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow))
        {
            isActive = true;
            var keyVector = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                keyVector += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                keyVector += Vector3.back;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                keyVector += Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                keyVector += Vector3.left;
            }
            targetVelocity = keyVector.normalized * keyWeight;
        }
        else
        {
            isActive = false;
            targetVelocity = Vector3.zero;
        }
    }
}
