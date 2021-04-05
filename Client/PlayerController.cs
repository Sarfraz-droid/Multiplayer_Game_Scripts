using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform camtransform;

    [Header("Time Handle : ")]
    public float fireRate = .25f;
    public float nextTimeofFire = 0f;

    [Header("Bullet System")]
    public int bullets = 6;
    public int max_bullets = 6;

    [Header("Animator Gun : ")]
    public Animator Gun_Anim;

    [Header("Particle Systems : ")]
    public ParticleSystem MuzzleFlash;

    [Header("Text : ")]
    public TextMeshProUGUI Curr_bullets;

    [Header("Bools :")]
    public bool isReload = false;
    public bool isShooting = false;
    public bool isMoving = false;
    private void Update()
    {
        Curr_bullets.text = bullets + "";
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(!isReload && bullets == 0 )
            {
                StartCoroutine(Reload());
            }else if (Time.time >= nextTimeofFire && !isReload)
            {
                Gun_Anim.SetBool("isShooting", true);
                nextTimeofFire = Time.time + fireRate;
                bullets--;
                MuzzleFlash.Play();
                ClientSend.PlayerShoot(camtransform.forward);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ClientSend.PlayerRunning(2);
        }
        else
            ClientSend.PlayerRunning(1);
    }
    private void FixedUpdate()
    {
        SendInputToServer();
    }
    private void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };
        ClientSend.Playermovment(inputs);
   
}
    //IEnumators

    IEnumerator Reload()
    {
        Gun_Anim.SetBool("isShooting", false);
        Gun_Anim.SetBool("isReloading",true);
        yield return new WaitForSeconds(.82f);
        Gun_Anim.SetBool("isReloading", false);
        bullets = max_bullets;
        isReload = false;
    }
}
