using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MazeObject : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Vector3 StartPosition
    {
        get
        {
            return transform.TransformPoint(StartLocalPosition);
        }
    }

    public Vector3 StartLocalPosition
    {
        get
        {
            return new Vector3(1, 1, 0);
        }
    }

    public Vector3 GoalPosition
    {
        get
        {
            return transform.TransformPoint(GoalLocalPosition);
        }
    }

    public Vector3 GoalLocalPosition
    {
        get
        {
            return new Vector3(Width - 1, Height - 2, transform.localPosition.z);
        }
    }

    MazeCreateor_Dig mazeCreateor;
    Mesh mesh;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    private void Awake()
    {
        name = "MazeObject";
        mesh = new Mesh();
        mesh.name = name;
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void CreateNewMaze(int width, int height)
    {
        if (mazeCreateor == null || mazeCreateor.Width != width || mazeCreateor.Height != height)
        {
            try
            {
                mazeCreateor = new MazeCreateor_Dig(width, height);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Debug.LogWarning(e.Message);
                return;
            }

            Width = mazeCreateor.Width;
            Height = mazeCreateor.Height;
        }

        var mazeArray = mazeCreateor.CreateMaze();

        GenerateMazeObject(mazeArray);
    }

    void GenerateMazeObject(int[,] mazeArray)
    {
        var width = mazeArray.GetLength(0);
        var height = mazeArray.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (IsEndCell(x, y))
                {
                    // 迷路終了位置は開ける
                    continue;
                }

                if (mazeArray[x, y] == Const.Wall)
                {
                    // 壁位置にCubeを一時的に生成
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = new Vector3(x, y, 0);
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = Vector3.one;
                }
            }
        }

        // 生成されたCubeを1つのMeshにする
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combines = new CombineInstance[meshFilters.Length - 1];
        for (int i = 0; i < combines.Length; i++)
        {
            // meshFilters[0] == meshFilterのため1つとばす
            combines[i].mesh = meshFilters[i + 1].sharedMesh;
            combines[i].transform = meshFilters[i + 1].transform.localToWorldMatrix;
        }
        mesh.CombineMeshes(combines);
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        // Cubeは破棄
        foreach (var meshFilter in meshFilters)
        {
            if (meshFilter != this.meshFilter)
            {
                Destroy(meshFilter.gameObject);
            }
        }
    }

    bool IsStartOrEndCell(int x, int y)
    {
        return IsStrartCell(x, y) || IsEndCell(x, y);
    }

    bool IsStrartCell(int x, int y)
    {
        return x == 0 && y == 1;
    }

    bool IsEndCell(int x, int y)
    {
        return x == Width - 1 && y == Height - 2;
    }
}
