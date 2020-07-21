using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    Sprite greenRock, redRock, whiteRock;
    // Start is called before the first frame update
    void Start() {
        switch (Random.Range(1, 4)) {
            case 1:
                GetComponent<SpriteRenderer>().sprite = greenRock;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = redRock;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = whiteRock;
                break;
        }
    }

    public void Initialize(Direction direction, Vector3 position) {
        // position of the asteroid
        transform.position = position;
        
        // apply impulse force to get game object moving
        float angle = Random.Range(0, 30 * Mathf.Deg2Rad);
        
        switch (direction)
        {
            case Direction.Up:
                angle += 75 * Mathf.Deg2Rad;
            break;

            case Direction.Left:
                angle += 165 * Mathf.Deg2Rad;
            break;

            case Direction.Down:
                angle += 255 * Mathf.Deg2Rad;
            break;

            case Direction.Right:
                angle += 345 * Mathf.Deg2Rad;
            break;
        }
        StartMoving(angle);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            

            // Instantiate twice Asteroids
            if (transform.localScale.x == 1)
            {
                //Re-Scale the Asteroid for the half
                Vector3 scale = transform.localScale;
                scale /= 2;
                transform.localScale = scale;
                GetComponent<CircleCollider2D>().radius /= 2;

                TwiceAsteroid();
            }

            //Sound and Destroy Objects
            AudioManager.Play(AudioClipName.Explosion);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void StartMoving(float angle) {
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 2f;
        
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }
    void TwiceAsteroid() {
        for (int i = 0; i < 2; i++)
        {
            GameObject asteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
            float angle = Random.Range(0, 2 * Mathf.PI);
            asteroid.GetComponent<Asteroid>().StartMoving(angle);
        }
    }
}