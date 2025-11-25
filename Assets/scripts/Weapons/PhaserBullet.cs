using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    PhaserWeapon weapon;
    void Start()
    {
        weapon = PhaserWeapon.Instance;
    }
    void Update()
    {
        transform.position += new Vector3(weapon.stats[PhaserWeapon.Instance.weaponLevel].speed * Time.deltaTime, 0f);
        if (transform.position.x > 9.5)
        {
            gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if(asteroid) asteroid.TakeDamage(weapon.stats[PhaserWeapon.Instance.weaponLevel].damage, true);
            gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if(boss1) boss1.TakeDamage(weapon.stats[PhaserWeapon.Instance.weaponLevel].damage);
            gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("Critter"))
        {
            gameObject.SetActive(false);
        }
    }
}
