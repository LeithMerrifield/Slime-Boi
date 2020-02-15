using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private Animator m_animController;

    protected override void Start()
    {
        base.Start();
        m_animController = GetComponent<Animator>();
    }

    public TrailRenderer m_trail = null;
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        m_animController.SetTrigger("Swing");
    }
}
