using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Fluffy _fluffyPrefab;
    [SerializeField] private House _housePrefab;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] TextMeshProUGUI tmp1, tmp2;

    bool selected, lastFluffy;
    public int selectedi, selectedj;
    public int numberToWin;
    private int completed;
    [SerializeField] private Transform _cam;

    private Tile[,] tiles;
    private int[,] array2D;

    private GridManager grid;

    public int lastMovedi=-1, lastMovedj=-1;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
        array2D = new int[_width, _height];
        tiles = new Tile[_width, _height];
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int i = 0; i < _width; i++)
        {
            for(int j=0; j< _height; j++)
            {
                //Spawn Tiles Here
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";

                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                spawnedTile.Init(isOffset);
                tiles[i, j] = spawnedTile;

                //Debug.Log(tiles[i, j]);

                //Create Game Logic here
                array2D[i, j] = 0;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10f);

        ConsoleLog();
    }

    public Tile GetTileAtPosition(int i, int j)
    {
        //Debug.Log(tiles[i, j]);
        return tiles[i, j];
    }

    public void SpawnFluffy(int i, int j, int color)
    {
        GetTileAtPosition(i, j).SpawnFluffy(_fluffyPrefab, color);
        array2D[i, j] = color;

        ConsoleLog();
    }

    public void SpawnHouse(int i, int j, int color)
    {
        GetTileAtPosition(i, j).SpawnHouse(_housePrefab, color);
        array2D[i, j] = 4;

        ConsoleLog();
    }

    public void SelectFluffy(int i, int j)
    {
        if (selected && IsFluffy(selectedi,selectedj) && (IsHome(i,j) || IsEmpty(i,j)))
        {
            selected = false;
            //selectedFluffy = false;
            if (IsValid(i, j) && !LastMoved(i, j))
            {
                if (IsHome(i, j))
                {
                    if(GetTileAtPosition(i,j).currentColor == GetTileAtPosition(selectedi, selectedj).currentColor)
                    {
                        lastMovedi = i; lastMovedj = j;
                        completed++;
                        GetTileAtPosition(i, j).CompleteHouse();
                        if(completed >= numberToWin)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        array2D[selectedi, selectedj] = 0;
                        GetTileAtPosition(selectedi, selectedj).deSpawnFluffy();
                    }
                }
                else
                {
                    MoveFluffy(i, j);
                    lastMovedi = i; lastMovedj = j;
                }
            }
            else
            {
                GetTileAtPosition(i, j).IllegalMove();
            }

            ConsoleLog();
        }
        else
        {
            
            tmp1.text = i.ToString();
            tmp2.text = j.ToString();
            selectedi = i;
            selectedj = j;
            selected = true;
            //selectedFluffy = array2D[i,j]==1;
        }
    }


    private bool LastMoved(int i, int j)
    {
        if(selectedi==lastMovedi && selectedj == lastMovedj)
        {
            return true;
        }
        return false;
    }
    private bool IsValid(int i, int j)
    {
        if(selectedi!=i && selectedj != j)
        {
            return true;
        }


        return false;
    }

    private bool IsFluffy(int i, int j)
    {
        if(array2D[i,j]==0 || array2D[i, j] == 4)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsHome(int i, int j)
    {
        return array2D[i, j] == 4;
    }

    private bool IsEmpty(int i, int j)
    {
        return array2D[i, j] == 0;
    }

    private void MoveFluffy(int i, int j)
    {
        //TODO COLOR
        tmp1.text = ""; tmp2.text = "";
        array2D[selectedi, selectedj] = 0;
        array2D[i, j] = 1;

        int color = GetTileAtPosition(selectedi, selectedj).deSpawnFluffy();
        GetTileAtPosition(i, j).SpawnFluffy(_fluffyPrefab, color);
    }

    public void ConsoleLog()
    {
        string arr = "";
        /*Debug.Log(array2D.GetLength(0));
        Debug.Log(array2D.GetLength(1));*/
        for(int i = array2D.GetLength(1)-1; i >=0 ; i--)
        {
            string line = "";
            for(int j = 0; j < array2D.GetLength(0); j++)
            {
                line = line + array2D[j, i].ToString() + ", ";
            }
            arr = arr + line + "\n";
        }
        Debug.Log(arr);
    }



}
