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

    // parametros para la dreccion
    [SerializeField] Vector2 fingerUp, fingerDown;
    // la distancia del desliz para determinar la direccion
    [SerializeField] float minDistance;
    // Evento que se activa cuando determina el parametro de direccion
    public static event System.Action<Vector2> onSwipe;

    private void Start()
    {
        if(bottonSnake == null)
        {
            bottonSnake = this;
        }
    }

    /// /////////////////////////////////// ESTO TAMBIEN SE PUEDE USAR EN BOTONES ////////////////////////////////////

    // Esto es para pulsar los botones en pantalla para mover a la vibora

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
        DetectSwipe();
    }

 
    ///////////////////////////////////// DIRECCIONES DETERMINADAS ////////////////////////////////////////
  
    // Esto es para el juego de la viborita se usaria para direcciones ya determinadas
    // osea solo moverte entre cuatro direcciones que son arriba, abajo, izquierda, derecha
    private void DetectSwipe()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                fingerDown = touch.position;
                fingerUp = touch.position;
            }

            if(touch.phase == TouchPhase.Moved)
            {
                fingerUp = touch.position;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                fingerUp = touch.position;
                CheckSwipe();
            }
        }
    }
    void CheckSwipe()
    {
        // darle los valores del touch a una variable
        Vector2 swipeDelta = fingerUp - fingerDown;

        // la magnitud del desliz del dedo debe ser mayor a la distancia minima
        if (swipeDelta.magnitude > minDistance)
        {
            // normalizar la maginitud del desliz
            swipeDelta.Normalize();

            // detectar el angulo de desplazamiento
            float angel = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;

            // convertir el angulo en un valor positivo en un rango de 0-360 grados
            if (angel < 0) {
                angel += 360;
            }

            // determinar la direccion en funcion del angulo Se puede usar un enum para esto como en el juego de la snake

            // Derecha
            if (angel < 45f || angel > 315f)
            {
                Debug.Log("derecha 0");
       
            }
            // Arriba
            else if (angel > 45f && angel < 135f)
            {
                Debug.Log("arriba 1");
   
            }
            // izquierda
            else if (angel > 135f && angel < 225f)
            {
                Debug.Log("izquierda 2");

            }
            // Abajo
            else
            {
                Debug.Log("abajo 4");
     
            }

            // llamamos al evento onSwipe y pasa la direccion del deslizamiento a un parametro
            onSwipe?.Invoke(swipeDelta);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
}
