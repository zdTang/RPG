using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        SoundPlayer soundPlayer;
        float timeSinceLastAttack=0f;

        private void Start()
        {
            soundPlayer = GetComponent<SoundPlayer>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
            // This will trigger the Hit() event.
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        // Animation Event
        void Hit()
        {
            soundPlayer.PlayHit();
            Health healthComponent = target?.GetComponent<Health>();
            SoundPlayer targetSP=target?.GetComponent<SoundPlayer>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(weaponDamage);
                targetSP.PlaySoundCry();
            }
            
        }
    }
}