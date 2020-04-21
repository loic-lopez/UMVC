using UMVC.Abstracts;
using UMVC.Interfaces;
using UMVC.Models;
using UMVC.Singleton;
using UMVC.Utils;
using UnityEngine;

namespace UMVC.Windows
{
    public class CreateModelWindow : Window
    {
        public override void SetupWindow()
        {
            base.SetupWindow();

            //_settingsModel = Asset.CreateAssetIfNotExists<SettingsModel>("Settings", "");
        }
        
        protected override void OnGUI()
        {
            base.OnGUI();
        }
    }
}