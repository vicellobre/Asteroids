using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour {

    [SerializeField]
    GameObject prefabBullet, HUD;

    // thrust and rotation support
    Rigidbody2D rb2D;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start() {
        // saved for efficiency
        rb2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
            AudioManager.Play(AudioClipName.Shoot);
        }
    }

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate() {
        // thrust as appropriate
        if (Input.GetAxis("Thrust") != 0) {
            rb2D.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.GameOver);
            HUD.GetComponent<HUD>().StopGameTimer();
            Destroy(gameObject);
        }
    }
}