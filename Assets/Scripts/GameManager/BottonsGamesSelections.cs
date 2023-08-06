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

    private void Update()
    {
        GameASelection();
    }
    // juego a seleccionar
    public void GameASelection()
    {
        if(GameManager.gameManager.gamesPlay == 0)
        {
            gameSnake = true;
            gameFuckHumans = false;
        }
        else if(GameManager.gameManager.gamesPlay == 1)
        {
            gameFuckHumans = true;
            gameSnake = false; 
        }
    }
    // el juego de la viborita
    public void BottonGameSnake()
    {
        if (gameSnake == true)
        {
            GameManager.gameManager.mainMenu = false;
            GameManager.gameManager.settings = false;
            GameManager.gameManager.selectionGames = false;
            GameManager.gameManager.snakeGame = true;
            Debug.Log("Jugar al juego de la viborita");
        }
    }
    public void BottonGameFuckHumans()
    {
        if (gameFuckHumans == true)
        {
            Debug.Log("Probando Boton");
        }
    }
}
