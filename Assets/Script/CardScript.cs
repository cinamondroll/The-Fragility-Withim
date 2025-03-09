using Unity.VisualScripting;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Color color;
    public GameObject ChosedCard;
    public Transform ChosedCardPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Card").SetActive(false);
            ChosedCard.transform.position = Vector3.zero;
            Instantiate(ChosedCard, ChosedCardPosition)
;        }
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
