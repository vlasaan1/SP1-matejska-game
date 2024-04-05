using UnityEngine;

[CreateAssetMenu(fileName = "New minigame", menuName = "Minigame Prefab")]
public class MinigamePrefabSO : ScriptableObject
{
    public GameObject minigamePrefab;
    public string introText;
    public Sprite icon;
}
