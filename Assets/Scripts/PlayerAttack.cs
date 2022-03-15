using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// Script Inputs
    /// </summary>
    [SerializeField]
    private float AttackCooldown;

    /// <summary>
    /// Components
    /// </summary>
    private Animator m_Animator;
    private PlayerMovement m_PlayerMovement;

    /// <summary>
    /// Contorl Params
    /// </summary>
    private float CooldownTimer;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && m_PlayerMovement.CanAttack() && CooldownTimer > AttackCooldown)
        {
            Attack();
        }

        CooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
