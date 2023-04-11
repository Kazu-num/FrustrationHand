using UnityEngine;

public class Goal : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 1, 1);
    }

    public void SetPosition(Vector3 position)
    {
        position.x += 0.25f;
        transform.position = position;
    }
}
