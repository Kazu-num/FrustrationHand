using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject panelPlay;
    [SerializeField]
    GameObject panelClear;
    [SerializeField]
    GameObject panelGameOver;

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
        OnStateChanged(StateManager.Instance.State);
    }

    void OnStateChanged(State state)
    {
        switch (state)
        {
            case State.Play:
                OnPlay();
                break;

            case State.Clear:
                OnClear();
                break;

            case State.GameOver:
                OnGameOver();
                break;

            default:
                OnDefault();
                break;
        }
    }

    void OnPlay()
    {
        panelClear.SetActive(false);
        panelGameOver.SetActive(false);
    }

    void OnClear()
    {
        panelClear.SetActive(true);
        panelGameOver.SetActive(false);
    }

    void OnGameOver()
    {
        panelClear.SetActive(false);
        panelGameOver.SetActive(true);
    }

    void OnDefault()
    {
        panelClear.SetActive(false);
        panelGameOver.SetActive(false);
    }

    public void NewGame()
    {
        StateManager.Instance.State = State.NewGame;
    }

    public void Restart()
    {
        StateManager.Instance.State = State.Restart;
    }
}
