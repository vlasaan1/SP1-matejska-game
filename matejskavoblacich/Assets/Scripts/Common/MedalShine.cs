using UnityEngine;

///<summary>
/// Makes medals shine - randomly chooses shine and plays animation
///</summary>
public class MedalShine : MonoBehaviour
{
    [SerializeField] float minTimeBetweenShines;
    [SerializeField] float maxTimeBetweenShines;

    Animator animator;
    float nextShine;
    readonly int animationVariantCount = 4;

    void Start()
    {
        nextShine = Time.time + Random.Range(minTimeBetweenShines,maxTimeBetweenShines);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time > nextShine){
            nextShine = Time.time + Random.Range(minTimeBetweenShines,maxTimeBetweenShines);
            animator.SetInteger("ShineVariant",Random.Range(0,animationVariantCount));
            animator.SetTrigger("Shine");
        }
    }
}
