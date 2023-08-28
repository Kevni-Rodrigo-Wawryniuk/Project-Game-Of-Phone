using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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
    [SerializeField] Image[] lifes; // estat son las vidas que se muestran en la esquina de la pantalla

    [Header("Move")]
    public bool move;
    [SerializeField] float forceDown, forceUp;

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
        // Mover la nave de abajo hacia arriba
        MoveNave();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coins"))
        {
            GameManager.gameManager.coinsScore++;
            GameManagerFuckHumans.gameManagerFuckHumans.point++;
        }
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
            lifes[0].enabled = false;
            lifes[1].enabled = false;
            lifes[2].enabled = false;

            live = false;
            ParticlesDead();
        }
        // Aqui solo puede soportar un golpe 
        if(lives == 1)
        {
            lifes[0].enabled = true;
            lifes[1].enabled = false;
            lifes[2].enabled = false;

            animN.SetBool("VidaMedia", false);
            animN.SetBool("VidaBaja", true);
        }
        // Aqui puede soportar dos golpes
        if(lives == 2)
        {
            lifes[0].enabled = true;
            lifes[1].enabled = true;
            lifes[2].enabled = false;

            animN.SetBool("VidaMedia", true);
            animN.SetBool("VidaBaja", false);
        }
        // Aqui puede soportar tres golpes
        if (lives == 3)
        {
            lifes[0].enabled = true;
            lifes[1].enabled = true;
            lifes[2].enabled = true;

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
   
    ///////////////////////////// HACER QUE LA NAVE SALTE ///////////////////////////////////
    private void MoveNave()
    {
        if(move == true)
        {

            rgbN.velocity = new Vector2(0, -forceDown);

            // esto es para ver si el jugador a tocado la pantalla
            if (Input.touchCount > 0)
            {
                // esto dispara un rayo en la direccion donde se a pulsado 
                Touch touch = Input.GetTouch(0);

                // esto es para cuando se pulsa el boton
                if (touch.phase == TouchPhase.Began)
                {
                    rgbN.velocity = new Vector2(0, 0);
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    rgbN.velocity = new Vector2(0, forceUp);
                }
            }

            if (transform.position.y <= -8)
            {
                lives = 0;
            }

            // limitar el movimiento de la nave en el eje Y
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -7, 7));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////// DISPARAR PERSONAS //////////////////////////////////////////
    public void Shotting()
    {

    }
    ////////////////////////////////////////////////////////////////////////////////////////
}
