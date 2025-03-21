using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float movespeed;
    Rigidbody2D rb;
    [SerializeField] private float anxStat;
    public new Animator animation;
    private bool isAnimate = true;
    float t;
    private bool isSound = false;
    public AudioClip[] audioStep;
    AudioSource audioSource;
    Coroutine walkss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
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
            if (!isSound)
            {
                isSound = true;
                walkss = StartCoroutine(playStep());
            }
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
            if (isSound)
            {
                isSound = false;
                audioSource.Stop();
            }
        }
    }

    IEnumerator playStep()
    {
        if (isSound == false)
        {
            yield return null;
        }
        else
        {
            audioSource.clip = audioStep[Random.Range(0, audioStep.Length)];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isSound = false;
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
        if (anxStat<60)
        {
            volume.GetComponent<Image>().color = Color.green;
        }
        else if (anxStat<80)
        {
            volume.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            volume.GetComponent<Image>().color = Color.red;
        }
        volume.GetComponent<Image>().fillAmount = presentase;
    }


}
