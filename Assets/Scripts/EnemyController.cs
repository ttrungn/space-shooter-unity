using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        _rb.velocity = Vector2.down * speed;
    }
}
