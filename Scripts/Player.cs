using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float JumpForce = 5;

    private Animator _animator;

    private float velocityX = 0.0f;

    private float velocityY = 0.0f;

    private bool jump = true;

    private Rigidbody _rig;

    private bool _isGrounded = true;

    public int HP = 100;

    public int Damage = 10;

    public float Cooldown;

    private float _lastFire;

    public TrashMob enemy;

    private int Detect;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouvement
        if (Input.GetKey(KeyCode.Z))
        {
            velocityY = 1f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            velocityY = -1f;
        }

        else
        {
            velocityY = 0f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            velocityX = -1f;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            velocityX = 1f;
        }

        else
        {
            velocityX = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocityY = velocityY * 2f;
            velocityX = velocityX * 2f;
        }

        //Saut
        if (Input.GetKey(KeyCode.Space) && jump)
        {
            Jump();
        }

        //Attaque
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Detect == 1)
            {
                if (_lastFire <= Time.time)
                {
                    Attack();
                    _lastFire = Time.time + Cooldown;
                }
            }
        }

        //Mort
        if(HP <= 0){
            Dead();
            SceneManager.LoadScene("Game Over");
        }

        Move(velocityX, velocityY);
    }

    private void Move(float x, float y)
    {
        _animator.SetFloat("VelocityX",x);
        _animator.SetFloat("VelocityY",y);

        transform.position += Vector3.forward * y * Time.deltaTime;
        transform.position += Vector3.right * x * Time.deltaTime;
    }

    private void Jump()
    {
        if(_isGrounded){
            _isGrounded = false;
            _animator.SetTrigger("IsJumping");
            _rig.velocity = new Vector3(0, JumpForce, 0);
            jump = true;
        }
    }

    private void Attack()
    {
        Damage = Mathf.Max(Damage, 0);
        enemy.HP -= Damage;
        _animator.SetTrigger("IsAttack");
    }

    private void OnCollisionEnter(Collision collider)
    {
        Vector3 tempPoint = new Vector3();
        foreach(ContactPoint point in collider.contacts)
        {
            tempPoint += (Vector3)point.point;
        }
        tempPoint /= collider.contacts.Length;
        if(tempPoint.y < transform.position.y)
        {
            _isGrounded = true;
        }

        //Résolution de l'enigme
        if (collider.gameObject.tag == "Ascenseur")
        {
            Destroy(GameObject.FindGameObjectWithTag("Porte"));
        }

        //Entrée dans l'aura de l'ennemi
        if (collider.gameObject.tag == "Enemy")
        {
            Detect = 1;
        }
    }

    //Sortie de l'aura de l'ennemi
    private void OnCollisionExit() {
        Detect = 0;
    }

    private void Dead(){
        _animator.SetInteger("IsDead",1);
        Destroy(gameObject, 3);
    }
}
