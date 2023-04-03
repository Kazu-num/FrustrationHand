using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class App : MonoBehaviour
{
    [SerializeField]
    bool debugRun = false;

    [SerializeField]
    Vector2Int debugRunVector;

    [SerializeField]
    Player player;

    [SerializeField]
    MazeObject mazeObject;

    [SerializeField]
    Goal goal;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (debugRun)
        {
            StartGame();
            debugRun = false;
        }
    }

    void StartGame()
    {
        mazeObject.CreateNewMaze(debugRunVector.x, debugRunVector.y);
        mazeObject.transform.localPosition = Vector3.zero;
        player.transform.position = mazeObject.StartPosition;
        goal.SetPosition (mazeObject.GoalPosition);
    }
}
