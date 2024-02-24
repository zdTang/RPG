using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoint = 100f;
        bool isDie = false;

        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
                Die();
        }

        private void Die()
        {
            if (isDie) return;
            isDie = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}