using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowingThing : BaseHoldable
{
    [SerializeField] Collider2D baseCollider;
    [SerializeField, Tooltip("Increase size of this if object is often left behind during moving")] Collider2D movementCollider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float throwMultiplier = 3;


    [HideInInspector] public bool isHeld = false;

    Vector3 moveDirection;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;



    //TMP FOR TESTING
    [Header("Temporary for testing")]
    [SerializeField] TextMeshPro colliderSizeText;
    [SerializeField] TextMeshPro objectSizeText;
    [SerializeField] GameObject colliderVisualization;
    [SerializeField] TextMeshPro throwMultiplierText;
    [SerializeField] TextMeshPro maxTimeBetweenClicksText;
    CapsuleCollider2D capsuleColl;

    public void Awake(){
        movementCollider.enabled = false;
        capsuleColl = GetComponent<CapsuleCollider2D>();
        //maxTimeBetweenClicks = 0.2f;
    }
    protected override void OnHold(Vector2 hitPosition)
    {
        //Do not start holding new things when already holding something
        if(!isHeld){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,transform.localScale.x/2);
            foreach(Collider2D coll in colliders){
                if(coll.gameObject.GetInstanceID()==gameObject.GetInstanceID() || coll.gameObject.CompareTag("MissEffectCollider"))
                {
                    continue;
                }
                if(coll.gameObject.TryGetComponent<MovingThing>(out MovingThing other)){
                    if(other.isHeld){
                        return;
                    }
                } else if(coll.gameObject.TryGetComponent<ThrowingThing>(out ThrowingThing other2)){
                    if(other2.isHeld){
                        return;
                    }
                }
            }
            //Start holding
            isHeld = true;
            movementCollider.enabled = true;
            lastFrameCount = Time.frameCount;
            //Stop all physics based movement
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.rotation = 0;
            //Do not set movement variables twice in same frame for hits that hit both colliders
        } else if(lastFrameCount != Time.frameCount) {
            //Is already holding -> move 
            moveDirection = (Vector3)hitPosition - transform.position;
            deltaFrame = Time.frameCount - lastFrameCount;
            lastFrameCount = Time.frameCount;
            currentMovingFrame = 0;
        }
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        if(isHeld){
            isHeld = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(moveDirection*throwMultiplier,ForceMode2D.Impulse);
            moveDirection = Vector3.zero;
            movementCollider.enabled = false;
            baseCollider.enabled = true;
        }
    }


    public void Update(){
        if(currentMovingFrame<=deltaFrame){
            transform.Translate(moveDirection/deltaFrame);
            currentMovingFrame++;
        }

        maxTimeBetweenClicksText.text = maxTimeBetweenClicks.ToString();
        colliderSizeText.text = capsuleColl.size.x.ToString();
        colliderVisualization.transform.localScale = new Vector3(1,1,1)*capsuleColl.size.x;
        objectSizeText.text = transform.localScale.x.ToString();
        throwMultiplierText.text = throwMultiplier.ToString();
    }
}
