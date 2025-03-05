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
        SceneManager.LoadScene("Dialog C1 part 1");
        
        
        }
    }

