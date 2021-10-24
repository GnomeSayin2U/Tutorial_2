using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
    public Text score;
    public Text Lives;
    public Text Win;
    public Text Lose;
    private int LivesValue = 3;
    private int scoreValue = 0;
    private bool facingRight = true;
    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    public AudioClip musicClipOne;

    public AudioClip musicCliptwo;

    public AudioClip musicClipthree;

    public AudioSource musicSource;

    


    Animator Anim; 
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        SetScoreText();
        Win.text = "";
        Lose.text ="";
        SetLivesText();
    }

    
    
    // Update is called once per frame
   
   
   void Update()
   {
       float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
       
       
       
        if(Input.GetKeyDown(KeyCode.W))
        {
            Anim.SetInteger("State", 2);
        }
        
        if(Input.GetKeyUp(KeyCode.W))
        {
            Anim.SetInteger("State", 0);
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            Anim.SetInteger("State", 1);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            Anim.SetInteger("State", 0);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Anim.SetInteger("State", 1);
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            Anim.SetInteger("State", 0);
        }
    
        if(facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if(facingRight == true && hozMovement < 0)
        {
            Flip();
        }


        if(hozMovement > 0 && facingRight == true)
        {
            Debug.Log ("facing Right");
        }

        if(hozMovement < 0 && facingRight == false)
        {
            Debug.Log ("facing left");
        }

        if(vertMovement > 0 && isOnGround == false)
        {
            Debug.Log ("Jumping");
        }
    
    }
   
   
   
   
   
   
   
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

       

    }

    void SetLivesText()
    {
        Lives.text = "Lives " + LivesValue.ToString();

        if(LivesValue == 0)
        {
            Lose.text = "You Lose! Game Created by Matthew Ciafone";
            
            musicSource.loop = false;
            musicSource.clip = musicClipthree;
            musicSource.Play();

            Destroy(this);
        }
    }

    void SetScoreText()
    {
        score.text = "Score " + scoreValue.ToString();

        if(scoreValue >= 10)
        {
            Win.text = "You Win! Game Created by Matthew Ciafone";

            musicSource.loop = false;
            musicSource.clip = musicCliptwo;
            musicSource.Play();
            
            Destroy(this);
        }

        if(scoreValue == 5)
        {
            transform.position = new Vector2(111.42f, -1.92f);
            LivesValue = 3;
            Lives.text = "Lives " + LivesValue.ToString();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);

           
        }



        if(collision.collider.tag == "Enemy")
        {
            LivesValue = LivesValue -1; 
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
    }





    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
                {
                     rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); 
                }
        }
    }

    void Flip()
    {
        facingRight =! facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}
