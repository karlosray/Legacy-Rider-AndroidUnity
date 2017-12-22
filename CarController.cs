using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    public float carSpeed;
    Vector3 position;

    public UIManager ui;
    public AuudioManager am;

    public float maxPos = 2.14f;
    public float minPos = -1.94f;


    bool currentPlatformfromAndroid = false;
    Rigidbody2D rbb;

    void Awake()
    {

        rbb = GetComponent<Rigidbody2D>();
#if UNITY_ANDROID
        currentPlatformfromAndroid = true;
#else
        currentPlatformfromAndroid = false;
#endif
        }
	// Use this for initialization
	void Start () {
        //ui = GetComponent<UIManager>();

        am.carSound.Play();
        position = transform.position;

        if (currentPlatformfromAndroid == true)
        {
            Debug.Log("ANDROID");
        }
        else {
            Debug.Log("WINDOWS");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (currentPlatformfromAndroid == true)
        {
            // Android Specific code
            //TouchMove();
            Accelerometer();

        }
        else
        {

            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -1.41f, 3.94f);
            transform.position = position;
        }
        position = transform.position;
        position.x = Mathf.Clamp(position.x, -1.41f, 3.94f);
        transform.position = position;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy Car")
        {
            // This is also possible if you want your gameObject to not pops Up error messgae
            //gameObject.SetActive(false); 
            Destroy(gameObject);
            ui.gameOvr();
            am.carSound.Stop();
        }
    }


    void Accelerometer()
    {
        float x = Input.acceleration.x;
        if (x < -0.1f)
        {
            MoveLeft();
        }
        else if (x > 0.1f)
        {
            MoveRight();
        }
        else { SetVelZero(); }
    }



    public void TouchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touchy = Input.GetTouch(0); // returns us the value of the very 1st touch
            float middle = Screen.width / 2;

            if (touchy.position.x < middle && touchy.phase == TouchPhase.Began)
            {
                MoveLeft();
            }
            else if (touchy.position.x > middle && touchy.phase == TouchPhase.Began)
            {
                MoveRight();
            }
        }
        else {
            SetVelZero();
        }
    }

    public void MoveLeft()
    {
        rbb.velocity = new Vector2(-carSpeed, 0);
    }


    public void MoveRight()
    {
        rbb.velocity = new Vector2(carSpeed, 0);
    }

    public void SetVelZero()
    {
        rbb.velocity = Vector2.zero;
    }

}
