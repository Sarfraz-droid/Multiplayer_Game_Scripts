using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Player : MonoBehaviour
{
    public int id;
    public string username;
    public Transform shootOrigin;
    public CharacterController controller;

    public float health;
    public float maxhealth = 100f;

    public float gravity = -9.8f;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

    public int speed_multiplier = 1;
    
    
    private bool[] inputs;
    private float yvelocity = 0;



    public bool isMoving;
    public bool isJumping;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;
    }
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        inputs = new bool[5];
        health = maxhealth;
    }
    public void FixedUpdate()
    {
        if (health <= 0f)
            return;
        Vector2 _inputDirection = new Vector2();
        if (inputs[0])
            _inputDirection.y += 1;
        if (inputs[1])
            _inputDirection.y -= 1;
        if (inputs[2])
            _inputDirection.x -= 1;
        if (inputs[3])
            _inputDirection.x += 1;
        if (_inputDirection != Vector2.zero)
        {
            isMoving = true;
        }
        else
            isMoving = false;
        Move(_inputDirection);
    }
    private void Move(Vector2 _inputDirection)
    {
        Vector3 MoveDirection = transform.forward * _inputDirection.y + transform.right * _inputDirection.x;
        MoveDirection *= moveSpeed*speed_multiplier;

        if (controller.isGrounded)
        {
            isJumping = false;
            yvelocity = 0f;
            if(inputs[4])
            {
                yvelocity = jumpSpeed;
            }
        }else
        {
            isMoving = false;
            isJumping = true;
        }
        yvelocity += gravity;

        MoveDirection.y = yvelocity;

        controller.Move(MoveDirection);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
        ServerSend.Playerbool(this);

    }
    public void SetInput(bool[] Inputs, Quaternion _rotation)
    {
        inputs = Inputs;
        transform.rotation = _rotation;
    }

    public void Shoot(Vector3 _viewDirection)
    {
        Debug.DrawRay(shootOrigin.position, _viewDirection*5, Color.red);
        if(Physics.Raycast(shootOrigin.position ,_viewDirection ,out RaycastHit hit , 25f))
        {

            Debug.Log(hit.collider.tag);
            if(hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponentInParent<Player>().TakeDamage(50f);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        if(health<=0f)
        {
            return;
        }
        health -= damage;
        if(health <=0)
        {
            health = 0f;
            controller.enabled = false;
            transform.position = new Vector3(0f, 25f, 0f);
            ServerSend.PlayerPosition(this);
            StartCoroutine(Respawn());
        }
        ServerSend.PlayerHealth(this);
    }

    public void Multiplier(int mult)
    {
        speed_multiplier = mult;
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        health = maxhealth;
        controller.enabled = true;
        ServerSend.playerRespawned(this);
    }

}
