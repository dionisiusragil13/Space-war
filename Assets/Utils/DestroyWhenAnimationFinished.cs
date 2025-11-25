using System.Collections;
using UnityEngine;

public class DestroyWhenAnimationFinished : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        //Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);

        StartCoroutine("Deactivate");
    }
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
