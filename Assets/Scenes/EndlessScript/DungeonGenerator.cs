using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject room;

    public Vector2Int size;
    public int startPos = 0;
    public Vector2 offset;
    List<Cell> board;

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)  // Fixed: was size.y, should be size.x
        {
            for (int j = 0; j < size.y; j++)
            {
                var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();

                newRoom.UpdateRoom(board[i + j * size.x].status); // Removed unnecessary Mathf.FloorToInt

                newRoom.name += $"{i} - {j} ";
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000) // Increased limit to ensure maze completes
        {
            k++;
            board[currentCell].visited = true;
            //Check neighbours
            List<int> neighbours = CheckNeighbours(currentCell);

            if (neighbours.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbours[Random.Range(0, neighbours.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //Up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbours(int cell)
    {
        List<int> neighbours = new List<int>();

        //Check up
        if (cell - size.x >= 0 && !board[cell - size.x].visited)
        {
            neighbours.Add(cell - size.x);
        }

        //Check down
        if (cell + size.x < board.Count && !board[cell + size.x].visited)
        {
            neighbours.Add(cell + size.x);
        }

        //Check right
        if ((cell + 1) % size.x != 0 && cell + 1 < board.Count && !board[cell + 1].visited)
        {
            neighbours.Add(cell + 1);
        }

        //Check left
        if (cell % size.x != 0 && cell - 1 >= 0 && !board[cell - 1].visited)
        {
            neighbours.Add(cell - 1);
        }

        return neighbours;
    }
}