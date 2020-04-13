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

        // Values
        public int mapScale;
        public int minSizeToSplit;
        public int maxNumOfHordes;

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

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            mapScaleSlider.value = mapScale;
            minSizeToSplitSlider.value = minSizeToSplit;
            maxNumOfHordesSlider.value = maxNumOfHordes;

            mapScaleText.text = mapScale.ToString();
            minSizeToSplitText.text = minSizeToSplit.ToString();
            maxNumOfHordesText.text = maxNumOfHordes.ToString();
        }

        private void Update()
        {
            mapScale = (int) mapScaleSlider.value;
            minSizeToSplit = (int) minSizeToSplitSlider.value;
            maxNumOfHordes = (int) maxNumOfHordesSlider.value;

            mapScaleText.text = mapScale.ToString();
            minSizeToSplitText.text = minSizeToSplit.ToString();
            maxNumOfHordesText.text = maxNumOfHordes.ToString();
        }

        #endregion


        #region Getters/Setters

        public int GetMapScale()
        {
            return mapScale;
        }

        public int GetMinSizeToSplit()
        {
            return minSizeToSplit;
        }

        public int GetMaxNumOfHordes()
        {
            return maxNumOfHordes;
        }

        #endregion
    }
}
