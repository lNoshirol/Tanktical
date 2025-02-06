using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TESTAttack : Skill
{
    private StatsHandler _target;

    public TESTAttack(StatsHandler target) 
    {
        _target = target;
    }


    public override void Use()
    {
        Damage(_target, 5);
    }
}
