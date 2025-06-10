using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public Transform muzzleSpawnPosition;
    public float destroyTime = 1f;

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
        PlayerShoot();
    }

    void PlayerMove()
    {
        var xPos = Input.GetAxis("Horizontal");
        var yPos = Input.GetAxis("Vertical");
        var movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMuzzleFlash();
            SpawnMissile();
        }
    }

    void SpawnMuzzleFlash()
    {
        var muzzleObject = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
        muzzleObject.transform.SetParent(null);
        Destroy(muzzleObject, destroyTime);
    }

    void SpawnMissile()
    {
        var missileObject = Instantiate(missile, missileSpawnPosition);
        missileObject.transform.SetParent(null);
        Destroy(missileObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Star")
        {
            Destroy(this.gameObject);
        }
    }
}