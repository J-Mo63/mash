using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxMovement;

    private void Update()
    {
        var position = transform.position;
        if (position.y >= maxMovement)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(position, new Vector2(position.x, maxMovement), 
            speed * Time.deltaTime);
    }
}
