using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX,0);
        if(transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
}
