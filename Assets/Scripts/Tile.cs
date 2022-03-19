using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] float Seconds = 1.5f;
    [SerializeField] private Color _baseColor, _offsetColor, _selectedColor,_illegalColor;
    [SerializeField] private Color[] colors;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject _highlight;
    public int currentColor = 0;
    GridManager grid;
    private SpriteRenderer _renderer;
    public bool fluffy;
    private bool selected, offset;
    bool justSpawned = false;

    private void Awake()
    {
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
        
        _highlight.SetActive(true);

    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
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
        //Debug.Log("sssspawn" + gameObject.name + "Fluffy: " + fluffy);
        justSpawned = true;

    }

    public void SpawnHouse(House _housePrefab, int color)
    {
        currentColor = color;
/*        if (gameObject.transform.childCount == 2)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
        }*/

        var fluffyObject = Instantiate(_housePrefab, gameObject.transform.position, Quaternion.identity);
        fluffyObject.GetComponent<SpriteRenderer>().sprite = sprites[color];
        fluffyObject.transform.SetParent(gameObject.transform);
        //Debug.Log("sssspawn" + gameObject.name + "Fluffy: " + fluffy);
        //justSpawned = true;

    }
    
    public int deSpawnFluffy()
    {
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

    internal void CompleteHouse()
    {
        Debug.Log("One more down!");
    }

    private void OnMouseUp()
    {
        selected = true;

        /*Debug.Log(name.Substring(7));
            Debug.Log(name.Substring(5,1));*/
        int i = Int32.Parse(name.Substring(5, 1));
        int j = Int32.Parse(name.Substring(7));

        grid.SelectFluffy(i, j);
        if (!justSpawned)
        {
            _renderer.color = _selectedColor;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") )
        {
            if (selected)
            {
                selected = false;
                _renderer.color = offset ? _offsetColor : _baseColor;
            }
            justSpawned = false;
            
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
}
