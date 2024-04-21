using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] GameObject dotParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] Rigidbody2D ballPrefabRb;
    [SerializeField] Transform minigameSize;
    [SerializeField] int numberOfDots = 20;
    [SerializeField] float baseDotSpacing = .3f;
    [SerializeField] float minDotSize;
    [SerializeField] float maxDotSize;
   
    Transform[] dots;

    void Start(){
        dots = new Transform[numberOfDots];
        for(int i=0;i<numberOfDots;i++){
            dots[i] = Instantiate(dotPrefab,dotParent.transform).transform;
        }
        HideTrajectory();
    }

    public void ShowTrajectory(){
        dotParent.SetActive(true);
        //Reset points
        for(int i=0;i<numberOfDots;i++){
            dots[i].position = new Vector3(500,500,0);
        }
    }

    public void UpdateTrajectory(Vector2 basePos, Vector2 force){
        Vector3 dotPos = Vector3.zero;
        float dotSpacing = baseDotSpacing;
        float dotSize = maxDotSize;
        float dotDiff = (maxDotSize-minDotSize)/numberOfDots;

        for(int i=0;i<numberOfDots;i++){
            dotPos.x = basePos.x+force.x*dotSpacing;
            dotPos.y = basePos.y+force.y*dotSpacing - Physics2D.gravity.magnitude*ballPrefabRb.gravityScale*dotSpacing*dotSpacing/2;

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

    public void HideTrajectory(){
        dotParent.SetActive(false);
    }
}
