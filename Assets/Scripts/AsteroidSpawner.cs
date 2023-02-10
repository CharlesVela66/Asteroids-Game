using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// Script that spawns the asteroids in the game
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    float radius;
    // Start is called before the first frame update
    void Start()
    {
        int numOfAsteroids = 10;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < numOfAsteroids; i++)
        {
            GameObject asteroidObject = Instantiate<GameObject>(prefabAsteroid);
            list.Add(asteroidObject);
        }
        for (int i =0; i < numOfAsteroids; i++)
        {
            Asteroid asteroid = list[i].GetComponent<Asteroid>();
            radius = list[i].GetComponent<CircleCollider2D>().radius;

            int numberPicker = Random.Range(0,4);
            if (numberPicker == 0) 
            {
                asteroid.Initialize(Direction.Left, new Vector3(ScreenUtils.ScreenRight + radius, 0, 0));
            }
            else if (numberPicker == 1)
            {
                asteroid.Initialize(Direction.Right, new Vector3(ScreenUtils.ScreenLeft - radius, 0, 0));
            }
            else if (numberPicker == 2)
            {
                asteroid.Initialize(Direction.Up, new Vector3(0, ScreenUtils.ScreenBottom - radius, 0));
            }
            else
            {
                asteroid.Initialize(Direction.Down, new Vector3(0, ScreenUtils.ScreenTop + radius, 0));
            }
        }

    }

}
