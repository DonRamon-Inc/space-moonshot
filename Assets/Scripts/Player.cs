using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float fuel;
    public Image FuelBar; //barra de combustível que irá diminuir ao longo do uso
    public static bool pressed;
    public GameObject map;
    private const float OnMapEndReachedThreshold = 0.1f;

    void Start()
    {
      fuel = 1;
      pressed = false;
    }

//checa se está sendo pressionado a tela e remove combustível
    void Update()
    {
      // Debug.Log(fuel);
      if (pressed)
      {
        fuel -= 0.001f;
        FuelBar.fillAmount = Mathf.MoveTowards(FuelBar.fillAmount,fuel,Time.deltaTime/10);
      }

        HandleMapBoundaries();

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

    void HandleMapBoundaries()
    {
        float mapWidth = map.GetComponent<SpriteRenderer>().bounds.size.x;
        float mapHeight = map.GetComponent<SpriteRenderer>().bounds.size.y;

        float halfMapWidth = mapWidth / 2;
        float[] xBoundaries = { -halfMapWidth, halfMapWidth };

        float halfMapHeight = mapHeight / 2;
        float[] yBoundaries = { -halfMapHeight, halfMapHeight };

        Transform playerTransform = this.gameObject.transform;
        Vector3 playerTrajectory = new Vector3(
            playerTransform.position.x * OnMapEndReachedThreshold,
            playerTransform.position.y * OnMapEndReachedThreshold
        );
        Vector3 newPlayerPosition = new Vector3(playerTransform.position.x, playerTransform.position.y);

        if (playerTransform.position.x - playerTrajectory.x <= xBoundaries[0])
        {
            newPlayerPosition.x = xBoundaries[1] - 1;
        }
        else if (playerTransform.position.x + playerTrajectory.x >= xBoundaries[1])
        {
            newPlayerPosition.x = xBoundaries[0] + 1;
        }

        if (playerTransform.position.y - playerTrajectory.y <= yBoundaries[0])
        {
            newPlayerPosition.y = yBoundaries[1] - 1;
        }
        else if (playerTransform.position.y + playerTrajectory.y >= yBoundaries[1])
        {
            newPlayerPosition.y = yBoundaries[0] + 1;
        }

        playerTransform.position = newPlayerPosition;
    }
}
