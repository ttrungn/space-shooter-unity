using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.InstantiateParticles(GameManager.instance.explosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
