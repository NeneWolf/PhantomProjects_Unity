using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.cygnusprojects.TalentTree
{
    public class TalentExtraScript : MonoBehaviour
    {
        public Color EnabledColor;
        public Color DisabledColor;

        private TalentUI tUI;
        private RectTransform rect;        
        
        // Use this for initialization
        void Start()
        {
            tUI = gameObject.GetComponent<TalentUI>(); 
            rect = gameObject.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (tUI.Talent.isValid && !tUI.Talent.IsEnabled)
            {
                float v = Mathf.Lerp(1f, 1.05f, Mathf.PingPong(Time.time, 1));
                rect.localScale = new Vector3(v, v, 1f);
            }
            else
                rect.localScale = new Vector3(1f, 1f, 1f);

            if (tUI.TalentBackground != null)
            {
                if (tUI.Talent.IsEnabled) // if (Talent.isValid)
                {
                    tUI.TalentBackground.color = EnabledColor;
                }
                else
                {
                    tUI.TalentBackground.color = DisabledColor;
                }
            }

        }
    }
}
