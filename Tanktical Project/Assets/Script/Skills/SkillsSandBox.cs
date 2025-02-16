using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillsSandBox
{
    public abstract class Skill
    {
        public string _skillName;

        protected int characterBaseDamage;

        public abstract void Use();
    }

    public class APFSDS : Skill
    {
        public APFSDS(string name)
        {
            _skillName = name;
        }

        public override void Use()
        {

        }
    }

    public class HE : Skill
    {
        public HE(string name)
        {
            _skillName = name;
        }

        public override void Use()
        {

        }
    }
}
