using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluffy : MonoBehaviour
{

    [SerializeField] private GameObject _highlight;

    public Animator ani;

    public void AnimateUp()
    {
        ani.SetTrigger("up");
    }
    public void AnimateDown()
    {
        ani.SetTrigger("down");
    }
    public void AnimateLeft()
    {
        ani.SetTrigger("left");
    }
    public void AnimateRight()
    {
        ani.SetTrigger("right");
    }

    public void DeSpawn()
    {
        gameObject.transform.parent.GetComponent<Tile>().deSpawnFluffy();
       
    }
    public void DisableMouse()
    {
        gameObject.transform.parent.GetComponent<Tile>().SetMouseToFalse();
    }

    public void SetHighlight()
    {
        _highlight.SetActive(true);
    }
    public void RemoveHighlight()
    {
        //_highlight.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
