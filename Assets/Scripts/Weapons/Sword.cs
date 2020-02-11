using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private Animator m_animController;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
        m_animController = GetComponent<Animator>();
    }
    public override void Attack()
    {
        m_animController.SetTrigger("Swing");
    }
}
