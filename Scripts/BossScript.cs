using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject Enemy;
    private Animator _animator;

    public int AttackTrigger;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(AttackTrigger == 0)
        {
            _animator.SetInteger("Attack", 0);
        }
        if(AttackTrigger == 1)
        {
            _animator.SetInteger("Attack", 1);
        }
    }

    void OnTriggerEnter()
    {
        AttackTrigger = 1;
    }
    void OnTriggerExit()
    {
        AttackTrigger = 0;
    }

}
