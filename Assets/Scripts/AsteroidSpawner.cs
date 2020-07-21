using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    float radius;

    // Start is called before the first frame update
    void Start()
    {
        GameObject asteroid = Instantiate(prefabAsteroid);
        radius = asteroid.GetComponent<CircleCollider2D>().radius;
        Destroy(asteroid);

        CreateAsteroid(Direction.Left);
        CreateAsteroid(Direction.Up);
        CreateAsteroid(Direction.Right);
        CreateAsteroid(Direction.Down);
    }

    void CreateAsteroid(Direction direction) {
        float point;
        Vector3 position = Vector3.zero;

        switch (direction)
        {
            case Direction.Up:
                point = ScreenUtils.ScreenBottom - radius;
                position = new Vector3(0, point, 1);
            break;

            case Direction.Down:
                point = ScreenUtils.ScreenTop + radius;
                position = new Vector3(0, point, 1);
            break;

            case Direction.Right:
                 point = ScreenUtils.ScreenLeft - radius;
                 position = new Vector3(point, 0, 1);
            break;

            case Direction.Left:
                 point = ScreenUtils.ScreenRight + radius;
                 position = new Vector3(point, 0, 1);
            break;
        }
        GameObject asteroid = Instantiate(prefabAsteroid);
        asteroid.GetComponent<Asteroid>().Initialize(direction, position);
    }
}
