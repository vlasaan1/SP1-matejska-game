using UnityEngine;

/// <summary>
/// Visualization of ball trajectory in basketball minigame
/// </summary>
public class Trajectory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject dotParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] Rigidbody2D ballPrefabRb;
    [SerializeField] Transform minigameSize;

    [Header("Trajectory variables")]
    [SerializeField] int numberOfDots = 20;
    [SerializeField] float baseDotSpacing = .3f;
    [SerializeField] float minDotSize;
    [SerializeField] float maxDotSize;
   
    Transform[] dots;

    void Start(){
        //Prepare all dots
        dots = new Transform[numberOfDots];
        for(int i=0;i<numberOfDots;i++){
            dots[i] = Instantiate(dotPrefab,dotParent.transform).transform;
        }
        HideTrajectory();
    }

    /// <summary>
    /// Resets dots out of screen to hide last trajectory and shows them
    /// </summary>
    public void ShowTrajectory(){
        dotParent.SetActive(true);
        //Reset points
        for(int i=0;i<numberOfDots;i++){
            dots[i].position = new Vector3(500,500,0);
        }
    }

    /// <summary>
    /// Hides all dots
    /// </summary>
    public void HideTrajectory(){
        dotParent.SetActive(false);
    }

    /// <summary>
    /// Moves dots to match new trajectory
    /// </summary>
    /// <param name="basePos"> Starting position of the trajectory </param>
    /// <param name="force"> Force the ball is thrown with, needed to calculate trajectory </param>
    public void UpdateTrajectory(Vector2 basePos, Vector2 force){
        Vector3 dotPos = Vector3.zero;
        float dotSpacing = baseDotSpacing;
        float dotSize = maxDotSize;
        float dotDiff = (maxDotSize-minDotSize)/numberOfDots;

        for(int i=0;i<numberOfDots;i++){
            dotPos.x = basePos.x+force.x*dotSpacing;
            dotPos.y = basePos.y+force.y*dotSpacing - Physics2D.gravity.magnitude*ballPrefabRb.gravityScale*dotSpacing*dotSpacing/2; //Physics formula

            //Hide dots that would go out of current minigame space
            if(dotPos.x > minigameSize.position.x + (minigameSize.localScale.x/2) || dotPos.x < minigameSize.position.x - (minigameSize.localScale.x/2)){
                dotPos.x = 500;
                for(int j=i;j<numberOfDots;j++){
                    dots[j].position = dotPos;
                }
                break;
            }

            dots[i].position = dotPos;
            dotSpacing += baseDotSpacing;

            dots[i].localScale = Vector3.one * dotSize;
            dotSize -= dotDiff;
        }
    }

}
