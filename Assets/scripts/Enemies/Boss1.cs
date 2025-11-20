using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private float speedX;
    private float speedY;
    private bool charging;
    private float switchInterval;
    private float switchTimer;
    private int lives;
    void Start()
    {
        animator = GetComponent<Animator>();
        EnterChargeState();
        lives = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTimer > 0 )
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (charging)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }
        if (transform.position.y >3 || transform.position.y < -3)
        {
            speedY *= -1;
        }
        float moveX = speedX * Time.deltaTime;
        float moveY = speedY * Time.deltaTime;

        transform.position += new Vector3(moveX,moveY);
        if(transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
    void EnterPatrolState()
    {
        speedX = 0;
        speedY = Random.Range(-2f,2f);
        switchInterval = Random.Range(5f,10f);
        switchTimer = switchInterval;
        charging = false;
        animator.SetBool("Charging",false);
    }
    void EnterChargeState()
    {
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(2f,2.5f);
        switchTimer = switchInterval;
        charging = true;
        animator.SetBool("Charging",true);
        AudioManager.Instance.PlaySound(AudioManager.Instance.bossCharge);
    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.hitArmor);
        lives -=damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(0);
        }
    }
}
