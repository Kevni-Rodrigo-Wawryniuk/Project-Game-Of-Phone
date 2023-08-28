using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Canvas")]
    // Aqui estaran los menus 
    // 0 = Main Menu
    // 1 = Settings
    // 2 = Selection Game
    [SerializeField] Canvas[] canvasMenus;
    public bool mainMenu, settings, selectionGames;

    [Header("Games")]
    public int gamesPlay;
    [SerializeField] GameObject[] gamePlayBottons;
    [SerializeField] Vector3 bottonOn, bottonsOff;

    [Header("Pause")]
    public bool pause;
    [SerializeField] GameObject bottonActivePause;
    [SerializeField] Image pauseOn, pauseOff;

    [Header("Coins")]
    public int coinsScore;
    [SerializeField] GameObject imageCoinMainMenu;
    [SerializeField] TextMeshProUGUI textCoins, textCoinsSelecton;

    [Header("Snake")]
    public bool snakeGame;
    [SerializeField] Canvas canvasSnake;
    [SerializeField] GameObject scenarySnake;

    [Header("Fuck Humans")]
    public bool fuckHumanGame;
    [SerializeField] GameObject scenaryFuckHuman;
    [SerializeField] Canvas canvasFuckHumans;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        coinsScore = PlayerPrefs.GetInt("CoinsBuy", 0);

        //////////// MENUS ///////////////
        mainMenu = true;
        settings = false;
        selectionGames = false;

        ///////// GAMES //////////
        snakeGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        ControlCanvas();
        SelectGame();
        TextCoinsScore();
        Games();
    }
    
    /// CONTROL DE LAS PANTALLAS ACTIVAS 
    private void ControlCanvas()
    {
        canvasMenus[0].enabled = mainMenu;
        canvasMenus[1].enabled = settings;
        canvasMenus[2].enabled = selectionGames;
    }
    //
    // BOTON SELECCION DE JUEGOS
    public void PulseBottonSelectionGames() 
    {
        selectionGames = !selectionGames;
        mainMenu = false;
        settings = false;
        Debug.Log("Botton Selection Games");
    }
    //
    // BOTON MENU PRINCIPAL
    public void PulseBottonMainMenu()
    {
        mainMenu = !mainMenu;
        selectionGames = false;
        settings = false;
        Debug.Log("Botton Main Menu");
    }
    //
    // BOTON CONFIGURACIONES
    public void PulseBottonSettings() 
    {
        settings = !settings;
        selectionGames = false;
        mainMenu = false;

        Debug.Log("Botton Settings");
    }
    //
    // BOTON SALIT DEL JUEGO
    public void pulseBottonExitGame()
    {
        Application.Quit();
        Debug.Log("Botton Exit Game");
    }
    //
    // MONEDAS PARA COMPRAR NUEVOS JUEGOS
    private void TextCoinsScore()
    {
        textCoins.enabled = mainMenu;
        textCoins.text = "Coins:" + coinsScore.ToString();
        
        textCoinsSelecton.enabled = selectionGames;
        textCoinsSelecton.text = "Coins: " + coinsScore;
    }
    //

    ////////////  JUEGOS A SELECCIONAR /////////////////////
    
    /// COMPORTAMIENTO DE LOS BOTONES DE SELECCION
    private void SelectGame()
    {
        if(selectionGames == true)
        {
            if(gamesPlay < 0)
            {
                gamesPlay = 1;
            }else if(gamesPlay > 1)
            {
                gamesPlay = 0;
            }

            switch (gamesPlay)
            {
                // juego Fuck Humans
                case 1:
                    gamePlayBottons[0].GetComponent<Image>().color = Color.white;
                    gamePlayBottons[0].GetComponent<RectTransform>().localScale = bottonsOff;
                    gamePlayBottons[0].GetComponent<RectTransform>().localPosition = new Vector3(-200, 0, 0);
       
                    gamePlayBottons[1].GetComponent<Image>().color = Color.red;
                    gamePlayBottons[1].GetComponent<RectTransform>().localScale = bottonOn;
                    gamePlayBottons[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    break;

                // Juego de la viborita
                default:
                    gamePlayBottons[0].GetComponent<Image>().color = Color.red;
                    gamePlayBottons[0].GetComponent<RectTransform>().localScale = bottonOn;
                    gamePlayBottons[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


                    gamePlayBottons[1].GetComponent<Image>().color = Color.white;
                    gamePlayBottons[1].GetComponent<RectTransform>().localScale = bottonsOff;
                    gamePlayBottons[1].GetComponent<RectTransform>().localPosition = new Vector3(200, 0, 0);
                    break;
            }

        }
    }  
    //

    ///  SUMAR JUEGO
    public void AddGame()
    {
        gamesPlay++;
    }
    //

    /// RESTAR JUEGO
    public void SubtractGame()
    {
        gamesPlay--;
    }
    //

   ///////////////// JUEGOS A ACTIVAR /////////////////////// 
    private void Games()
    {
        if(snakeGame == true)
        {
            fuckHumanGame = false;
            scenarySnake.SetActive(true);
            canvasSnake.enabled = true; 
            PauseGames();
        }
        else
        {
            scenarySnake.SetActive(false);
            canvasSnake.enabled = false;
        }

        if(fuckHumanGame == true)
        {
            snakeGame = false;
            canvasFuckHumans.enabled = true;
            scenaryFuckHuman.SetActive(true);
            PauseGames();
        }
        else
        {
            canvasFuckHumans.enabled = false;
            scenaryFuckHuman.SetActive(false);
        }
    }
   ////////////////////////////////////////////////
    
    // CONTROL AL PAUSAR JUEGO
    private void PauseGames()
    {        
        if(pause == true)
        {
            pauseOn.enabled = true;
            pauseOff.enabled = false;
            Time.timeScale = 0;
        }
        else
        {

            pauseOn.enabled = false;
            pauseOff.enabled = true;
            Time.timeScale = 1;
        }
    }
    //
    // BOTON PAUSAR JUEGO
    public void PausarGame()
    {
        pause = !pause;
    }
    //
}
