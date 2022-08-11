using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.cygnusprojects.TalentTree
{
	public class TalentUI : MonoBehaviour 
	{
        #region Variables
        public TalentTreeNodeBase Talent;
        public TalentusEngine Engine;
        public Text NameLabel;
        public Text LevelIndicator;
        public Text CostLabel;
        public Image IconField;

        public Image TierIndicator;
        public Image TalentBackground;
        public Color EnabledColor = Color.white;
        public Color DisabledColor = Color.gray;

        public Button buyButton;
        public Button revertButton;

        private Sprite ImageUI;
        private Sprite DisabledImageUI;
         
        #endregion

        #region Unity Methods                
        private void TalentTree_TreeEvaluated(object sender, TalentTreeGraph.TreeEvaluatedEventArgs e)
        {
            UpdateUI();
        }

        private void OnEnable()
        {
            if (Engine != null)
                Engine.TalentTree.TreeEvaluated += TalentTree_TreeEvaluated;

            if (buyButton != null)
                buyButton.gameObject.SetActive(false);
            if (revertButton != null)
                revertButton.gameObject.SetActive(false);

            // Cache the Images as Sprites
            if (Talent != null)
            {
                if (IconField != null) ImageUI = Talent.ImageAsSprite;
                if (Talent.DisabledImage != null) DisabledImageUI = Talent.DisabledImageAsSprite;
            }

            UpdateUI();
        }

        private void OnDisable()
        {
            if (Engine != null)
                Engine.TalentTree.TreeEvaluated -= TalentTree_TreeEvaluated;
        }

        void UpdateUI()
        {            
            if (Talent != null)
            {               
                if (NameLabel != null)
                {
                    NameLabel.text = Talent.Name;
                }
                if (IconField != null)
                {
                    IconField.sprite = ImageUI; //Talent.ImageAsSprite;
                    if (!Talent.isValid && Talent.DisabledImage != null)
                    {
                        IconField.sprite = DisabledImageUI; //Talent.DisabledImageAsSprite;
                    }
                }
                if (LevelIndicator != null)
                {
                    LevelIndicator.text = string.Format("{0}/{1}",Talent.Level, Talent.MaxLevel);
                }
                if (CostLabel != null)
                {
                    CostLabel.text = string.Format("{0}", Talent.GetCostForNextLevel().Cost);
                }
                if (TierIndicator != null)
                {
                    TierIndicator.color = Talent.Tier.EditorColor;
                }
                if (TalentBackground != null)
                {
                    if (Talent.isValid)
                    {
                        TalentBackground.color = EnabledColor;
                    }
                    else
                    {
                        TalentBackground.color = DisabledColor;
                    }
                }
                if (buyButton != null)
                {
                    if (Talent.isValid)
                    {
                        buyButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        buyButton.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void DoBuy()
        {
            Engine.BuyTalent(Talent);
            if (revertButton != null)
            {
                revertButton.gameObject.SetActive(true);
            }
        }

        public void DoRevert()
        {
            Engine.RevertTalent(Talent);
            if (buyButton != null)
            {
                buyButton.gameObject.SetActive(true);
            }
        }
		#endregion
	}
}
