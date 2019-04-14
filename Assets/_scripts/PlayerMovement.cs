using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject dialogText;

    Rigidbody2D myRigidbody;
    Vector3 movement;
    Animator animator;
    bool isAnimation = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //isAnimation = true;
        //StartCoroutine(Animation());
    }

    void Update()
    {

        if (!isAnimation)
        {
            movement = Vector3.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if(movement != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("moving", true);

            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (movement.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    IEnumerator Animation()
    {
        movement.x = 1.0f;
        yield return new WaitForSeconds(1.3f);
        movement.x = -1.0f;
        yield return new WaitForSeconds(1.35f);
        movement.x = 0;
        movement.y = -1.0f;
        yield return new WaitForSeconds(0.6f);
        movement.y = 0f;
        dialogText.SetActive(true);
        yield return new WaitForSeconds(2f);
        dialogText.SetActive(false);
        isAnimation = false;
    }
}