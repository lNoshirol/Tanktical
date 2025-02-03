using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTHeal : Skill
{
    private StatsHandler _target;

    public TESTHeal(StatsHandler target)
    {
        _target = target;
    }


    public override void Use()
    {
        Heal(_target, 5);
    }
}
