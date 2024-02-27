using initi.prefabScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// Created by 2023 SP1 team, FIT CTU
/// 
/// <summary>
///     Basic interface for interactive elements. This class serves as an abstraction
///     of BaseHittable to act as a button. In this analogy a button interaction is
///     a series of method calls. When the button is pressed, it sends a signal (onPress).
///     Then it is held, which sends signal periodically (onHold). Finally the button
///     is released, which again sends signal (onRelease).
///     
///     In order to function it requires a collider.
/// </summary>
public class BaseInteractive : BaseHittable
{
    private const float HIT_DELAY = 0.5f;
    private float lastHitTime = 0;

    /// <summary>
    ///     Registers any hit and decides which control functions to call.
    /// </summary>
    /// <param name="hitPosition">Global position of hit. Changes it to local position.</param>
    public override void Hit(Vector2 hitPosition)
    {
        float currentTime = Time.time;

        // Transform global position to local position
        hitPosition = transform.InverseTransformPoint(new Vector3(hitPosition.x, hitPosition.y));

        // onHit
        OnHit(hitPosition);

        // onHold and onPress
        if (currentTime - lastHitTime < HIT_DELAY)
            OnHold(hitPosition);
        else
            OnPress(hitPosition);

        // onRelease
        StartCoroutine(TestRelease(hitPosition));

        lastHitTime = currentTime;
    }

    /// <summary>
    ///     Checks if touch was released, in that case calls onRelease().
    /// </summary>
    /// <param name="hitPosition">Local position of hit.</param>
    /// <returns></returns>
    private IEnumerator TestRelease(Vector2 hitPosition)
    {
        yield return new WaitForSeconds(HIT_DELAY);
        if (Time.time - lastHitTime >= HIT_DELAY)
        {
            OnRelease(hitPosition);
        }
    }

    /// <summary>
    ///     Called on first hit.
    ///     Child classes should override this function.
    /// </summary>
    /// <param name="hitPosition">Local position of hit.</param>
    public virtual void OnPress(Vector2 hitPosition) { }

    /// <summary>
    ///     Called after last hit.
    ///     Child classes should override this function.
    /// </summary>
    /// <param name="hitPosition">Local position of hit.</param>
    public virtual void OnRelease(Vector2 hitPosition) { }

    /// <summary>
    ///     Called on every hit between onHold and onRelease.
    ///     Child classes should override this function.
    /// </summary>
    /// <param name="hitPosition">Local position of hit.</param>
    public virtual void OnHold(Vector2 hitPosition) { }

    /// <summary>
    ///     Called on every hit. Use this if you want to implement custom hit logic.
    ///     Child classes should override this function.
    /// </summary>
    /// <param name="hitPosition">Local position of hit.</param>
    public virtual void OnHit(Vector2 hitPosition) { }


}
