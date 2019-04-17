using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator animator = GameObject.Find("Player").GetComponent<Animator>();
            animator.SetTrigger("hurt");
            StartCoroutine(TakeDamage());
            TakeEffect();
        }
    }

    public void TakeEffect()
    {
        int[] effects = ItemWiki.ReturnEffects(itemName);

        PlayerBehaviors.health += effects[0];
        PlayerBehaviors.health = Mathf.Clamp(PlayerBehaviors.health, 0, 100);

        PlayerBehaviors.hunger += effects[1];
        PlayerBehaviors.hunger = Mathf.Clamp(PlayerBehaviors.hunger, 0, 100);

        PlayerBehaviors.sanity += effects[2];
        PlayerBehaviors.sanity = Mathf.Clamp(PlayerBehaviors.sanity, 0, 100);
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerMovement.isHurt = true;
        yield return new WaitForSeconds(1);
        PlayerMovement.isHurt = false;
    }
}
