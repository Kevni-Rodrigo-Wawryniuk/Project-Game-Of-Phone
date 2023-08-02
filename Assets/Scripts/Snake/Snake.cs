using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool rigth, left, down, up;

    [Header("Move Tails")]
    [SerializeField] GameObject[] tailprefab;
    [SerializeField] int tailSelec;
    [SerializeField] List<GameObject> tailList = new List<GameObject>();

    [Header("End Game")]
    public bool deadSnake;

    [Header("Bottones A Emerger")]
    [SerializeField] GameObject bottonReturnGamme;
    public bool bottonReturnActive;

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

        bottonReturnActive = false;

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
        bottonReturnGamme.SetActive(bottonReturnActive);

        Moviment();
        DeadSnake();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eggs"))
        {
            tailSelec = Random.Range(0, 3);
        
            tailList.Add(Instantiate(tailprefab[tailSelec], tailList[tailList.Count - 1].transform.position, Quaternion.identity));
            
            collision.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-2, 4));
            Debug.Log("Huevo");
        }
        if (collision.gameObject.CompareTag("Tails"))
        {
            deadSnake = true;
            Debug.Log("Cola");
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            deadSnake = true;
        }
    }

    private void Moviment()
    {
        if(move == false)
        {
            if(moveSnake == 1)
            {
                move = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigth = false;
            left = false;
            down = false;
            up = true;
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            rigth = false;
            left = false;
            down = true;
            up = false;
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigth = true;
            left = false;
            down = false;
            up = false;
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            rigth = false;
            left = true;
            down = false;
            up = false;
        }

        if (rigth == true)
        {
            direction = Direction.rigth;
        }else if(left == true)
        {
            direction = Direction.left;
        }else if(up == true)
        {
            direction = Direction.up;
        }else if(down == true)
        {
            direction = Direction.down;
        }

    }
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
    public void DeadSnake()
    {
        if(deadSnake == true)
        {
            bottonReturnActive = true;
            distanceFPS = 0;
            timePosition = 0;

            if (tailList.Count > 0)
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
                bottonReturnActive = false;
            }
        }
    }
    public void BottonReturnGame()
    {
            transform.position = startPosition;
            distanceFPS = 0.3f;
            timePosition = 0.15f;

            moveSnake = 0;
            deadSnake = false;
            move = false;
            bottonReturnActive = false;   
    }
}
