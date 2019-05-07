using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Run,
        Attack,
        Hurt,
        Stayback
    }

    [HideInInspector]
    public EnemyState e;
    [HideInInspector]
    public bool enemyHit;

    public float startAttack;
    public float speed;

    Animator ani;
    GameObject player;
    bool _isNewState = false;
    private AnimatorStateInfo myAnimatorStateInfo;
    private float myAnimatorNormalizedTime = 0.0f;

    EnemyFov fow;
    Vector3 deltaPos;
    Vector2 dir;

    private void Start()
    {
        enemyHit = false;
        ani = GetComponent<Animator>();
        fow = GetComponent<EnemyFov>();
        SetState(EnemyState.Idle);
        StartCoroutine(FSMMain());
        player = GameObject.Find("Player");
    }

    void SetState(EnemyState newState)
    {
        _isNewState = true;
        e = newState;
    }

    private void Update()
    {
        if (enemyHit && e != EnemyState.Hurt)
        {
                SetState(EnemyState.Hurt);
        }

        FlipSprite();

        myAnimatorStateInfo = ani.GetCurrentAnimatorStateInfo(0);
        myAnimatorNormalizedTime = myAnimatorStateInfo.normalizedTime;
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            _isNewState = false;
            yield return StartCoroutine(e.ToString());
        }
    }

    void FlipSprite()
    {
        dir = (Vector2)player.transform.position - (Vector2)transform.position;

        ani.SetFloat("moveX", dir.x);
        ani.SetFloat("moveY", dir.y);

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

    }


    IEnumerator Idle()
    {
        do
        {
            yield return null;
            if (_isNewState) break;
            // Action
            if (fow.visiblePlayer.Contains(player.transform) && player != null)
            {
                SetState(EnemyState.Run);
            }
        } while (!_isNewState);
    }

    IEnumerator Run()
    {
        // Start
        ani.SetBool("moving", true);

        do
        {
            yield return null;
            if (_isNewState) break;

//            FlipSprite();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(!fow.visiblePlayer.Contains(player.transform))
            {
                SetState(EnemyState.Idle);
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= startAttack)
            {
                SetState(EnemyState.Attack);
            }
        } while (!_isNewState);

        //End
        ani.SetBool("moving", false);
    }

    IEnumerator Attack()
    {
        ani.SetBool("attacking", true);

        GetComponent<AudioSource>().Play();

        myAnimatorNormalizedTime = 0;
        Vector2 lastTargetPosition = player.transform.position;

        do
        {
            yield return null;
            if (_isNewState) break;
            transform.position = Vector2.MoveTowards(transform.position, lastTargetPosition, speed * 2f * Time.deltaTime);
            if (myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Attack Tree"))
            {
                SetState(EnemyState.Idle);
            }
        } while (!_isNewState);

        ani.SetBool("attacking", false);

        yield return new WaitForSeconds(1.5f);

        //        if (player != null)
        //            player.GetComponent<PlayerStats>().isHurt = false;
    }

    IEnumerator Hurt()
    {
        // Start
        ani.SetBool("isHurt", true);
        myAnimatorNormalizedTime = 0;

        do
        {
            yield return null;
            if (_isNewState) break;
            // Action
            if (myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Hurt Tree"))
            {
                SetState(EnemyState.Idle);
            }
        } while (!_isNewState);

        //End
        ani.SetBool("isHurt", false);
        yield return new WaitForSeconds(1.5f);
        enemyHit = false;
    }

    IEnumerator Stayback()
    {
        myAnimatorNormalizedTime = 0;
        float timer = 0.0f;
        float waitingTime = 0.3f;

        do
        {
            yield return null;
            if (_isNewState) break;
            timer += Time.deltaTime;

            transform.Translate(deltaPos * 0.8f * Time.deltaTime);

            if (timer > waitingTime)
            {
                SetState(EnemyState.Idle);
                timer = 0;
            }
        } while (!_isNewState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            deltaPos = transform.position - collision.gameObject.transform.position;
            deltaPos = deltaPos.normalized;
            SetState(EnemyState.Stayback);
        }

        if (collision.tag == "Nail")
        {
            enemyHit = true;
            SetState(EnemyState.Hurt);
        }
    }
}
