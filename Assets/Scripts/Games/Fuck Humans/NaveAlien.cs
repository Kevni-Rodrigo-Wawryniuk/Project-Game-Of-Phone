using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveAlien : MonoBehaviour
{
    public static NaveAlien naveAline;

    [Header("Components")]
    Rigidbody2D rgbN;
    Animator animN;

    [Header("Live")]
    public bool live;
    [SerializeField] int lives; // Las vidas maximas son 3
    [SerializeField] GameObject particlesDead; // Particulas que se instancian cuando el jugador muere

    [Header("Move")]
    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    void StartProgram()
    {
        if(naveAline == null)
        {
            naveAline = this;
        }

        rgbN = GetComponent<Rigidbody2D>();
        animN = GetComponent<Animator>();

        lives = 3;
        live = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Aqui se maneja el estado de la nave y cuantos golpes puede soportar
        Lives();
    }

    ///////////////////// VIDAS DE LA NAVE Y SU ESTADO DE RESISTENCIA ////////////////////////
    private void Lives()
    {
        this.gameObject.SetActive(live);

        if(lives > 0)
        {
            live = true;
        }
        else
        {
            live = false;
            ParticlesDead();
        }
        // Aqui solo puede soportar un golpe 
        if(lives == 1)
        {
            animN.SetBool("VidaMedia", false);
            animN.SetBool("VidaBaja", true);
        }
        // Aqui puede soportar dos golpes
        if(lives == 2)
        {
            animN.SetBool("VidaMedia", true);
            animN.SetBool("VidaBaja", false);
        }
        // Aqui puede soportar tres golpes
        if (lives == 3)
        {
            animN.SetBool("VidaMedia", false);
            animN.SetBool("VidaBaja", false);
        }
    }
    private void ParticlesDead()
    {
        Instantiate(particlesDead, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
        particlesDead.GetComponent<ParticleSystem>().Play();
    }
    /////////////////////////////////////////////////////////////////////////////////////////
}
