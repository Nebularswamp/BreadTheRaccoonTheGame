using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed =3;

    private Animator animator;


    public bool ishurting;
    public bool isEating;

    public static PlayerController instance;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        SetAnimationStates();

        animator.SetBool("walking", true);
    }

    void SetAnimationStates()
    {
        animator.SetBool("walkDown", false);
        animator.SetBool("walkUp", false);
        animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);
        animator.SetBool("eating", false);
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Update()
    {
        StartCoroutine(Eats());
    }

    void Walk()
    {
        /*
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
        */

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

}
