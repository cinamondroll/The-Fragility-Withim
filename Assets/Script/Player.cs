using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float movespeed;
    Rigidbody rb;
    [SerializeField]private float anxStat;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        
    }
    void Start()
    {
        float x=PlayerPrefs.GetFloat("x");
        float y=PlayerPrefs.GetFloat("y");
        transform.position=new Vector3(x,y,-5);
        anxStat=PlayerPrefs.GetFloat("anxStat");
        shapeVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
           float hInput=Input.GetAxis("Horizontal");
           if (hInput<0 && transform.localScale.x>0)
           {
               transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
           }
           else if (hInput>0 && transform.localScale.x<0)
           {
               transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
           }
           transform.Translate(hInput*movespeed, 0, 0);
    }
    public float getAnxSta()
    {
        return anxStat;
    }
     public void shapeVolume(){
        GameObject volume=GameObject.Find("Volume");
        float presentase=volume.GetComponent<Image>().fillAmount;
        if (anxStat<=100) presentase=anxStat/100;
        volume.GetComponent<Image>().fillAmount=presentase;
    }
    
   
}
