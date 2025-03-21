using System.Threading.Tasks;
using UnityEngine;

public class Shuffle : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isAvail=true;
    public GameObject gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (isAvail)
        {
        Color color = Color.white;
        GetComponent<SpriteRenderer>().color = color;    
        }
    }
    void OnMouseExit()
    {  
        Color color = new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
        GetComponent<SpriteRenderer>().color = color;    
        
    }

    async Task OnMouseDown()
    {
        if (isAvail)
        {isAvail=false;
        await gameManager.GetComponent<DialogManager>().Reshuffle();
        isAvail=true;
        }
    }
}
