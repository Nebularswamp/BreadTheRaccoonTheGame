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
        Hurt
    }

    [HideInInspector]
    public EnemyState e;
    [HideInInspector]
    public bool enemyHit;

    public float startChasing;
    public float stopChasing;
    public float startAttack;
    public GameObject player;
    public float speed;

    Animator ani;
    bool _isNewState = false;
    private AnimatorStateInfo myAnimatorStateInfo;
    private float myAnimatorNormalizedTime = 0.0f;

    EnemyFov fow;

    private void Start()
    {
        enemyHit = false;
        ani = GetComponent<Animator>();
        fow = GetComponent<EnemyFov>();
        SetState(EnemyState.Idle);
        StartCoroutine(FSMMain());
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
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }


    IEnumerator Idle()
    {
//        transform.Rotate(new Vector3(0, 0, 90f));
        // Starta
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

        //End
    }

    IEnumerator Run()
    {
        // Start
        ani.SetBool("moving", true);

        do
        {
            yield return null;
            if (_isNewState) break;

            FlipSprite();
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
        myAnimatorNormalizedTime = 0;
        Vector2 lastTargetPosition = player.transform.position;

        do
        {
            yield return null;
            if (_isNewState) break;
            transform.position = Vector2.MoveTowards(transform.position, lastTargetPosition, speed * 2f * Time.deltaTime);
            if (myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Attack"))
            {
                SetState(EnemyState.Idle);
            }
        } while (!_isNewState);

        ani.SetBool("attacking", false);
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
            if (myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Hurt"))
            {
                SetState(EnemyState.Idle);
            }
        } while (!_isNewState);

        //End
        ani.SetBool("isHurt", false);
        enemyHit = false;
    }
}
