using UnityEngine;


public class App : MonoBehaviour
{
    [SerializeField]
    public Vector2Int mazeScale;

    [SerializeField]
    Player player;

    [SerializeField]
    MazeObject mazeObject;

    [SerializeField]
    Goal goal;

    void OnEnable()
    {
        StateManager.Instance.OnStateChanged += OnStateChanged;
    }

    void OnDisable()
    {
        StateManager.Instance.OnStateChanged -= OnStateChanged;
    }

    void Start()
    {
        StateManager.Instance.State = State.NewGame;
    }

    void OnStateChanged(State state)
    {
        switch (state)
        {
            case State.NewGame:
                NewGame();
                break;

            case State.Restart:
                Restart();
                break;

            default:
                break;
        }
    }

    void NewGame()
    {
        mazeObject.CreateNewMaze(mazeScale.x, mazeScale.y);
        Restart();
    }

    void Restart()
    {
        mazeObject.transform.localPosition = Vector3.zero;
        player.transform.position = mazeObject.StartPosition;
        goal.SetPosition(mazeObject.GoalPosition);
        StateManager.Instance.State = State.Play;
    }
}
