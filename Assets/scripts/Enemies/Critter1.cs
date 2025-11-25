using UnityEngine;

public class Critter1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private ObjectPooler zappedEffectPool;
    private ObjectPooler burnEffectPool;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float moveSpeed;
    private float moveTimer;
    private float moveInterval;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        zappedEffectPool = GameObject.Find("Critter1_ZappedPool").GetComponent<ObjectPooler>();
        burnEffectPool = GameObject.Find("Critter1_BurnPool").GetComponent<ObjectPooler>();
        moveSpeed = Random.Range(0.5f,2f);
        GenerateRandomPosition();
        moveInterval = Random.Range(0.5f,2f);
        moveTimer = moveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.5f,2f);
            moveTimer = moveInterval;
        }
        targetPosition -= new Vector3(GameManager.Instance.worldSpeed * Time.deltaTime ,0);
        transform.position = Vector3.MoveTowards(transform.position,targetPosition,moveSpeed* Time.deltaTime);
        Vector3 relativePos = targetPosition - transform.position;
        if(relativePos != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward,relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,1080 * Time.deltaTime);
        }

    } 
    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(-5f,5f);
        float randomY = Random.Range(-5f,5f);
        targetPosition =new  Vector2(randomX,randomY);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject zappedEffect = zappedEffectPool.GetPooledObject();
            zappedEffect.transform.position = transform.position;
            zappedEffect.transform.rotation = transform.rotation;
            zappedEffect.SetActive(true);
            //Instantiate(zappedEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.squished);
            GameManager.Instance.critterCounter ++;
        } else if (collision.gameObject.CompareTag("Player"))
        {
            GameObject burnEffect = burnEffectPool.GetPooledObject();
            burnEffect.transform.position = transform.position;
            burnEffect.transform.rotation = transform.rotation;
            burnEffect.SetActive(true);
            //Instantiate(burnEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
            GameManager.Instance.critterCounter ++;
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.burn);
        }
    }
}
