using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public void StartTransition(){
        GetComponent<Animator>().SetTrigger("EndScene");
    }
}
