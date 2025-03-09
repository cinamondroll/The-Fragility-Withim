using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerChat : MonoBehaviour
{   
    public GameObject card;
    public GameObject panel;
    void Start()
    {
        
    }
    void Update()
    {
     if (Input.GetMouseButtonDown(0))
     {
         card.SetActive(true);
        panel.SetActive(true);
     }   
    }

    void chooseCard(){
        GameObject ChosedCard = GameObject.Find("ChosedCard");
    }

   
}
