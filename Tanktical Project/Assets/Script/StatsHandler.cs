using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public float CurrentLife { get; set; }
    public float MaxLife { get; set; }

    private void Start()
    {
        CurrentLife = 10;
    }

    #region DebugProtoTestingTemporary
    public void Attack()
    {
        float life = CurrentLife;
        TESTAttack attack = new(this);
        attack.Use();
        Debug.Log($"<color=#e02b49> Former life : {life}, current life : {CurrentLife}</color>");
    }

    public void Heal()
    {
        float life = CurrentLife;
        TESTHeal heal = new(this);
        heal.Use();
        Debug.Log($"<color=#34eb4f> Former life : {life}, current life : {CurrentLife}</color>");
    }
    #endregion
}
