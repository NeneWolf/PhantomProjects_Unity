using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Core
{
    public class PlayerStats : MonoBehaviour
    {
        #region 
        [Header("health & Energy")]
        [Space]
        [SerializeField] public int maxHealth;
        [SerializeField] public int maxEnergy;
        public int currentHealth { get; private set; }
        public int currentEnergy { get; private set; }

        [Header("Regenerating Energy - Time ( Seconds ) & Points ")]
        [Space]
        [SerializeField] float regenEnergyPerSecond = 6f;
        [SerializeField] int regenEnergyPointsRegain;

        private WaitForSecondsRealtime regenTick = new WaitForSecondsRealtime(0.5f);

        private Coroutine regen;

        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            //Set Health & Energy
            currentHealth = maxHealth;
            currentEnergy = maxEnergy;

        }

        // Update is called once per frame
        void Update()
        {
            //Always making sure the energy & Health does not goes pass the max
            if (currentEnergy > maxEnergy)
                currentEnergy = maxEnergy;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            if (currentHealth - attackDetails.damageAmount >= 0)
            {
                currentHealth -= (int)attackDetails.damageAmount;

            }
        }
        public void TakeDamage(float damageAmount)
        {
            if (currentHealth - damageAmount >= 0)
            {
                currentHealth -= (int)damageAmount;

            }
        }

        //Cost of the Energy when using "Special Skill"
        public void UseEnergy(int amount)
        {
            if (currentEnergy - amount >= 0)
            {
                currentEnergy -= amount;

                //Start and stop the coroutine to regen the energy back
                if (regen != null)
                    StopCoroutine(regen);

                regen = StartCoroutine(RegenEnergy());
            }
        }

        public void AddEnergy(int amount)
        {
            if(currentEnergy + amount >= maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
            else
            {
                currentEnergy += amount;
            }
            
        }

        public void AddHealth(int amount)
        {
            if (currentHealth + amount >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += amount;
            }

        }

        //Auto Energy Regen
        private IEnumerator RegenEnergy()
        {
            // Wait time before starting to regen
            yield return new WaitForSecondsRealtime(regenEnergyPerSecond);

            while (currentEnergy <= maxEnergy)
            {
                //Regen Tick - Time between each regeneration 
                yield return regenTick;

                AddEnergy(regenEnergyPointsRegain);

                yield return regenTick;
            }

            regen = null;
        }

        public int ReportHealth()
        {
            return currentHealth;
        }
    }
}
