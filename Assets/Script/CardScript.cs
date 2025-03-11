
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Color color=new Color(0.4339623f, 0.4339623f, 0.4339623f, 1);
    public GameObject ChosedCard;
    public Transform ChosedCardPosition;
    private BoxCollider2D collid;
    private bool Onclick=false;
    private GameObject gameManager;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        Vector3 startPos = transform.position;
        transform.position=new Vector3(startPos.x, 1.5f, startPos.z);
        collid=GetComponent<BoxCollider2D>();
      
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnMouseEnter()
    {   
        if (!Onclick)
        {   

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            Vector3 startPos = transform.position;
            Vector3 TargetPosition = new Vector3(startPos.x, 2f, startPos.z);
            StartCoroutine(MoveUp(startPos, TargetPosition));
            collid.size = new Vector2(2.5f, 5f);      
        }
    }
    void OnMouseExit()
    {   
        if (!Onclick)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            Vector3 startPos = transform.position;
            Vector3 TargetPosition = new Vector3(startPos.x, 1.5f, startPos.z);
            StartCoroutine(MoveDown(startPos, TargetPosition)); 
            collid.size = new Vector2(2.5f, 4f);
        }    
    }

    async void OnMouseDown()
    {
          if(Onclick == false)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                Onclick=true;
                gameManager = GameObject.Find("GameManager");
                StartCoroutine(gameManager.GetComponent<GameManagerChat>().HideCard(this.gameObject.name));
                StartCoroutine(MoveCenter(ChosedCardPosition.position));
                await Task.Delay(500);
            }
    }




    IEnumerator MoveUp(Vector3 Start, Vector3 Target){
        Vector3 startPos = Start;
        Vector3 TargetPosition = new Vector3(startPos.x, 2f, startPos.z);
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
        Vector3 TargetPosition = new Vector3(startPos.x, 1.5f, startPos.z);
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

    // public IEnumerator Fade(){
        
        
    // }
}
