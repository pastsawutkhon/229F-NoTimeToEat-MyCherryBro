using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    
        if (anim != null)
        {
            float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
        }
        
    }
}