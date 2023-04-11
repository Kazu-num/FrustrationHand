using UnityEngine;
using UnityEngine.Events;
[DefaultExecutionOrder(-10)]
public class StateManager : MonoBehaviour
{
    public static StateManager Instance = null;

    public State State
    {
        get
        {
            return state;
        }

        set
        {
            if (value != state)
            {
                state = value;
                OnStateChanged?.Invoke(State);
            }
        }
    }

    public UnityAction<State> OnStateChanged;

    private State state = State.Unknown;

    void Awake()
    {
        Instance = this;
    }
}

public enum State
{
    Unknown,
    NewGame,
    Restart,
    Play,
    GameOver,
    Clear,
}
