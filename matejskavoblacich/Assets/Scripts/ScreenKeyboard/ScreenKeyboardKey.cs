using UnityEngine;
using UnityEngine.Events;

public class ScreenKeyboardKey : BaseHoldable
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    char character;
    [SerializeField]
    UnityEvent<char> keyPress;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnHold(Vector2 hitPosition) {
        spriteRenderer.color = Color.gray;
        keyPress.Invoke(character);
    }

    protected override void OnRelease(Vector2 hitPosition) {
        spriteRenderer.color = Color.white;
    }
}
