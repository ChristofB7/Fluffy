using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    GridManager grid;
    [SerializeField] int blue1i;
    [SerializeField] int blue1j;
    [SerializeField] int blue2i;
    [SerializeField] int blue2j;
    [SerializeField] int white1i;
    [SerializeField] int white1j;
    [SerializeField] int white2i;
    [SerializeField] int white2j;
    [SerializeField] int black1i;
    [SerializeField] int black1j;
    [SerializeField] int black2i;
    [SerializeField] int black2j;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridManager>();

        if (blue1i != -1 && blue1j != -1)
        {
            grid.SpawnHouse(blue1i, blue1j, 1);
        }
        if (blue2i != -1 && blue2j != -1)
        {
            grid.SpawnHouse(blue2i, blue2j, 1);
        }
        if (white1i != -1 && white1j != -1)
        {
            grid.SpawnHouse(white1i, white1j, 2);
        }
        if (white2i != -1 && white2j != -1)
        {
            grid.SpawnHouse(white2i, white2j, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
