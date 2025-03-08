using UnityEngine;

public class CardScript : MonoBehaviour
{
    Color color;
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
        Renderer renderer = GetComponent<Renderer>();
        color = renderer.material.color;
        renderer.material.color = Color.black;
    }
    void OnMouseExit()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = color;
        
    }
}
