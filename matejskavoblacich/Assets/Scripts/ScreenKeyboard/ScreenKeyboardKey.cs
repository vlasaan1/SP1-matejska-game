using UnityEngine;
using UnityEngine.Events;

public class ScreenKeyboardKey : BaseHoldable
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    char character;
    [SerializeField]
    UnityEvent<char> keyPress;

    private bool isActive = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        minTimeBeforeHold = .7f;
    }

    protected override void OnHold(Vector2 hitPosition) {
        if(!isActive){
            spriteRenderer.color = Color.gray;
            keyPress.Invoke(character);
            isActive = true;
        }
    }

    protected override void OnPress(Vector2 hitPosition)
    {
        spriteRenderer.color = new Color (200 / 255f, 200 / 255f, 200 / 255f);
    }

    protected override void OnRelease(Vector2 hitPosition) {
        spriteRenderer.color = Color.white;
        isActive = false;
    }
}
