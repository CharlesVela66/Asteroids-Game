using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    GameObject HUD;

    // Variable to store the rigidbody component of the object
    Rigidbody2D rb2d;

    // Variable to store the direction of the thrust
    Vector3 thrustDirection = new Vector3(1f, 0f, 0f);

    // Constant to store the value of the thrust
    const int ThrustForce = 10;

    // Variable to store the value of rotation
    float rotateDegreesPerSecond = 175;


    // Start is called before the first frame update
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0f)
        {
            // calculate rotation amount and apply rotation 
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            float rotateZ = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(rotateZ);
            thrustDirection.y = Mathf.Sin(rotateZ);

        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // We add the shooting sound effect
            AudioManager.Play(AudioClipName.PlayerShot);

            // We instantiate the bullet and we apply a force to it
            GameObject bulletObject = Instantiate<GameObject>(prefabBullet, transform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    // Calculations for the physics of the ship
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Asteroid"))
        {
            // We stop the timer
            HUD.GetComponent<HUD>().StopGameTimer();

            // We add the shooting sound effect
            AudioManager.Play(AudioClipName.PlayerDeath);

            // We destroy the game object
            Destroy(gameObject);
        }
    }
}
