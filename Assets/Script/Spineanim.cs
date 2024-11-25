using UnityEngine;
using Spine.Unity;

public class Spineanim : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    void Start()
    {
        var animationNames = skeletonAnimation.Skeleton.Data.Animations;
        foreach (var anim in animationNames)
        {
            Debug.Log("Animation Name: " + anim.Name);
        }
    }
}
