using UnityEngine;

namespace PixelsoftGames
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// On awake, check if there's already an existing instanceData of this Singleton.  If there is, then destroy this instanceData.
        /// </summary>
        public virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            // Check if a Singleton instanceData already exists
            if (instance == null)
            {
                // If none exists, make this the Singleton
                instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
                enabled = true;
            }
            else
            {
                // If another Singleton instanceData already exists, destroy this instanceData.
                if (this != instance)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        /// <summary>
        /// On destroy check if we are destroying the singleton instanceData.  If so, we want to ensure that we nullify the instanceData reference.
        /// </summary>
        public virtual void OnDestroy()
        {
            if(instance != null && instance == this)
            {
                instance = null;
            }
        }
    }
}