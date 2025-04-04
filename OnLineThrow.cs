using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnLineThrow : MonoBehaviour
{
    public Animation animations;

    public Fishing fishing;

    public IEnumerator ThrowLine()
    {
        animations.Play("ThrowingLine");

        yield return new WaitForSeconds(animations.GetClip("ThrowingLine").length);
        fishing.SetAnimationIsPlaying(false);

        fishing.ThrowBobber();
    }

    public IEnumerator ReelLine()
    {
        animations.Play("ReelingLine");

        yield return new WaitForSeconds(animations.GetClip("ReelingLine").length);

        fishing.SetAnimationIsPlaying(false);
    }
}
