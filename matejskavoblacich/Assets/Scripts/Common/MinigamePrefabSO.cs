using UnityEngine;

[CreateAssetMenu(fileName = "New minigame", menuName = "MinigameSO")]
/// <summary>
/// Groups minigame prefab with other minigame informations
/// </summary>
public class MinigamePrefabSO : ScriptableObject
{
    public GameObject minigamePrefab;
    public string introText; //Text shown on screen at the start of the minigame
}
