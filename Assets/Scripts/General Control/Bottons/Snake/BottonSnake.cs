using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BottonSnake : MonoBehaviour
{
    public static BottonSnake bottonSnake;


    private void Start()
    {
        if(bottonSnake == null)
        {
            bottonSnake = this;
        }
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////
    /// </summary>
    // Esto es para pulsar los botones en pantalla para mover a la vibora
    public void PulseBottonRigth()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }

    }
    public void PulseBottonLeft()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }
    }
    public void PulseBottonDown()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }

    }
    public void PulseBottonUp()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////
    /// </summary>
   
    /////////////////////////////////// BOTTON RETURN GAME SNAKE   /////////////////////////////////////
    public void ReturnGameSnake()
    {
        Snake.snake.BottonReturnGame();
    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    // Esto es cuando se arrastre el dedo en la pantalla

    private void Update()
    {
        DragToTheRigth();
    }
    public void DragToTheRigth()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                touch.deltaPosition = Vector2.zero;
            }

            if(touch.phase == TouchPhase.Moved)
            {
                Vector2 touchoDeltaPosition = touch.deltaPosition;

                Vector2 moveDirection = new Vector2(touchoDeltaPosition.x, touchoDeltaPosition.y).normalized;
                
            }

            if(touch.phase == TouchPhase.Ended)
            {
                Vector2 touchPosition = touch.deltaPosition;

                Vector2 moveDirection = new Vector2(touchPosition.x, touchPosition.y);

                if (moveDirection.x > moveDirection.y)
                {
                    if (moveDirection.x > 0.5f)
                    {
                        Snake.snake.rigth = true;
                        Snake.snake.left = false;
                        Snake.snake.up = false;
                        Snake.snake.down = false;
                    }
                }
                if(-moveDirection.x < -moveDirection.y)
                {
                    if (moveDirection.x < -0.5f)
                    {
                        Snake.snake.rigth = false;
                        Snake.snake.left = true;
                        Snake.snake.up = false;
                        Snake.snake.down = false;
                    }
                }
                if (moveDirection.y < moveDirection.x)
                {
                    if (moveDirection.y > 0.5f)
                    {
                        Snake.snake.left = false;
                        Snake.snake.rigth = false;
                        Snake.snake.down = false;
                        Snake.snake.up = true;
                    }
                }
                if(-moveDirection.y < -moveDirection.x)
                {
                    if (moveDirection.y < -0.5f)
                    {
                        Snake.snake.left = false;
                        Snake.snake.rigth = false;
                        Snake.snake.down = true;
                        Snake.snake.up = false;
                    }
                }
            }
        }
    }

}
