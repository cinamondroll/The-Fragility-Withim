using UnityEngine;

public class ChosedCardScript : MonoBehaviour
{


    public void SetChosedCard(GameObject card)
    {
        Renderer chosedCard = this.GetComponent<Renderer>();
        chosedCard.material = card.GetComponent<Renderer>().material;
        chosedCard.enabled=card.GetComponent<Renderer>().enabled;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
