using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash_Cat : MonoBehaviour
{
    public Image cat;
    public Image attack;
    public GameObject panel;
    public GameObject catEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boo()
    {
        StartCoroutine(Boooo());
        Vector3 location = new Vector3(Random.Range(1f, 5f), Random.Range(1f, 5f), 0);
        Instantiate(catEnemy, transform.position + location, Quaternion.identity);
    }

    IEnumerator Boooo()
    {
        GetComponent<AudioSource>().Play();
        panel.GetComponent<Image>().enabled = true;
        cat.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        attack.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1f);
        panel.GetComponent<Image>().enabled = false;
        cat.GetComponent<Image>().enabled = false;
        attack.GetComponent<Image>().enabled = false;
    }
}
