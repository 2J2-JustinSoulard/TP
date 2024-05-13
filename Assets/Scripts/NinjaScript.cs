using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaScript : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée
    public bool partieTerminee;
    public bool attaque;

    public GameObject FinScene1;
    public GameObject DebutScene2;

    public GameObject ImgBulle1;
    public GameObject ImgBulle2;

    void Update()
    {
        if (!partieTerminee)
        {
            // déplacement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   //déplacement vers la droite
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
            }

            // sauter l'objet à l'aide la touche "w"

            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow) && Physics2D.OverlapCircle(transform.position, 1f))
            {
                vitesseY = vitesseSaut;
                GetComponent<Animator>().SetBool("saut", true);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
            }

            if (Input.GetKeyDown(KeyCode.Space) && !attaque)
            {
                attaque = true;
                Invoke("AnnulerAttaque", 0.4f);
                GetComponent<Animator>().SetTrigger("attaqueAnim");
                GetComponent<Animator>().SetBool("saut", false);
            }

            if (attaque && vitesseX <= vitesseXMax && vitesseX >= -vitesseXMax)
            {
                vitesseX *= 3;
            }

            //Applique les vitesses en X et Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);

            //**************************Gestion des animaitons de course et de repos********************************
            //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator

            if (vitesseX > 0.05f || vitesseX < -0.3f)
            {
                GetComponent<Animator>().SetBool("course", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("course", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        // Vérifie si le personnage touche le sol (objet avec le layer "Ground")
        if (infoCollision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<Animator>().SetBool("saut", false);
        }

        //if (infoCollision.gameObject.name == "Bombe")
        //{
        //    partieTerminee = true;
        //    GetComponent<Animator>().SetTrigger("mort");
        //    if (transform.position.x > infoCollision.transform.position.x)
        //    {
        //        GetComponent<Rigidbody2D>().velocity = new Vector2(10, 30);
        //    }
        //    else
        //    {
        //        GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 30);
        //    }
        //    Invoke("recommencer", 3f);
        //}
        else if (infoCollision.gameObject.name == "EnnemiNinja")
        {
            if (!attaque)
            {
                GetComponent<Animator>().SetTrigger("mort");
                Invoke("recommencer", 3f);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D infoCollision)
    {
        if (infoCollision.gameObject.CompareTag("Sensai"))
        {
            partieTerminee = true;
            GetComponent<Animator>().SetBool("course", false);
            Invoke("ArretPersonnage", 3f);
            Invoke("ChangeImg", 1.5f);
            infoCollision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            //GetComponent<AudioSource>().PlayOneShot();
        }

        if (FinScene1 != null && DebutScene2 != null && infoCollision.gameObject.name == "FinScene1")
        {
            gameObject.transform.position = DebutScene2.transform.position;
        }

    }


    void recommencer()
    {
        SceneManager.LoadScene(0);
    }
    void AnnulerAttaque()
    {
        attaque = false;
    }
    void ArretPersonnage()
    {
        partieTerminee = false;

    }
    void ChangeImg()
    {
        ImgBulle2.SetActive(true);
        ImgBulle1.SetActive(false);
    }
}