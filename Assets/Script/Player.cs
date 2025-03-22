using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
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
    public GameObject pause;
    bool isOpenPouse = false;
    [SerializeField] GameObject CutScene;
    PlayableDirector playableDirector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {    
    }
    void Start()
    {
        playableDirector = CutScene.GetComponent<PlayableDirector>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        string tes=PlayerPrefs.GetString("sceneBefore");
        CutScene.SetActive(true);
        if(tes.Equals("Chapter Choice")) playableDirector.Play();
        transform.position = new Vector3(x, y, -5);
        anxStat = PlayerPrefs.GetFloat("anxStat");
        shapeVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector.state == PlayState.Playing)
        {
            isAnimate = true;
        }
        else
        {
            CutScene.SetActive(false);
            playableDirector.Stop();
            isAnimate = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isOpenPouse)
        {
            isOpenPouse = true;
            openPouse();
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
        if (anxStat < 60)
        {
            volume.GetComponent<Image>().color = Color.green;
        }
        else if (anxStat < 80)
        {
            volume.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            volume.GetComponent<Image>().color = Color.red;
        }
        volume.GetComponent<Image>().fillAmount = presentase;
    }
    public void openPouse()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        isOpenPouse = false;
    }



}
