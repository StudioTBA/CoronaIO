using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.StudioTBD.CoronaIO.Menus
{
    public class Settings : MonoBehaviour
    {
        #region Properties

        // Sliders
        public Slider mapScaleSlider;
        public Slider minSizeToSplitSlider;
        public Slider maxNumOfHordesSlider;

        // Text
        public TMP_Text mapScaleText;
        public TMP_Text minSizeToSplitText;
        public TMP_Text maxNumOfHordesText;

        #endregion


        #region MonoBehaviour Callbacks

        private void Start()
        {
            mapScaleSlider.value = MenuManager.mapScale;
            minSizeToSplitSlider.value = MenuManager.minSizeToSplit;
            maxNumOfHordesSlider.value = MenuManager.maxNumOfHordes;

            mapScaleText.text = MenuManager.mapScale.ToString();
            minSizeToSplitText.text = MenuManager.minSizeToSplit.ToString();
            maxNumOfHordesText.text = MenuManager.maxNumOfHordes.ToString();
        }

        private void Update()
        {
            MenuManager.mapScale = (int) mapScaleSlider.value;
            MenuManager.minSizeToSplit = (int) minSizeToSplitSlider.value;
            MenuManager.maxNumOfHordes = (int) maxNumOfHordesSlider.value;

            mapScaleText.text = MenuManager.mapScale.ToString();
            minSizeToSplitText.text = MenuManager.minSizeToSplit.ToString();
            maxNumOfHordesText.text = MenuManager.maxNumOfHordes.ToString();
        }

        #endregion
    }
}
