using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float timeLife = 1f;

    Timer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = timeLife;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    //Apply force to the direction give
    public void ApplyForce(Vector2 directionForce) {
        const float MagnitudeForce = 10f;
        GetComponent<Rigidbody2D>().AddForce(directionForce * MagnitudeForce, ForceMode2D.Impulse);
    }
}
