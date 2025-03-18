using UnityEngine;

public class StartProlog : MonoBehaviour
{
    [SerializeField] PrologScript manager;
    [SerializeField] DialogNode startNode;
    void Start()
    {
        Invoke("StartThis", 1f);
    }
    void StartThis()
    {
        manager.StartDialog(startNode);
        
    }
}
