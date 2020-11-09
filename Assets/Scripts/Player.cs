using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float fuel;
    public Image FuelBar; //barra de combustível que irá diminuir ao longo do uso
    public static bool pressed;

    void Start()
    {
      fuel = 1;
      pressed = false;
    }

//checa se está sendo pressionado a tela e remove combustível
    void Update()
    {
      Debug.Log(fuel);
      if (pressed)
      {
        fuel -= 0.001f;
        FuelBar.fillAmount = Mathf.MoveTowards(FuelBar.fillAmount,fuel,Time.deltaTime/10);
      }

    }
    // colisão com gasolina e acrescenta gasolina
    void OnCollisionEnter2D(Collision2D col)
    {
      if (col.gameObject.tag == "Fuel")
      {
        fuel += 0.5f;
        if (fuel > 1)
        {
          fuel = 1;
        }
        Destroy(col.gameObject);
      }
    }
}
