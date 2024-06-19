using UnityEngine;

/// <summary>
/// Sets trigger in animator to end scene
/// </summary>
public class SceneTransition : MonoBehaviour
{
    public void StartTransition(){
        GetComponent<Animator>().SetTrigger("EndScene");
    }
}
