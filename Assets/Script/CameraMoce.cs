using UnityEngine;

public class CameraMoce : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y= transform.position.y;
        if (transform.position.x < 13&& transform.position.x > -13)
        {
        transform.position = new Vector3(player.position.x, y, transform.position.z);
        }
        if (player.position.x < 13 && player.position.x > -13)
        {
            transform.position = new Vector3(player.position.x, y, transform.position.z);
        }
        {
            
        }
     
        
    }
   

}
