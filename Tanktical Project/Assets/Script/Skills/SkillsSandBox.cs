using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillsSandBox
{
    public abstract class Skill
    {
        public string _skillName;
        public Characters _skillOwner;
        public int damageMultiplier;
        public Vector2 skillRange;
        public int explosionDamageRange;

        public abstract void Use(GameObject target);

        public abstract void SkillSelected();        
    }

    public class BasicAttack : Skill
    {
        public BasicAttack(string name, Characters skillOwner) 
        { 
            _skillName = name;
            _skillOwner = skillOwner;

            damageMultiplier = 100;

            damageOutpout = _skillOwner.characterType.baseDamage * (damageMultiplier/100);
            skillRange = _skillOwner.characterType.range;
        }

        public int damageOutpout;

        public override void Use(GameObject target)
        {
            Characters targetCharacter;
            target.TryGetComponent(out targetCharacter);

            targetCharacter.TakeDamage(damageOutpout);
        }

        public override void SkillSelected()
        {
            //foreach cases "walkable" du terrain
            //  si dans la range du skill
            //    si troupe ennemi sur case
            //      colorer la case en rouge
            //    fin si
            //
            //    sinon si troupe allié sur case
            //      colorer la case en bleu
            //      faire en sorte de ne pas pouvoir cliquer sur cases bleus
            //    fin sinon
            //
            //    sinon
            //      colorer case en vert
            //    fin sinon
            //  fin si
            //
            //  sinon
            //    colorer en gris ou gris sombre
            //  fin sinon

            //si curseur dans range du sort
            //  foreach case dans range explosion
            //    colorer case en jaune (ou orange)
            //  fin foreach
            //fin si
        }
    }

    public class APFSDS : Skill
    {
        public APFSDS(string name, Characters skillOwner)
        {
            _skillName = name;
            _skillOwner = skillOwner;
        }

        public override void Use(GameObject target)
        {

        }

        public void SecondShot()
        {

        }

        public override void SkillSelected()
        {

        }
    }

    public class HE : Skill
    {
        public HE(string name, Characters skillOwner)
        {
            _skillName = name;
            _skillOwner = skillOwner;
        }



        public override void Use(GameObject target)
        {

        }

        public override void SkillSelected()
        {

        }
    }
}
