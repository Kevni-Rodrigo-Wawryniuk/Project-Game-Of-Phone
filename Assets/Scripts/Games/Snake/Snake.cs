using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    public static Snake snake;

    [Header("Move")]
    public bool move;
    [SerializeField] Vector3 startPosition, lastPosition;
    [SerializeField] enum Direction
    {
        up,rigth,left,down,
    }
    [SerializeField] Direction direction;
    [SerializeField] float timePosition,distanceFPS;
    public int moveSnake;
    // parametros para la dreccion
    [SerializeField] Vector2 fingerUp, fingerDown;
    // la distancia del desliz para determinar la direccion
    [SerializeField] float minDistance;
    // Evento que se activa cuando determina el parametro de direccion
    public static event System.Action<Vector2> onSwipe;

    [Header("Move Tails")]
    [SerializeField] GameObject[] tailprefab;
    [SerializeField] int tailSelec;
    [SerializeField] List<GameObject> tailList = new List<GameObject>();

    [Header("End Game")]
    public bool deadSnake;

    [Header("Audio")]
    [SerializeField] AudioSource food, dead;

    [Header("Points")]
    public int pointScreem, lastPointsPlay, lastScore;
    public int pointBuy, onePointBuy;

    [Header("Tutorial")]
    [SerializeField] GameObject activeTutorial; 

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    void StartProgram()
    {
        if(snake == null)
        {
            snake = this;
        }

        onePointBuy = 10;

        lastScore = PlayerPrefs.GetInt("LastPoint", 0);

        moveSnake = 0;

        move = false;

        deadSnake = false;

        startPosition = transform.position;

        Time.timeScale = 1;
   
        InvokeRepeating("Move", timePosition, timePosition);
    }
    private void Move()
    {
        if (move == true)
        {
            lastPosition = transform.position;

            Vector3 nextPosition = Vector3.zero;

            if (direction == Direction.up)
            {
                nextPosition = Vector3.up;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (direction == Direction.down)
            {
                nextPosition = Vector3.down;
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (direction == Direction.rigth)
            {
                nextPosition = Vector3.right;
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (direction == Direction.left)
            {
                nextPosition = Vector3.left;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }

            nextPosition *= distanceFPS;
            transform.position += nextPosition;
            MoveTails();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moviment();
        DeadSnake();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eggs"))
        {
            pointScreem++;

            tailSelec = Random.Range(0, 3);
        
            tailList.Add(Instantiate(tailprefab[tailSelec], tailList[tailList.Count - 1].transform.position, Quaternion.identity));
            
            collision.transform.position = new Vector2(Random.Range(-11.3f, 11.3f), Random.Range(-6, 4.6f));
            food.Play();
            Debug.Log("Huevo");

            if(pointBuy == onePointBuy)
            {
                GameManager.gameManager.coinsScore++;
                PlayerPrefs.SetInt("CoinsBuy", GameManager.gameManager.coinsScore);
                onePointBuy += Random.Range(5,10);
            }

        }
        if (collision.gameObject.CompareTag("Tails"))
        {
            deadSnake = true;
            dead.Play();
            lastPointsPlay = pointScreem;
            Debug.Log("Cola");
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            lastPointsPlay = pointScreem;
            dead.Play();
            deadSnake = true;
        }
    }

    private void Moviment()
    {
        pointBuy = pointScreem;

        Tutorial();

        if (move == false)
        {
            if(moveSnake == 1)
            {
                Invoke("LastPoint", 0.1f);
                move = true;
            }
        }

        DetectSwipe();
    }
    ///////////////////////////////////// DIRECCIONES DETERMINADAS ////////////////////////////////////////

    // Esto es para el juego de la viborita se usaria para direcciones ya determinadas
    // osea solo moverte entre cuatro direcciones que son arriba, abajo, izquierda, derecha
    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDown = touch.position;
                fingerUp = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerUp = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUp = touch.position;
                CheckSwipe();
            }
        }
    }
    void CheckSwipe()
    {
        // darle los valores del touch a una variable
        Vector2 swipeDelta = fingerUp - fingerDown;

        // la magnitud del desliz del dedo debe ser mayor a la distancia minima
        if (swipeDelta.magnitude > minDistance)
        {
            // normalizar la maginitud del desliz
            swipeDelta.Normalize();

            // detectar el angulo de desplazamiento
            float angel = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;

            // convertir el angulo en un valor positivo en un rango de 0-360 grados
            if (angel < 0)
            {
                angel += 360;
            }

            // determinar la direccion en funcion del angulo Se puede usar un enum para esto como en el juego de la snake

            // Derecha
            if (angel < 45f || angel > 315f)
            {
                moveSnake = 1;
                Debug.Log("derecha 0");
                direction = Direction.rigth;
            }
            // Arriba
            else if (angel > 45f && angel < 135f)
            {
                moveSnake = 1;
                Debug.Log("arriba 1");
                direction = Direction.up;
            }
            // izquierda
            else if (angel > 135f && angel < 225f)
            {
                moveSnake = 1;
                Debug.Log("izquierda 2");
                direction = Direction.left;
            }
            // Abajo
            else
            {
                moveSnake = 1;
                Debug.Log("abajo 4");
                direction = Direction.down;
            }

            // llamamos al evento onSwipe y pasa la direccion del deslizamiento a un parametro
            onSwipe?.Invoke(swipeDelta);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    private void MoveTails()
    {
        for (int i = 0; i < tailList.Count; i++)
        { 
            Vector3 temp;
            temp = tailList[i].transform.position;
            tailList[i].transform.position = lastPosition;
            lastPosition = temp;
        }
    }

    private void Tutorial()
    {

        if (move == false && deadSnake == false)
            {
                activeTutorial.SetActive(true);   
            }

        if (move == true && deadSnake == false)
            {
                activeTutorial.SetActive(false);
            }
    }
    public void DeadSnake()
    {
        if(deadSnake == true)
        {
            move = false;
            distanceFPS = 0;
            timePosition = 0;

            if (tailList.Count > 1)
            {
                for (int i = 1; i < tailList.Count; i++)
                {
                    Destroy(tailList[i].gameObject);
                    tailList.RemoveAt(i);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = startPosition;
                distanceFPS = 0.3f;
                timePosition = 0.15f;

                moveSnake = 0;
                deadSnake = false;
                move = false;
                pointScreem = 0;
            }
        }
    }
    public void LastPoint()
    {
        lastScore = PlayerPrefs.GetInt("LastPoint", 0);
    }
    public void BottonReturnGame()
    {
        if (lastScore >= 0 && lastScore < pointScreem)
        {
            PlayerPrefs.SetInt("LastPoint", pointScreem);
        }
     
        if (tailList.Count > 1)
        {
            for (int i = 0; i < tailList.Count; i++)
            {
                Destroy(tailList[i].gameObject);
                tailList.RemoveAt(i);
            }
        }

        transform.position = startPosition;
        distanceFPS = 0.3f;
        timePosition = 0.15f;
        moveSnake = 0;
        deadSnake = false;
        move = false;
        pointScreem = 0;
        Time.timeScale = 1;
        GameManager.gameManager.pause = false;
    }
    public void BottonOutGame()
    {
        GameManager.gameManager.mainMenu = true;
        GameManager.gameManager.settings = false;
        GameManager.gameManager.selectionGames = false;
        GameManager.gameManager.snakeGame = false;

        if (lastScore >= 0 && lastScore < pointScreem)
        {
            PlayerPrefs.SetInt("LastPoint", pointScreem);
        }

        if (tailList.Count > 1)
        {
            for (int i = 0; i < tailList.Count; i++)
            {
                Destroy(tailList[i].gameObject);
                tailList.RemoveAt(i);
            }
        }
        transform.position = startPosition;
        distanceFPS = 0.3f;
        timePosition = 0.15f;
        moveSnake = 0;
        deadSnake = false;
        move = false;
        pointScreem = 0;
        GameManager.gameManager.pause = false;
        Time.timeScale = 1;

        Debug.Log("Salir del Juego De la viborita");
    }
}
