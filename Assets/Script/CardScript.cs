
using System.Collections;
using System.Threading.Tasks;
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
    }

    void FixedUpdate()
    {
       
    }

    void OnMouseEnter()
    {   
        if (!Onclick)
        {
            Renderer renderer = GetComponent<Renderer>();
            color = Color.white;
            renderer.material.color = Color.white;
            Vector3 startPos = transform.position;
            Vector3 TargetPosition = new Vector3(startPos.x, 0.65f, startPos.z);
            StartCoroutine(MoveUp(startPos, TargetPosition));
            collid.size = new Vector2(2.5f, 5f);      
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
            collid.size = new Vector2(2.5f, 4f);
        }    
    }

    async void OnMouseDown()
    {
          if(Onclick == false)
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.color = Color.white;
                Onclick=true;
                gameManager = GameObject.Find("GameManager");
                StartCoroutine(gameManager.GetComponent<GameManagerChat>().HideCard(this.gameObject.name));
                StartCoroutine(MoveCenter(ChosedCardPosition.position));
                await Task.Delay(500);
            }
    }




    IEnumerator MoveUp(Vector3 Start, Vector3 Target){
        Vector3 startPos = Start;
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
        Vector3 startPos = Start;
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

    IEnumerator MoveCenter(Vector3 Target){
        Vector3 startPos = transform.position;
        float t=0;
        while (t<0.3f)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, Target, t/0.3f);
            yield return null;
        }
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;
    }
}
