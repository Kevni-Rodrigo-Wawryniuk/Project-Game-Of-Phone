using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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

    [Header("Pause")]
    public bool pause;
    [SerializeField] GameObject bottonActivePause;
    [SerializeField] Image pauseOn, pauseOff;

    [Header("Snake")]
    public bool snakeGame;
    [SerializeField] Canvas canvasSnake;
    [SerializeField] GameObject scenarySnake;

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
        Games();
        ControlCanvas();
        SelectGame();
    }
    
    /// CONTROL DE LAS PANTALLAS ACTIVAS 
    private void ControlCanvas()
    {
        canvasMenus[0].enabled = mainMenu;
        canvasMenus[1].enabled = settings;
        canvasMenus[2].enabled = selectionGames;
    }

    public void PulseBottonSelectionGames() 
    {
        selectionGames = !selectionGames;
        mainMenu = false;
        settings = false;
        Debug.Log("Botton Selection Games");
    }
    public void PulseBottonMainMenu()
    {
        mainMenu = !mainMenu;
        selectionGames = false;
        settings = false;
        Debug.Log("Botton Main Menu");
    }
    public void PulseBottonSettings() 
    {
        settings = !settings;
        selectionGames = false;
        mainMenu = false;

        Debug.Log("Botton Settings");
    }
    public void pulseBottonExitGame()
    {
        Application.Quit();
        Debug.Log("Botton Exit Game");
    }


    ////////////  JUEGOS A SELECCIONAR /////////////////////
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
                    gamePlayBottons[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    gamePlayBottons[0].GetComponent<RectTransform>().localPosition = new Vector3(-200, 0, 0);
       
                    gamePlayBottons[1].GetComponent<Image>().color = Color.red;
                    gamePlayBottons[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    gamePlayBottons[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    break;

                // Juego de la viborita
                default:
                    gamePlayBottons[0].GetComponent<Image>().color = Color.white;
                    gamePlayBottons[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    gamePlayBottons[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


                    gamePlayBottons[1].GetComponent<Image>().color = Color.red;
                    gamePlayBottons[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    gamePlayBottons[1].GetComponent<RectTransform>().localPosition = new Vector3(200, 0, 0);
                    break;
            }

        }
    }  

    ///  SUMAR JUEGO
    public void AddGame()
    {
        gamesPlay++;
    }
    
    /// RESTAR JUEGO
    public void SubtractGame()
    {
        gamesPlay--;
    }

   ///////////////// JUEGOS A ACTIVAR /////////////////////// 
    private void Games()
    {
        // Esto es para la imagen que se muestra cuando el juego se pone en pausa
        pauseOn.enabled = pause;
        pauseOff.enabled = !pause;

        // El juego de la viboita
        if (snakeGame == true)
        {
            canvasSnake.enabled = true;
            scenarySnake.SetActive(true);
            PauseGames();
            
            if(Snake.snake.move == true && Snake.snake.deadSnake == false)
            {
                bottonActivePause.SetActive(true);
            }
            else
            {
                bottonActivePause.SetActive(false);
            }
        }
        else
        {
            canvasSnake.enabled = false;
            scenarySnake.SetActive(false);
        }
    }
   ////////////////////////////////////////////////
    
    // BOTTONS PAUSE
    private void PauseGames()
    {        
        if(pause == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void PausarGame()
    {
        pause = !pause;
    }

}
