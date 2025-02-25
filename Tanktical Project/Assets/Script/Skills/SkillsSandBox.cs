using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SkillsSandBox
{
    public abstract class Skill
    {
        protected GridHandler gridHandler;

        public string _skillName;
        public Characters _skillOwner;
        public int damageMultiplier;
        public Vector2 skillRange;
        public int explosionDamageRange;

        public abstract void Use(GameObject target);

        public abstract void SkillSelected();

        public abstract void SetSkillOwner(Characters owner);
    }

    public class BasicAttack : Skill
    {
        public BasicAttack(string name, Characters owner, string targetTag) 
        { 
            _skillName = name;
            _skillOwner = owner;

            damageMultiplier = 100;

            damageOutpout = _skillOwner.characterType.baseDamage * (damageMultiplier/100);
            skillRange = _skillOwner.characterType.range;

            gridHandler = GridHandler.Instance;
        }

        public string TargetTag;

        public int damageOutpout;

        public override void Use(GameObject target)
        {
            Characters targetCharacter;
            target.TryGetComponent(out targetCharacter);

            targetCharacter.TakeDamage(damageOutpout);

            _skillOwner.gameObject.TryGetComponent(out Entity _skillOwnerEntity);

            Debug.Log(damageOutpout);

            _skillOwnerEntity.EndTurn();
            SkillSelectorManager.Instance.SetSelectedSkill(null);
        }

        public override void SkillSelected()
        {
            Debug.Log(gridHandler.CellsList.Count);

            foreach (GameObject cells in gridHandler.CellsList) //cases "walkable" du terrain
            {
                SpriteRenderer currentCellsSpriteRenderer;
                cells.TryGetComponent<SpriteRenderer>(out currentCellsSpriteRenderer);

                if (Vector3.Distance(_skillOwner.transform.position, cells.transform.position) >= skillRange.x && Vector3.Distance(_skillOwner.transform.position, cells.transform.position) <= skillRange.y) //  si dans la range du skill
                {
                    if (skillRange == Vector2.one) //si troupe ennemi sur case
                    { 
                        currentCellsSpriteRenderer.color = gridHandler.EnnemyOnCaseRangePreview; //colorer la case en rouge
                    }
                    else if (skillRange == Vector2.right) //sinon si troupe allié sur case
                    {
                        currentCellsSpriteRenderer.color = gridHandler.AllyOnCaseRangePreview; //colorer la case en bleu
                    }
                    //    fin sinon
                    else
                    {
                        currentCellsSpriteRenderer.color = gridHandler.CaseInSkillRange; //colorer case en vert
                    }
                    //    fin sinon
                }
                    //  fin si
                //  sinon
                else
                {
                //    colorer en gris ou gris sombre

                }
                //  fin sinon

                //si curseur dans range du sort
                //  foreach case dans range explosion
                //    colorer case en jaune (ou orange)
                //  fin foreach
                //fin si
            }
        }

        public override void SetSkillOwner(Characters owner)
        {
            _skillOwner = owner;
        }
    }

    public class APFSDS : Skill
    {
        public APFSDS(string name)
        {
            _skillName = name;
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

        public override void SetSkillOwner(Characters owner)
        {
            _skillOwner = owner;
        }
    }

    public class HE : Skill
    {
        public HE(string name, Characters skillOwner)
        {
            _skillName = name;
        }



        public override void Use(GameObject target)
        {

        }

        public override void SkillSelected()
        {

        }

        public override void SetSkillOwner(Characters owner)
        {
            _skillOwner = owner;
        }
    }
}
