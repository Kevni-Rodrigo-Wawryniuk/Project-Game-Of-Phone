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
    [SerializeField] TextMeshProUGUI TextGuia;

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
        
    }
}
