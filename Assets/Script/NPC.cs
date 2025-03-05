using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    GameManager Operator;
    public GameObject Chat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {          
        Chat.SetActive(true);
          
        
        }
    
    void OnCollisionStay2D(Collision2D other)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("11111");
            SceneManager.LoadScene("chat");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Chat.SetActive(false);  
      
    }
    }

