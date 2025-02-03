using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public abstract void Use();

    #region GameFeel
    protected void PlaySFX(AudioClip sfx)
    {
        // play sfx
    }

    protected void PlayVFX()
    {
        // play vfx
    }
    #endregion

    protected void Damage(StatsHandler target, float amount)
    {
        target.CurrentLife -= amount;
    }

    protected void Heal(StatsHandler target, float amount) 
    {
        target.CurrentLife += amount;
    }

}
