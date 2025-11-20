using UnityEngine;

public class Boom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX,0);
    }

}
