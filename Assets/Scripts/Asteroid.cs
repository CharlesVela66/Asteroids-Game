using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite spriteAsteroid1;
    [SerializeField]
    Sprite spriteAsteroid2;
    [SerializeField]
    Sprite spriteAsteroid3;

    SpriteRenderer spriteR;


    // Start is called before the first frame update
    void Start()
    {

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        int randomPicker = Random.Range(0, 3);
        if (randomPicker == 0) 
        {
            spriteR.sprite = spriteAsteroid1;
        }
        else if (randomPicker == 1)
        {
            spriteR.sprite = spriteAsteroid2;
        }
        else
        {
            spriteR.sprite = spriteAsteroid3;
        }
    }

    public void Initialize(Direction moveDirection, Vector3 location)
    {

        float angle = (Mathf.Deg2Rad) * Random.Range(-15, 16);
        if (moveDirection == Direction.Left)
        {
            StartMoving(180 + angle);
        }
        else if (moveDirection == Direction.Right)
        {
            StartMoving(360 + angle);
        }
        else if (moveDirection == Direction.Up)
        {
            StartMoving(90 + angle);
        }
        else
        {
            StartMoving(270 + angle);
        }

        transform.position = location;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // We add the asteroid hit sound effect
            AudioManager.Play(AudioClipName.AsteroidHit);

            // We set the localScale to a variable
            Vector3 scale = transform.localScale;

            // Destroy the bullet
            Destroy(collision.gameObject);

            // If the asteroid has already being cut in half, destroy it
            if (scale.x == 0.5)
            {
                Destroy(gameObject);
            }

            else
            {
                // Cut the asteroid in half, both in x and y
                scale.x /= 2;
                scale.y /= 2;
                transform.localScale = scale;

                // Cut the collider in half
                float radius = GetComponent<CircleCollider2D>().radius;
                radius /= 2;
                GetComponent<CircleCollider2D>().radius = radius;

                // Instantiating both asteroids and destroying the original one
                GameObject asteroid1 = Instantiate(gameObject);
                GameObject asteroid2 = Instantiate(gameObject);
                Destroy(gameObject);

                // Setting a direction and velocity to the asteroids
                float rand1 = Random.Range(0, 360);
                float rand2 = Random.Range(0, 360);
                asteroid1.GetComponent<Asteroid>().StartMoving(rand1);
                asteroid2.GetComponent<Asteroid>().StartMoving(rand2);
            }
        }
    }

    private void StartMoving(float angle)
    {
        // Declaration of constants
        const float minimumForce = 1.0f;
        const float maximumForce = 3.0f;

        // We set a random value for our magnitude within the range
        float magnitude = Random.Range(minimumForce, maximumForce);

        // We set the direction and we save it in a variable 
        Vector2 direction;
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        // We add the impulse force to the gameObject
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
    }
}
