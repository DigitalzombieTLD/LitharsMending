using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;

namespace LitharsMending
{
	public static class ActivateAndUpdate
	{
        public static void ActivateRepairButton(bool enable)
        {
            LitharsMendingMain.buttonRepairMain.SetActive(enable);
            LitharsMendingMain.buttonRepairMain.transform.localPosition = new Vector3(0, -60, 0);
        }

        public static void ActivateRepairPanel(Panel_Inventory_Examine invPanel)
		{
            LitharsMendingMain.repairPanel.SetActive(true);
            LitharsMendingMain.repairPanelButtons.SetActive(true);

            float chanceSuccessfullRepair = invPanel.GetChanceSuccessfullRepair(invPanel.m_GearItem);
            float chanceActionSuccess = invPanel.GetChanceActionSuccess(chanceSuccessfullRepair);
            Repairable repairable = invPanel.m_GearItem.m_Repairable;
            string expandedDurationString = Utils.GetExpandedDurationString(invPanel.GetModifiedRepairDuration(repairable, repairable.m_DurationMinutes));

            invPanel.m_Repair_AmountLabel.text = invPanel.GetConditionIncreaseFromRepair(invPanel.m_GearItem) + "%";
            invPanel.m_Repair_ChanceSuccessLabel.text = chanceActionSuccess.ToString("F0") + "%";
            invPanel.m_Repair_ConditionCapLabel.text = repairable.m_RepairConditionCap.ToString("F0") + "%";
            invPanel.m_Repair_TimeLabel.text = expandedDurationString;
            invPanel.UpdateWeightAndConditionLabels();
        }       
    }
}