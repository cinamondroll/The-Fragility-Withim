
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Color color;
    public GameObject ChosedCard;
    public Transform ChosedCardPosition;
    private BoxCollider2D collid;
    private bool Onclick=false;
    private GameObject gameManager;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
         Vector3 startPos = transform.position;
        transform.position=new Vector3(startPos.x, 0.15f, startPos.z);
    }
    void Start()
    {
        collid=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // GameObject.Find("Card").SetActive(false);
        Onclick=true;
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManagerChat>().HideUnless(this.gameObject.name);
        StartCoroutine(MoveCenter());
;        }
    }

    void FixedUpdate()
    {
       
    }

    void OnMouseEnter()
    {   
        if (!Onclick)
        {
        Renderer renderer = GetComponent<Renderer>();
        color = renderer.material.color;
        renderer.material.color = Color.black;
        Vector3 startPos = transform.position;
        Vector3 TargetPosition = new Vector3(startPos.x, 0.65f, startPos.z);
        StartCoroutine(MoveUp(startPos, TargetPosition));
        collid.size = new Vector2(1f, 1.5f);      
        }
    }
    void OnMouseExit()
    {   
        if (!Onclick)
        {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = color;
        Vector3 startPos = transform.position;
        Vector3 TargetPosition = new Vector3(startPos.x, 0.15f, startPos.z);
        StartCoroutine(MoveDown(startPos, TargetPosition)); 
        collid.size = new Vector2(1f, 1f);
        }
        
    }



    IEnumerator MoveUp(Vector3 Start, Vector3 Target){
        Vector3 startPos = transform.position;
        Vector3 TargetPosition = new Vector3(startPos.x, 0.65f, startPos.z);
        float t=0;
        while (t<0.1f)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, TargetPosition, t/0.1f);
            yield return null;
        }
        transform.position = TargetPosition;
    }
     IEnumerator MoveDown(Vector3 Start, Vector3 Target){
        Vector3 startPos = transform.position;
        Vector3 TargetPosition = new Vector3(startPos.x, 0.15f, startPos.z);
        float t=0;
        while (t<0.1f)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, TargetPosition, t/0.1f);
            yield return null;
        }
        transform.position = TargetPosition;
    }

    IEnumerator MoveCenter(){
        Vector3 startPos = transform.position;
        float t=0;
        while (t<0.1f)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, new Vector3(0, 2, 0), t/0.1f);
            yield return null;
        }
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
