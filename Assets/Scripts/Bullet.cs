using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    const float dur = 1.0f;
    Timer deathTimer;
    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = dur;
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
    /// <summary>
    /// Method that gives the direction for the force to apply
    /// </summary>
    /// <param name="direction"></param>
    public void ApplyForce(Vector2 direction)
    {
        const float magnitude = 25.0f;
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude,ForceMode2D.Impulse);
    }
}
