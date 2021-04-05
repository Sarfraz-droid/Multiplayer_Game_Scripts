using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    public Animator Gun_anim;
    public void ShootDown()
    {
        Gun_anim.SetBool("isShooting", false);
    }
}
