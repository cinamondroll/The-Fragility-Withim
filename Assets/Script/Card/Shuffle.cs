using UnityEngine;

public class Shuffle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Color color = Color.white;
        GetComponent<SpriteRenderer>().color = color;    
    }
    void OnMouseExit()
    {
        Color color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
        GetComponent<SpriteRenderer>().color = color;    
    }

    void OnMouseDown()
    {
        GameObject gameManager=GameObject.Find("GameManager");
        gameManager.GetComponent<GameManagerChat>().Reshuffle();    
    }
}
