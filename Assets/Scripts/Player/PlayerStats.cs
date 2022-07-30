using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Core
{
    public class PlayerStats : MonoBehaviour
    {
        #region 
        [Header("Bars")]
        [Space]
        public HealthBar healthBar;
        public EnergyBar energyBar;

        [Header("Basic Stats")]
        [Space]
        [SerializeField] int maxHealth;
        [SerializeField] int maxEnergy;
        int currentHealth;
        int currentEnergy;

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

            //Set the bars values
            healthBar.SetMaxHealth(maxHealth);
            energyBar.SetMaxEnergy(maxEnergy);
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

                healthBar.SetHealth(currentHealth);
            }
        }
        public void TakeDamage(float damageAmount)
        {
            if (currentHealth - damageAmount >= 0)
            {
                currentHealth -= (int)damageAmount;

                healthBar.SetHealth(currentHealth);
            }
        }

        //Cost of the Energy when using "Special Skill"
        public void UseEnergy(int amount)
        {
            if (currentEnergy - amount >= 0)
            {
                currentEnergy -= amount;
                energyBar.SetEnergy(currentEnergy);

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
            
            energyBar.SetEnergy(currentEnergy);
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

            healthBar.SetHealth(currentHealth);
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
