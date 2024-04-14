using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : BaseHoldable
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Minigame minigame;
    [SerializeField] Transform baseBallPosition;
    [SerializeField] Trajectory trajectory;
    [SerializeField] Strip strip;
    [SerializeField] float maxDraw = 0.9f;
    [SerializeField] float throwMultiplier = 12;
    [SerializeField] float returnBallDelay = .3f;
    [SerializeField] float nextBallPreparationDelay = .2f;

    Queue<Ball> readyBalls = new();
    Ball heldBall;
    Vector3 moveDirection;
    Vector3 finalDest;
    float minMovement = 0.1f;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;
    bool isHeld = false;
    bool nextBallPrepared = false;
    float timeToAddBall;

    void Start(){
        readyBalls.Enqueue(Instantiate(ballPrefab).GetComponent<Ball>());
        readyBalls.Peek().isInQueue = true;

        timeToAddBall = minigame.startTime + (minigame.endTime - minigame.startTime)/2;
    }

    protected override void OnHold(Vector2 hitPosition)
    {
        if(!nextBallPrepared) return;
        //Need one input update to find delta frame, fix ball position
        if(!isHeld){
            isHeld = true;
            moveDirection = baseBallPosition.position - heldBall.transform.position;
            deltaFrame = 1;
            trajectory.ShowTrajectory();
        } else { 
            moveDirection = (Vector3)hitPosition - heldBall.transform.position;
            finalDest = hitPosition;
            
            if((baseBallPosition.position-(Vector3)hitPosition).magnitude>maxDraw){
                finalDest = baseBallPosition.position + (((Vector3)hitPosition - baseBallPosition.position).normalized * maxDraw);
                moveDirection = finalDest - heldBall.transform.position;
                hitPosition = finalDest;
            }

            //Stop jittering due to input accuracy
            if(moveDirection.magnitude < minMovement){
                deltaFrame = -1;
            } else {
                deltaFrame = Time.frameCount - lastFrameCount;
                strip.SetStrip(heldBall.transform.position,finalDest,deltaFrame);
                Vector3 hitPosV3 = hitPosition;
                trajectory.UpdateTrajectory(hitPosition,(baseBallPosition.position-hitPosV3)*throwMultiplier);
            }

        }
        lastFrameCount = Time.frameCount;
        currentMovingFrame = 0;
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        if(isHeld){
            Vector3 throwDirection = baseBallPosition.position - heldBall.transform.position;

            Rigidbody2D rb = heldBall.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(throwDirection*throwMultiplier,ForceMode2D.Impulse);

            heldBall.isInQueue = false;
            isHeld = false;
            moveDirection = Vector3.zero;
            Invoke(nameof(NextBallPreparedToFalse), nextBallPreparationDelay);

            strip.ResetStrip();
            trajectory.HideTrajectory();
        }
    }

    void Update(){
        TryPrepareNextBall();
        if(nextBallPrepared && currentMovingFrame<=deltaFrame && heldBall.isInQueue){
            if(currentMovingFrame==deltaFrame){
                heldBall.transform.position = finalDest; 
            } else {
                heldBall.transform.Translate(moveDirection/deltaFrame);
            }
            currentMovingFrame++;
        }
        if(Time.time > timeToAddBall){
            timeToAddBall = float.MaxValue;
            AddNewBall();
        }
    }

    void NextBallPreparedToFalse(){
        nextBallPrepared = false;
    }
    
    void TryPrepareNextBall(){
        if(!nextBallPrepared && readyBalls.Count>0){
            heldBall = readyBalls.Dequeue();
            moveDirection = baseBallPosition.position - heldBall.transform.position;
            finalDest = baseBallPosition.position;
            deltaFrame = 30;
            currentMovingFrame = 0;

            //Stop all physics based movement
            Rigidbody2D rb = heldBall.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.rotation = 0;

            nextBallPrepared = true;
        }
    }
    public void AddNewBall(){
        Instantiate(ballPrefab,baseBallPosition.position+new Vector3(-1,0,0),Quaternion.identity);
    }

    IEnumerator ReturnBall(Ball ball){
        yield return new WaitForSeconds(returnBallDelay);
        readyBalls.Enqueue(ball);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Ball ball) && !ball.isInQueue){
            StartCoroutine(ReturnBall(ball));
            ball.isInQueue = true;
        }
    }
}
