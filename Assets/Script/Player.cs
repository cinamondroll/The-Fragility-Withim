using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float movespeed;
    Rigidbody rb;
 
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
   
}
