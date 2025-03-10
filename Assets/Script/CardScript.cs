
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
    SpriteRenderer materialRenderer;
    Material material;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
         Vector3 startPos = transform.position;
        transform.position=new Vector3(startPos.x, 0.15f, startPos.z);
    }
    void Start()
    {
        collid=GetComponent<BoxCollider2D>();
        materialRenderer = GetComponent<SpriteRenderer>();
        material = materialRenderer.material;
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
            color=material.color;
            material.color = Color.white;
            Debug.Log(material.color);
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
            gameObject.GetComponent<SpriteRenderer>().material.color = color;
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
                GetComponent<Renderer>().material.color = Color.white;
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

    public IEnumerator Fade(){
        float t=0;
        while(t<0.3f){
                t+=Time.deltaTime;
                Color renderer = GetComponent<Renderer>().material.color;
                renderer.a=Mathf.Lerp(1, 0, t/0.3f);
                GetComponent<Renderer>().material.color=renderer;
                yield return null;
            }
        
    }
}
