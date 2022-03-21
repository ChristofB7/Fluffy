using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canClick;
    private float Seconds = 0.85f;
    [SerializeField] private Color _baseColor, _offsetColor, _selectedColor,_illegalColor;
    [SerializeField] private Color[] colors;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject higles;
    public int currentColor = 0;
    GridManager grid;
    private SpriteRenderer _renderer;
    public bool fluffy;
    private bool offset;

    private bool mouseOver;
    public Sprite closedHouseSprite;

    //public bool justSpawned = false;
    //bool reset;

    private void Awake()
    {
        canClick = true;
        //reset = false;
        grid = FindObjectOfType<GridManager>();
    }

    public void Init(bool isOffset)
    {
        offset = isOffset;
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        fluffy = false;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        if (canClick)
        {

            higles.SetActive(true);
        }


    }

    private void OnMouseExit()
    {
        mouseOver = false;
        if (canClick)
        {

            higles.SetActive(false);
        }

    }

    public void SpawnFluffy(Fluffy _fluffyPrefab, int color)
    {

        currentColor = color;
        if(gameObject.transform.childCount==2)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
            
        }

        fluffy = true;
        var fluffyObject = Instantiate(_fluffyPrefab, gameObject.transform.position, Quaternion.identity);

        fluffyObject.GetComponent<SpriteRenderer>().color = colors[color];
        fluffyObject.transform.SetParent(gameObject.transform);
        //fluffyObject.SetHighlight();
        //Debug.Log("sssspawn" + gameObject.name + "Fluffy: " + fluffy);
        //justSpawned = true;

    }

    public void SpawnHouse(House _housePrefab, int color)
    {
        currentColor = color;
/*        if (gameObject.transform.childCount == 2)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
        }*/

        var fluffyObject = Instantiate(_housePrefab, gameObject.transform.position + new Vector3(0f,0.35f,0f), Quaternion.identity);
        fluffyObject.GetComponent<SpriteRenderer>().sprite = sprites[color];
        fluffyObject.transform.SetParent(gameObject.transform);
        //Debug.Log("sssspawn" + gameObject.name + "Fluffy: " + fluffy);
        //justSpawned = true;

    }
    
    public int deSpawnFluffy()
    {
        //gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().SetHighlight();
        SetMouseToTrue();
        grid.spawn = true;
        //Debug.Log("DeSpawn" + gameObject.name + "Fluffy: " + fluffy);
        if (fluffy)
        {
            fluffy = false;
            //Debug.Log(gameObject.transform.GetChild(1));
            Destroy(gameObject.transform.GetChild(1).gameObject);
        }
        int temp = currentColor;
        currentColor = 0;
        return temp;

    }

    public int deSpawnFluffyFromHome()
    {
        SetMouseToTrue();
        grid.spawn = true;
        //Debug.Log("DeSpawn" + gameObject.name + "Fluffy: " + fluffy);
        if (fluffy)
        {
            fluffy = false;
            //Debug.Log(gameObject.transform.GetChild(1));
            Destroy(gameObject.transform.GetChild(1).gameObject);
        }
        int temp = currentColor;
        currentColor = 0;
        return temp;

    }

    public int getColor()
    {
        return currentColor;
    }

    internal void CompleteHouse()
    {
        Debug.Log("One more down!");
        if (gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = closedHouseSprite;
        }

    }

    private void OnMouseUp()
    {
        if (canClick)
        {

            int i = Int32.Parse(name.Substring(5, 1));
            int j = Int32.Parse(name.Substring(7));

            grid.SelectFluffy(i, j);
        }

        

    }


    public void IllegalMove()
    {
        _renderer.color = _illegalColor;
        StartCoroutine(WaitAndResetColor());
    }

    IEnumerator WaitAndResetColor()
    {
        yield return new WaitForSeconds(Seconds);
        _renderer.color = offset ? _offsetColor : _baseColor;
    }

    internal void WrongAnswer()
    {
        _renderer.color = _illegalColor;
    }

    internal void selectedColor()
    {
        _renderer.color = _selectedColor;
    }

    internal void resetColor()
    {
        _renderer.color = offset ? _offsetColor : _baseColor;
        //Debug.Log("resetting" + name);
    }

    public void Animate(string direction)
    {
        switch (direction)
        {
            case "up":
                if (fluffy)
                {  
                    gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().AnimateUp();
                }
                break;
            case "down":
                if (fluffy)
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().AnimateDown();
                }
                break;
            case "left":
                if (fluffy)
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().AnimateLeft();
                }
                break;
            case "right":
                if (fluffy)
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().AnimateRight();
                }
                break;
            default:
                Debug.Log("Incorrect intelligence level.");
                break;
        }
    }

    public void SetMouseToFalse()
    {
        grid.canClick = false;
        
    }

    public void SetMouseToTrue()
    {
        grid.canClick = true;
        
    }

    private void Update()
    {
        canClick = grid.canClick;
        if (!canClick)
        {
            //reset = true;
            if (higles.gameObject.activeSelf&&!mouseOver)
            {
                higles.SetActive(false);
            }
           
            
        }
        if (!higles.gameObject.activeSelf && mouseOver)
        {
            higles.SetActive(true);
        }
        //if(canClick&&Mouse && Input.mousePosition)
    }

    public void SetHighlightActive()
    {
        gameObject.transform.GetChild(1).gameObject.GetComponent<Fluffy>().SetHighlight();
    }
}
