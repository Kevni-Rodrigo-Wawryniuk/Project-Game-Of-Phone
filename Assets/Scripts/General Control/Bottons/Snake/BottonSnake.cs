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
    public Vector2 moveX, moveY;

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

        Snake.snake.rigth = true;
        Snake.snake.left = false;
    }
    public void PulseBottonLeft()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }
        Snake.snake.rigth = false;
        Snake.snake.left = true;

    }
    public void PulseBottonDown()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }

        Snake.snake.up = false;
        Snake.snake.down = true;
    }
    public void PulseBottonUp()
    {
        if (Snake.snake.move == false)
        {
            Snake.snake.moveSnake = 1;
        }
        Snake.snake.up = true;
        Snake.snake.down = false;
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

                moveX = new Vector2(moveDirection.x, 0);
                moveY = new Vector2(0,moveDirection.y);
                
                // Esta es otra forma de cambiar la dierccion del jugador
                Snake.snake.rigth = moveDirection.x > 0;
                Snake.snake.left = moveDirection.x < 0;
                Snake.snake.up = moveDirection.y > 0;
                Snake.snake.down = moveDirection.y < 0;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                Vector2 touchPosition = touch.deltaPosition;

                Vector2 moveDirection = new Vector2(touchPosition.x, touchPosition.y);

                Snake.snake.rigth = moveDirection.x > 0;
                Snake.snake.left = moveDirection.x < 0;
                Snake.snake.up = moveDirection.y > 0;
                Snake.snake.down = moveDirection.y < 0;
            }
        }
    }

}
