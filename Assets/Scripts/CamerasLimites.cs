using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasLimites : MonoBehaviour
{
    public float limiteGauche1;
    public float limiteDroite1;
    public float limiteHaut;
    public float limiteBas;

    public float limiteGauche2;
    public float limiteDroite2;


    public float limiteScene2;
    public GameObject Ninja;

    void Update()
    {
        Vector3 camPosition = transform.position;

        if (camPosition.y >= limiteHaut)
        {
            camPosition.y = limiteHaut;
        }
        else if (camPosition.y <= limiteBas)
        {
            camPosition.y = limiteBas;
        }

        if (Ninja.transform.position.x <= 34)
        {
            // Vérifie si la caméra est à l'intérieur des premières limites horizontales
            if (camPosition.x < limiteGauche1)
            {
                camPosition.x = limiteGauche1;
            }
            else if (camPosition.x > limiteDroite1)
            {
                camPosition.x = limiteDroite1;
            }
        }

        // Vérifie si la caméra est à l'intérieur des deuxièmes limites horizontales

        if (Ninja.transform.position.x >= 34)
        {
            if (camPosition.x < limiteScene2) // Mets la caméra dans la scène 2
            {
                camPosition.x = limiteScene2;
            }
            else if (camPosition.x < limiteGauche2)
            {
                camPosition.x = limiteGauche2;
            }
            else if (camPosition.x > limiteDroite2)
            {
                camPosition.x = limiteDroite2;
            }
        }

        transform.position = camPosition;
    }
}
