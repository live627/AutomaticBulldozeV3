﻿using ColossalFramework;
using ColossalFramework.UI;
using System.Reflection;
using UnityEngine;

namespace AutomaticBulldozeV3.UI
{
    public class UIAutoBulldozerPanel : UIPanel
    {
        private readonly Localization localization = Localization.Instance;
        private UIButton demolishAbandonedButton;
        private UIButton demolishBurnedButton;

        public static readonly SavedBool DemolishAbandoned = new SavedBool("ModDemolishAbandoned", Settings.gameSettingsFile, true, true);
        public static readonly SavedBool DemolishBurned = new SavedBool("ModDemolishBurned", Settings.gameSettingsFile, true, true);

        private void InitButton(UIButton button)
        {
            button.normalBgSprite = "SubBarButtonBaseHovered";
            button.disabledBgSprite = "SubBarButtonBaseHovered";
            button.hoveredBgSprite = "SubBarButtonBaseHovered";
            button.focusedBgSprite = "SubBarButtonBaseHovered";
            button.pressedBgSprite = "SubBarButtonBasePressed";
            button.textColor = new Color32(255, 255, 255, 255);
        }

        private void UpdateCheckButton(UIButton button, bool isActive)
        {
            var inactiveColor = new Color32(64, 64, 64, 255);
            var activeColor = new Color32(64, 255, 64, 255);
            var textColor = new Color32(255, 255, 255, 255);
            var textColorDis = new Color32(128, 128, 128, 255);

            if (isActive)
            {
                button.color = activeColor;
                button.focusedColor = activeColor;
                button.hoveredColor = activeColor;
                button.pressedColor = activeColor;
                button.textColor = textColor;
            }
            else
            {
                button.color = inactiveColor;
                button.focusedColor = inactiveColor;
                button.hoveredColor = inactiveColor;
                button.pressedColor = inactiveColor;
                button.textColor = textColorDis;
            }

            button.Unfocus();
        }

        private void SetLocales()
        {
            demolishAbandonedButton.text = localization.GetString("Switch.DemolishAbandoned");
            MethodInfo m = typeof(UIButton).GetMethod("AutoSize", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(demolishAbandonedButton, null);
            demolishAbandonedButton.width = demolishAbandonedButton.width + 30;
            demolishAbandonedButton.height = 40;
            demolishBurnedButton.text = localization.GetString("Switch.DemolishBurned");
            m.Invoke(demolishBurnedButton, null);
            demolishBurnedButton.width = demolishBurnedButton.width + 30;
            demolishBurnedButton.height = 40;
        }

        public override void Start()
        {
            // configure panel
            height = 40;
            autoLayout = true;
            autoLayoutDirection = LayoutDirection.Horizontal;
            autoLayoutPadding = new RectOffset(0, 10, 0, 0);
            autoLayoutStart = LayoutStart.TopLeft;

            demolishAbandonedButton = AddUIComponent<UIButton>();
            InitButton(demolishAbandonedButton);
            demolishAbandonedButton.eventClick += (component, param) =>
            {
                DemolishAbandoned.value = !DemolishAbandoned.value;
                UpdateCheckButton(demolishAbandonedButton, DemolishAbandoned.value);
            };

            demolishBurnedButton = AddUIComponent<UIButton>();
            
            demolishBurnedButton.width = 200;
            demolishBurnedButton.height = 50;
            InitButton(demolishBurnedButton);
            demolishBurnedButton.eventClick += (component, param) =>
            {
                DemolishBurned.value = !DemolishBurned.value;
                UpdateCheckButton(demolishBurnedButton, DemolishBurned.value);
            };

            UpdateCheckButton(demolishAbandonedButton, DemolishAbandoned.value);
            UpdateCheckButton(demolishBurnedButton, DemolishBurned.value);

            SetLocales();
            Localization.Instance.eventLocaleChanged += language => SetLocales();
            
            base.Start();
        }
    }
}
