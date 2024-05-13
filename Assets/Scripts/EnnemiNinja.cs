
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiNinja : MonoBehaviour
{
    public bool sMortTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isMortTriggered)
        {
            InvokeRepeating("AttaqueEnnemi", 0f, 5f);
        }
    }

    private bool isMortTriggered = false;

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (!isMortTriggered && infoCollision.gameObject.tag == "Ninja")
        {
            isMortTriggered = true;
            GetComponent<Animator>().SetTrigger("mort");
            Invoke("Mort", 2f);
        }
    }

    void Mort()
    {
        Destroy(gameObject);
    }
    void AttaqueEnnemi()
    {
        GetComponent<Animator>().SetTrigger("attaqueAnim");
    }
}