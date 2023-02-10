using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{

    // Variable to store the radius of the Circle2DCollider
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;
    }
    void OnBecameInvisible()
    {      
        Vector2 position = transform.position;

        if (position.x -radius > ScreenUtils.ScreenRight || position.x + radius < ScreenUtils.ScreenLeft)
        {
            position.x = -position.x;
        }
        if (position.y - radius > ScreenUtils.ScreenTop || position.y + radius < ScreenUtils.ScreenBottom)
        {
            position.y = -position.y;
        }

        transform.position = position;
    }
}
