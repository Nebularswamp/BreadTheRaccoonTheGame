using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 5f;

    public float speed;

    Rigidbody2D myRigidbody;
    public Vector3 movement;
    Animator animator;
    PlayerAutoMotor autoMotor;

    public bool isScripting;

    public static bool isHurt;
    bool isflip = false;
    
    void Start()
    {
        speed = normalSpeed;
        autoMotor = GetComponent<PlayerAutoMotor>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isHurt = false;

//        Time.timeScale = 2f;
    }

    void Update()
    {
        if (!isScripting)
        {
            movement = Vector3.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (PlayerBehaviors.hunger < 50)
            {
                animator.SetBool("Hungry", true);
                if (1 < PlayerBehaviors.hunger && PlayerBehaviors.hunger < 50)
                {
                    speed = normalSpeed * (2f / 3f);
                }
                if (PlayerBehaviors.hunger <= 1)
                {
                    speed = normalSpeed * (1f / 3f);
                }
            }
            else
            {
                animator.SetBool("Hungry", false);
                speed = normalSpeed;
            }
        }

        if(movement != Vector3.zero && isHurt == false)
        {
            MoveCharacter();
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("moving", true);

            if (movement.x < 0)
            {
                if (!isflip)
                {
                    transform.Rotate(new Vector3(0f, -180f, 0));
                    isflip = true;
                }
//                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (movement.x > 0)
            {
                if (isflip)
                {
                    transform.Rotate(new Vector3(0f, -180f, 0));
                    isflip = false;
                }
                //                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if (PlayerBehaviors.health < 30)
        {
            animator.SetBool("Damaged", true);
        }
        else
        {
            animator.SetBool("Damaged", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    public void MoveMotor(Vector3 _movement, float time)
    {
        movement = _movement;
        StartCoroutine(MoveMotor(time));
    }

    IEnumerator MoveMotor(float time)
    {
        yield return new WaitForSeconds(time);
        movement = Vector3.zero;
        autoMotor.isAutoMoving = false;
        ScriptLocator.scriptParser.CommandRun();
    }
}