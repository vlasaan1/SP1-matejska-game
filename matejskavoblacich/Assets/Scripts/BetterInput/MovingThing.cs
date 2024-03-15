using UnityEngine;

public class MovingThing : BaseHoldable
{
    [SerializeField] protected Collider2D baseCollider;
    [SerializeField, Tooltip("Increase size of this if object is often left behind during moving")] protected Collider2D movementCollider;
    [HideInInspector] public bool isHeld = false;

    Vector3 moveDirection;
    float minMovement = 0.2f;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;

    public void Awake(){
        movementCollider.enabled = false;
    }
    protected override void OnHold(Vector2 hitPosition){
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
            //baseCollider.enabled = false;
            movementCollider.enabled = true;
            lastFrameCount = Time.frameCount;
        //Do not set movement variables twice in same frame for hits that hit more than one collider on this object
        } else if(lastFrameCount != Time.frameCount) {
            //Is already holding -> move 
            moveDirection = (Vector3)hitPosition - transform.position;
            //Stop jittering due to input accuracy
            if(moveDirection.magnitude < minMovement){
                moveDirection = Vector3.zero;
            }

            deltaFrame = Time.frameCount - lastFrameCount;
            lastFrameCount = Time.frameCount;
            currentMovingFrame = 0;
        }
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        isHeld = false;
        moveDirection = Vector3.zero;
        movementCollider.enabled = false;
        baseCollider.enabled = true;
    }

    public void Update(){
        if(currentMovingFrame<=deltaFrame){
            transform.Translate(moveDirection/deltaFrame);
            currentMovingFrame++;
        }
    }
}
