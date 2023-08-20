using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BottonsGamesSelections : MonoBehaviour
{
    public static BottonsGamesSelections bottonsGamesSelections;

    [Header("Game Actives")]
    public bool gameSnake;
    public bool gameFuckHumans;

    [Header("Buy Game")]
    [SerializeField] int buyFuckHumanGame;

    private void Start()
    {
        StartProgram();
    }
    void StartProgram()
    {
        if(bottonsGamesSelections == null)
        {
            bottonsGamesSelections = this;
        }

      buyFuckHumanGame = PlayerPrefs.GetInt("FuckHuman", 0);
    }

    private void Update()
    {
        GameASelection();
    }
    // juego a seleccionar
    public void GameASelection()
    {
        if (GameManager.gameManager.gamesPlay == 0)
        {
            gameSnake = true;
            gameFuckHumans = false;
        }
        else if (GameManager.gameManager.gamesPlay == 1)
        {
            gameFuckHumans = true;
            gameSnake = false;
        }
    }


    // el juego de la viborita
    public void BottonGameSnake()
    {
        if (GameManager.gameManager.gamesPlay == 0)
        {
            GameManager.gameManager.mainMenu = false;
            GameManager.gameManager.settings = false;
            GameManager.gameManager.selectionGames = false;

            GameManager.gameManager.snakeGame = true;
            GameManager.gameManager.fuckHumanGame = false;
            Debug.Log("Jugar al juego de la viborita");
        }
    }
    //

    // juego de la nave lanza humanos
    // jugar juego de la nave
    public void BottonGameFuckHumans()
    {
        if (GameManager.gameManager.gamesPlay == 1)
        {
            if (GameManager.gameManager.coinsScore >= 10 && buyFuckHumanGame == 0)
            {
                GameManager.gameManager.coinsScore -= 10;
                buyFuckHumanGame = 1;
                PlayerPrefs.SetInt("FuckHuman", buyFuckHumanGame);

                Debug.Log("has desbloqueado este juego");
            }
            else if (GameManager.gameManager.coinsScore < 10 && buyFuckHumanGame == 0)
            {
                Debug.Log("Requieres conseguir 10 monedas para desbloquear este juego");
            }

            if (buyFuckHumanGame >= 1)
            {
                Debug.Log("Jugar el juego de la nave");
                GameManager.gameManager.fuckHumanGame = true;
                GameManager.gameManager.snakeGame = false;

                GameManager.gameManager.mainMenu = false;
                GameManager.gameManager.settings = false;
                GameManager.gameManager.selectionGames = false;
            }
            else
            {
                Debug.Log("no puedes jugar");
            }
        }
    }
    //
}
