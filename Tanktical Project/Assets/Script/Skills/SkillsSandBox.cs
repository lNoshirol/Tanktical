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
        protected ClickDetector clickDetector;

        public string _skillName;
        public Characters _skillOwner;
        public int damageMultiplier;
        public Vector2 skillRange;
        public int explosionDamageRange;

        public abstract void Use(GameObject target);

        public abstract void SkillSelected();


        protected GameObject GetUnitCase(GameObject owner)
        {
            Vector2 ownerPosIn2dSpace = new Vector2(owner.transform.position.x, owner.transform.position.z);

            foreach (GameObject cell in gridHandler.CellsList)
            {
                Vector2 realCellPos = new Vector2(cell.transform.position.x, cell.transform.position.z);
                if (Vector2.Distance(ownerPosIn2dSpace, realCellPos) <= 0.99f)
                {
                    Debug.Log($"Find ! charPos {ownerPosIn2dSpace}, cellPos {realCellPos} ");
                    return cell;
                }
            }
            return null;
        }
    }

    public class BasicAttack : Skill
    {
        public BasicAttack(string name, Characters owner, string targetTag) 
        { 
            _skillName = name;
            _skillOwner = owner;

            damageMultiplier = 100;
            zoneDamageRange = 3;

            damageOutpout = _skillOwner.characterType.baseDamage * (damageMultiplier/100);
            skillRange = _skillOwner.characterType.range;

            gridHandler = GridHandler.Instance;
            clickDetector = ClickDetector.Instance;
        }

        public string TargetTag;

        public int damageOutpout;

        public float zoneDamageRange;



        public override void Use(GameObject target)
        {
            Characters targetCharacter;
            target.TryGetComponent(out targetCharacter);

            targetCharacter.TakeDamage(damageOutpout);

            _skillOwner.gameObject.TryGetComponent(out Entity _skillOwnerEntity);


            _skillOwnerEntity.EndTurn();
            SkillSelectorManager.Instance.SetSelectedSkill(null);

            if (zoneDamageRange > 0 )
            {
                foreach (GameObject ennemy in EnitityList.instance.EnnemyList)
                {
                    if (Vector3.Distance(target.transform.position, ennemy.transform.position) <= zoneDamageRange)
                    {

                    }
                }
            }

            gridHandler._newOffsets.Clear();
        }

        public override void SkillSelected()
        {

            if (Vector3.Distance(_skillOwner.transform.position, clickDetector.Pos) <= skillRange.y)
            {
                for (int i = (int)-zoneDamageRange; i <= zoneDamageRange; i++)
                {
                    for (int j = (int)-zoneDamageRange; j <= zoneDamageRange; j++)
                    {
                        if (!gridHandler._newOffsets.Contains(new Vector2(j, i)) && Mathf.Abs(i) + Mathf.Abs(j) <= zoneDamageRange)
                        {
                            gridHandler._newOffsets.Add(new Vector2(j, i));
                        }
                    }
                }
            }

            foreach (GameObject cells in gridHandler.CellsList) //cases "walkable" du terrain
            {
                SpriteRenderer currentCellsSpriteRenderer;
                cells.TryGetComponent<SpriteRenderer>(out currentCellsSpriteRenderer);

                if (Vector3.Distance(_skillOwner.transform.position, cells.transform.position) >= skillRange.x && Vector3.Distance(_skillOwner.transform.position, cells.transform.position) <= skillRange.y) //  si dans la range du skill
                {
                    currentCellsSpriteRenderer.color = gridHandler.CaseInSkillRange; //colorer case en bleu
                }
            }
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
    }
}
