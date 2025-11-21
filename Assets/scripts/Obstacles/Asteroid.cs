using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    [SerializeField] private GameObject destroyEffect;
    private int lives;
    private int damage;
    private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        rb = GetComponent<Rigidbody2D>();
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);
        rb.linearVelocity = new Vector2(pushX, pushY);
        float randomScale = Random.Range(0.6f,1f);
        transform.localScale = new Vector2(randomScale,randomScale);
        lives = 5;
        damage = 1;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if(player) player.TakeDamage(damage);
        }
    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        flashWhite.Flash();
        lives-=damage;
        if (lives <= 0)
        {
            Instantiate(destroyEffect,transform.position,transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            Destroy(gameObject);
        }
    }

}
