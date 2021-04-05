using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    public float health;
    public float maxhealth = 100f;
    public MeshRenderer model;

    [Header("Text : ")]
    public TextMeshProUGUI Health_text;


    void FixedUpdate()
    {
        if(Client.instance.myId == id)
            Health_text.text = "Health : " + health + " ";
    }
    public void Initialize(int _id,string _username)
    {
        id = _id;
        username = _username;
        health = maxhealth;
    }
    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        model.enabled = false;
    }
    public void respawn()
    {
        model.enabled = true;
        SetHealth(maxhealth);
    }
}
