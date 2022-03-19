using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Fluffy _fluffyPrefab;

    [SerializeField] private Transform _cam;

    private Tile[,] tiles;
    private int[,] array2D;

    private GridManager grid;

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
        Debug.Log(tiles[i, j]);
        return tiles[i, j];
    }

    public void SpawnFluffy(int i, int j)
    {
        

        var fluffy = Instantiate(_fluffyPrefab, GetTileAtPosition(i,j).transform.position, Quaternion.identity);
        fluffy.transform.SetParent(GetTileAtPosition(i, j).gameObject.transform);

        array2D[i, j] = 1;
        ConsoleLog();


    }

    public void ConsoleLog()
    {
        string arr = "";
        Debug.Log(array2D.GetLength(0));
        Debug.Log(array2D.GetLength(1));
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
