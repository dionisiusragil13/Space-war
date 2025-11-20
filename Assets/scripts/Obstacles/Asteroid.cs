using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int lives;
    private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        rb = GetComponent<Rigidbody2D>();
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1);
        rb.linearVelocity = new Vector2(pushX, pushY);
        float randomScale = Random.Range(0.6f,1f);
        transform.localScale = new Vector2(randomScale,randomScale);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage)
    {
        spriteRenderer.material = whiteMaterial;
        StartCoroutine("ResetMaterial");
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        lives-=damage;
        if (lives <= 0)
        {
            Instantiate(destroyEffect,transform.position,transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            Destroy(gameObject);
        }
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }
}
