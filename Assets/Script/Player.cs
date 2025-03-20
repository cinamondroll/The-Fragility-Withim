using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float movespeed;
    Rigidbody2D rb;
    [SerializeField] private float anxStat;
    public Animator animation;
    private bool isAnimate = true;
    float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    async Task Start()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        transform.position = new Vector3(x, -0.29f, -5);
        anxStat = PlayerPrefs.GetFloat("anxStat");
        shapeVolume();
    }

    // Update is called once per frame
    void Update()
    {

        if (t < 4.6f)
        {
            t += Time.deltaTime;
        }
        else
        {
            isAnimate = false;
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 && !isAnimate)
        {
            float hInput = Input.GetAxis("Horizontal");
            animation.SetBool("isWalk", true);
            if (hInput < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (hInput > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            rb.velocity = new Vector2(hInput * movespeed, rb.velocity.y);
        }
        else
        {
            animation.SetBool("isWalk", false);
        }
    }
    public float getAnxSta()
    {
        return anxStat;
    }
    public void shapeVolume()
    {
        GameObject volume = GameObject.Find("Volume");
        float presentase = volume.GetComponent<Image>().fillAmount;
        if (anxStat <= 100) presentase = anxStat / 100;
        volume.GetComponent<Image>().fillAmount = presentase;
    }


}
