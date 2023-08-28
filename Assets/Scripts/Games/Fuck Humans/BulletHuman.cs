using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHuman : MonoBehaviour
{
    public static BulletHuman bulletHuman;

    [Header("Components")]
    [SerializeField] Rigidbody2D rgb;

    [Header("Move Bullet")]
    public bool move;
    [SerializeField] float speed;


    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram()
    {
        if(bulletHuman == null)
        {
            bulletHuman = this;
        }

        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AlienShip"))
        {
            Destroy(this.gameObject);
        }
    }

    ///////////////////// MOVER LA BALA /////////////////////////
    private void moveBullet()
    {
        if(move == true)
        {
            rgb.velocity = new Vector2(speed, 0);

            if(transform.position.x <= -14)
            {
                Destroy(this.gameObject);
            }
        }
    }
    /////////////////////////////////////////////////////////////
}
