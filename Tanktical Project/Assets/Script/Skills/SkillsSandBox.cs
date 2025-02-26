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

        public string TargetTag;

        public int damageOutpout;

        public float zoneDamageRange;

        public string skillName;

        public Characters _skillOwner;

        public int damageMultiplier;

        public Vector2 skillRange;

        public int explosionDamageRange;

        public int TDR;

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

    public class ActiveSkill : Skill
    {
        public ActiveSkill(string _name, Characters _owner, string _targetTag, int _damageMultiplayer, int _zoneDamage, Vector2 _skillRange, int _TDR)
        {
            skillName = _name;
            _skillOwner = _owner;

            damageMultiplier = _damageMultiplayer;
            zoneDamageRange = _zoneDamage;

            damageOutpout = _skillOwner.characterType.baseDamage * (damageMultiplier / 100);
            skillRange = _skillRange;

            gridHandler = GridHandler.Instance;
            clickDetector = ClickDetector.Instance;
            TDR = _TDR;
        }

        


        public override void Use(GameObject target)
        {
            foreach (GameObject cell in GridHandler.Instance.CellsList)
            {
                cell.GetComponent<SpriteRenderer>().color = GridHandler.Instance.BlankCellColor;
            }

            Characters targetCharacter;
            target.TryGetComponent(out targetCharacter);

            targetCharacter.TakeDamage(damageOutpout);

            _skillOwner.gameObject.TryGetComponent(out Entity _skillOwnerEntity);


            _skillOwnerEntity.EndTurn();
            SkillSelectorManager.Instance.SetSelectedSkill(null);

            if (zoneDamageRange > 0)
            {
                foreach (GameObject ennemy in EnitityList.instance.EnnemyList)
                {
                    float distance = Vector3.Distance(target.transform.position, ennemy.transform.position);

                    if (distance <= zoneDamageRange && ennemy != target)
                    {
                        ennemy.GetComponent<Characters>().TakeDamage((int)(damageOutpout/3));
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
}