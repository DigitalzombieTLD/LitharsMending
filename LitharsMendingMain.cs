using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;

namespace LitharsMending
{
	public class LitharsMendingMain : MelonMod
	{
		public static GameObject buttonRepairMain;
        public static GameObject repairPanel;
        public static GameObject repairPanelButtons;

        public override void OnInitializeMelon()
		{
            Settings.OnLoad();
        }

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (sceneName.Contains("Menu"))
			{
                buttonRepairMain = GameObject.Find("SCRIPT_InterfaceManager/_GUI_Common/Camera/Anchor/Panel_Inventory_Examine/ExamineWidget/MainWindow/MainWindow_Items/MenuItems/Button_Repair");
                repairPanel = GameObject.Find("SCRIPT_InterfaceManager/_GUI_Common/Camera/Anchor/Panel_Inventory_Examine/ExamineWidget/RepairPanel");
                repairPanelButtons = GameObject.Find("SCRIPT_InterfaceManager/_GUI_Common/Camera/Anchor/Panel_Inventory_Examine/ExamineWidget/RepairPanel/RepairPanel_Buttons");
            }
        }

        public override void OnUpdate()
		{

		}
    }
}