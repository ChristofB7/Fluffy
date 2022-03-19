using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private GameObject _highlight;
    private SpriteRenderer _renderer;

    public void Init(bool isOffset)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Entered"+gameObject.name);
        _highlight.SetActive(true);

    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void SpawnFluffy()
    {

    }
}
