using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ControlCanvasSnake : MonoBehaviour
{
    public static ControlCanvasSnake controlCanvasSnake;

    [Header("Screem Snake")]
    public bool ScreemSnake;
    [SerializeField] Canvas canvasSnake;
    [SerializeField] TextMeshProUGUI textPoints, lastPointsPlay, lastScore, pauseText;
    [SerializeField] GameObject bottonReturnSnake, bottonOutGameSnake;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram()
    {
        if(controlCanvasSnake == null)
        {
            controlCanvasSnake = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        CanvasActive();
    }

    public void CanvasActive()
    {
        canvasSnake.enabled = ScreemSnake;

        if (GameManager.gameManager.snakeGame == true)
        {

            if (Snake.snake.move == false && Snake.snake.deadSnake == false)
            {
                PointStart();
                BottonReturnAndOutGameDesactivate();
                pauseText.enabled = false;
            }
            // Cuando se esta jugando
            if (Snake.snake.move == true && Snake.snake.deadSnake == false)
            {

                lastScore.text = "Last Score: " + Snake.snake.lastScore;

                PointsCount();
                BottonReturnAndOutGameDesactivate();
                if (GameManager.gameManager.pause == true)
                {
                    pauseText.enabled = true;
                    BottonReturnAndOutGameActive();
                }
                else
                {
                    pauseText.enabled = false;
                }
            }
            // cuando se muere la serpiente
            if (Snake.snake.move == false && Snake.snake.deadSnake == true)
            {
                LastPoints();
                BottonReturnAndOutGameActive();
                pauseText.enabled = false;
            }
        }
    }

    private void PointStart()
    {
        ScreemSnake = false;
        textPoints.enabled = false;
        lastPointsPlay.enabled = false;
        lastScore.enabled = false;
    }
    private void PointsCount()
    {
        ScreemSnake = true;
        textPoints.enabled = true;
        textPoints.text = "Points: " + Snake.snake.pointScreem;
        lastPointsPlay.enabled = false;
        lastScore.enabled = true;
    }
    private void LastPoints()
    {
        ScreemSnake = true;
        textPoints.enabled = false;
        lastPointsPlay.enabled = true;
        lastPointsPlay.text = "Last Points Play: " + Snake.snake.lastPointsPlay;
        lastScore.enabled = true;
    }

    // Botones de volver a jugar y salir del juego
    // Botones activos
    private void BottonReturnAndOutGameActive()
    {
        bottonReturnSnake.SetActive(true);
        bottonOutGameSnake.SetActive(true);
    }
    // botones desactivos
    private void BottonReturnAndOutGameDesactivate()
    {
        bottonReturnSnake.SetActive(false);
        bottonOutGameSnake.SetActive(false);
    }
}
