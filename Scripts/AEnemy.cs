using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AEnemy : MonoBehaviour
{
    public int HP = 50;
    public int Damages;
    public float Cooldown;

    private float _lastFire;
    protected NavMeshAgent _agent;
    
    private float speed = 2;
    private Transform playerPos;
    private Rigidbody _rig;
    private Animator _animator;
    private int Detect;
    public Player player;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rig = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Detect == 0)
        {
            Move();
        }
        else if (Detect == 1)
        {
            if (_lastFire <= Time.time)
            {
                Attack();
                _lastFire = Time.time + Cooldown;
            }
        }
        if(HP <= 0)
        {
            Detect = 2;
            Dead();
        }
    }

    //Mouvement automatique de l'ennemi
    protected virtual void Move(){
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        _animator.SetFloat("VelocityY", transform.position.y);
        _animator.SetFloat("VelocityX", transform.position.x);
    }

    //Attaque
    protected virtual void Attack(){
        Damages = Mathf.Max(Damages, 0);
        player.HP -= Damages;
        _animator.SetTrigger("IsAttack");
    }

    //Mort
    protected virtual void Dead(){
        if(Detect == 2){
        _animator.SetInteger("IsDead",1);
        Destroy(gameObject, 3);
        }
    }

    //Detection de l'aura du joueur
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Detect = 1;
        }
    }
    
    //Sortie de l'aura du joueur
    private void OnCollisionExit() {
        Detect = 0;
    }


}