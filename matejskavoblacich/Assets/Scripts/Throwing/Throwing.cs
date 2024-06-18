using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic for throwing the ball in basketball minigame
/// </summary>
public class Throwing : BaseHoldable
{
    [Header("References")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Minigame minigame;
    [SerializeField] Transform baseBallPosition;
    [SerializeField] Trajectory trajectory;
    [SerializeField] Strip strip;

    [Header("Throwing settings")]
    [SerializeField] float maxDraw = 0.9f;
    [SerializeField] float throwMultiplier = 12;
    [SerializeField, Tooltip("Time between ball touching the floor and flying to base position")] float returnBallDelay = .3f;
    [SerializeField, Tooltip("Time the ball takes to fly to the base position")] float nextBallPreparationDelay = .2f;
    [SerializeField] int bonusBallEveryNBalls = 3;


    Queue<Ball> readyBalls = new();  //Not really needed now, but prepared for adding more then 1 ball
    Ball heldBall;
    Vector3 moveDirection;
    Vector3 finalDest;
    readonly float minMovement = 0.03f;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;
    bool isHeld = false;
    bool nextBallPrepared = false;
    int bonusBallCounter;

    void Start(){
        //Spawn one ball, put it in queue and inicialize counter for bonus
        readyBalls.Enqueue(Instantiate(ballPrefab).GetComponent<Ball>());
        readyBalls.Peek().isInQueue = true;
        bonusBallCounter = bonusBallEveryNBalls;
    }

    protected override void OnHold(Vector2 hitPosition)
    {
        //If no ball is prepared, there is nothing to do
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
            
            //Start moving the ball to match input 
            if((baseBallPosition.position-(Vector3)hitPosition).magnitude>maxDraw){
                finalDest = baseBallPosition.position + (((Vector3)hitPosition - baseBallPosition.position).normalized * maxDraw);
                moveDirection = finalDest - heldBall.transform.position;
                hitPosition = finalDest;
            }

            //Stop jittering due to input inaccuracy
            if(moveDirection.magnitude < minMovement){
                deltaFrame = -1;
            //Move strips with the ball, update trajectory vizualization
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
        //Throw ball, reset variables, reset strip, hide trajectory vizualization
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
    }

    /// <summary>
    /// Needed for invoking ball preparation
    /// </summary>
    void NextBallPreparedToFalse(){
        nextBallPrepared = false;
    }
    
    /// <summary>
    /// Prepare next ball to base position, if possible
    /// </summary>
    void TryPrepareNextBall(){
        if(!nextBallPrepared && readyBalls.Count>0){
            heldBall = readyBalls.Dequeue();
            moveDirection = baseBallPosition.position - heldBall.transform.position;
            finalDest = baseBallPosition.position;
            deltaFrame = 30;
            currentMovingFrame = 0;

            //Stop all physics based movement of the ball
            Rigidbody2D rb = heldBall.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.rotation = 0;

            nextBallPrepared = true;
            bonusBallCounter -= 1;
            if(bonusBallCounter <= 0){
                heldBall.TurnOnBonus();
                bonusBallCounter = bonusBallEveryNBalls;
            } else {
                heldBall.TurnOffBonus();
            }
        }
    }

    /// <summary>
    /// Wait for returnBallDelay, then add ball to ready queue
    /// </summary>
    IEnumerator ReturnBall(Ball ball){
        yield return new WaitForSeconds(returnBallDelay);
        readyBalls.Enqueue(ball);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Mark ball as ready after it hits floor
        if(collision.collider.TryGetComponent(out Ball ball) && !ball.isInQueue){
            StartCoroutine(ReturnBall(ball));
            ball.isInQueue = true;
        }
    }
}
