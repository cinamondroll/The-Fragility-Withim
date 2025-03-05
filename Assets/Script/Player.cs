using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float movespeed;
    public GameObject Chat;
    GameManager Operator;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
           float hInput=Input.GetAxis("Horizontal");
           transform.Translate(hInput*movespeed, 0, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {   

        Debug.Log("111111");
        if (other.gameObject.tag=="npc")
        {
        SceneManager.LoadScene("chat");
            
        }       
        
        
    }
    
 


}
