using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int health = 100;

    public int pointsPerApple = 10;
    public int pointsPerPie = 20;
    public int pointsLossPerGlass = 15;
    public int pointsLossPerNails = 25;

    public Text hpDisplay;
    public Text itemtext;

    public float speed = 3;

    public float restartLevelDelay = 1f;

    private Animator animator;
    
    private Rigidbody2D rb2D;

    public bool ishurting;
    public bool isEating;
    

    public GameObject theApple;
    public GameObject thePie;

    public bool onApple;
    public bool onPie;

    // Use this for initialization
    void Start ()
    {

        animator = GetComponent<Animator>();

        health = GameManager.instance.healthPoints;

        animator.SetBool("walkDown", false);
        animator.SetBool("walkUp", false);
        animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);
        animator.SetBool("eating", false);

        hpDisplay.text = "health:" + health;
        

        rb2D = GetComponent<Rigidbody2D>();

        InvokeRepeating("isHurtingStop", 0f, 0.5f);
    }

    private void OnDisable()
    {
        GameManager.instance.healthPoints = health;
    }
	
    private void CheckIfGameOver()
    {
        if (health <= 0)
            GameManager.instance.GameOver();
    }
    void Update ()
    {

        if (ishurting)
        {
            animator.SetBool("walking", false);
            animator.SetBool("hurting", true);

        }else if (!ishurting)
        {
            animator.SetBool("hurting", false);
            animator.SetBool("walking", true);
        }

        if (onApple)
        {
            itemtext.text = "fruit thing";
        }

        if (onPie)
        {
            itemtext.text = "Chocolaty thing";
        }

        if (!onApple && !onPie)
        {
            itemtext.text = "";
        }

        if (onApple && theApple && Input.GetKey(KeyCode.E))
        {
            health += pointsPerApple;
            hpDisplay.text = "health:" + health;
            Destroy(theApple.gameObject);
            Debug.Log("entered");
            isEating = true;

            animator.SetBool("eating", true);
            StartCoroutine(Eats());
        }

        

        if (onPie && thePie && Input.GetKey(KeyCode.E))
        {
            health += pointsPerPie;
            hpDisplay.text = "health:" + health;
            Destroy(thePie.gameObject);
            Debug.Log("entered");
            isEating = true;
            animator.SetBool("walking", false);
            animator.SetBool("eating", true);
            StartCoroutine(Eats());
        }

    }

    IEnumerator Eats()
    {
        if (isEating)
        {
            
            yield return new WaitForSeconds(2);
        }

        isEating = false;
        animator.SetBool("walking", false);
        animator.SetBool("eating", false);

    }


	void FixedUpdate ()
    {

        if (ishurting)
        {
            animator.SetBool("walking", false);
            animator.SetBool("hurting", true);

        }
        else if (!ishurting)
        {
            animator.SetBool("hurting", false);
            animator.SetBool("walking", true);

        }

        if (Input.GetKey(KeyCode.A) && !isEating)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            animator.SetBool("walkDown", false);
            animator.SetBool("walkUp", false);
            animator.SetBool("walkLeft", true);
            animator.SetBool("walkRight", false);


        }
        if (Input.GetKey(KeyCode.D) && !isEating)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            animator.SetBool("walkDown", false);
            animator.SetBool("walkUp", false);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", true);

        }
        if (Input.GetKey(KeyCode.W) && !isEating)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            animator.SetBool("walkDown", false);
            animator.SetBool("walkUp", true);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", false);


        }
        if (Input.GetKey(KeyCode.S) && !isEating)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            animator.SetBool("walkDown", true);
            animator.SetBool("walkUp", false);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkRight", false);

        }
    }

    void isHurtingStop()
    {
   
        ishurting = false;

    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag =="Apple")
        {

            onApple = true;
            theApple = other.gameObject;
            Debug.Log("isOnApple");

        }

        if (other.tag == "ChocoPie")
        {
            onPie = true;
            thePie = other.gameObject;
        }

        if (other.tag == "glass")
            {

                health -= pointsLossPerGlass;

                hpDisplay.text = "health:" + health;

                ishurting = true;

            }

            if (other.tag == "Nails")
            {
                health -= pointsLossPerNails;

                hpDisplay.text = "health:" + health;

                ishurting = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Apple")
        {
            onApple = false;
            Debug.Log("notOnApple");
        }

        if (other.tag == "ChocoPie")
        {
            onPie = false;
        }
    }



    }

