using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameManagerFuckHumans : MonoBehaviour
{
    public static GameManagerFuckHumans gameManagerFuckHumans;

    [Header("Move BackGround")]
    public bool backGround;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float speedBackGround;

    [Header("Shoot Missiles")]
    public bool missiles;
    [SerializeField] float timeShoot, endTimeShoot, timer;
    [SerializeField] GameObject bullet;
    [SerializeField] int positionBullet;
    [SerializeField] Transform[] positionBullets;

    [Header("Coins")]
    public bool coins;
    [SerializeField] float timeCoins, endTimeCoins;
    [SerializeField] GameObject coin;
    public int point;
    [SerializeField] int positionCoin;
    [SerializeField] Transform[] positionCoins;
    [SerializeField] TextMeshProUGUI textCoins;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram()
    {
        if (gameManagerFuckHumans == null)
        {
            gameManagerFuckHumans = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Mover el fondo
        MoveBackGround();
        // Disparar los misiles
        ShootMissiles();
        // Monedas
        PlaceCoin();
        TextCoins();
    }

    ////////////////////////////  MOVER EL FONDO ////////////////////////////////
    private void MoveBackGround()
    {
        if (backGround == true)
        {
            meshRenderer.material.mainTextureOffset = meshRenderer.material.mainTextureOffset += new Vector2(speedBackGround * Time.deltaTime, 0);
        }
    }
    /////////////////////////////////////////////////////////////////////////////
    /////////////////////////// COLOCAR MISILES ////////////////////////////////
    private void ShootMissiles()
    {
        if(missiles == true)
        {
            timeShoot += timer;

            if(timeShoot >= endTimeShoot)
            {
                positionBullet = Random.Range(0, 4);

                Instantiate(bullet, positionBullets[positionBullet].position, Quaternion.identity);
             
                timeShoot = 0;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////
    ////////////////////////// COLOCAR LAS MONEDAS ////////////////////////////
    private void PlaceCoin()
    {
        if(coins == true)
        {
            timeCoins += timer;

            if(timeCoins >= endTimeCoins)
            {
                positionCoin = Random.Range(0, 13);

                Instantiate(coin, positionCoins[positionCoin].position, Quaternion.identity);

                timeCoins = 0;
            }
        }
    }
    private void TextCoins()
    {
        textCoins.text = "Coins: " + point;
    }
    ///////////////////////////////////////////////////////////////////////////
}
