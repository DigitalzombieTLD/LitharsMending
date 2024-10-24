using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;
using Il2CppNewtonsoft.Json.Linq;


namespace LitharsMending
{   
    [HarmonyLib.HarmonyPatch(typeof(Panel_Inventory_Examine), "Enable", new Type[] { typeof(bool), typeof(ComingFromScreenCategory) })]
    public class Panel_Inventory_Examine_Enable_Patch
    {
        public static void Postfix(ref Panel_Inventory_Examine __instance, bool enable, ComingFromScreenCategory cameFrom)
        {
            if (Settings.options.modEnabled && LitharsMendingMain.buttonRepairMain != null)
            {
                if(__instance.m_GearItem && __instance.m_GearItem.m_ClothingItem && __instance.m_GearItem.CurrentHP == 0)
                {
                    LitharsMendingMain.buttonRepairMain.SetActive(enable);
                    LitharsMendingMain.buttonRepairMain.transform.localPosition = new Vector3(0, -60, 0);
                }                
            }           
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(Panel_Inventory_Examine), "OnSelectRepairButton")]
    public class OnSelectRepairButton_ClickButtonPatch
    {
        public static void Postfix(ref Panel_Inventory_Examine __instance)
        {
            if (Settings.options.modEnabled && LitharsMendingMain.buttonRepairMain != null)
            {
                if (__instance.m_GearItem && __instance.m_GearItem.m_ClothingItem && __instance.m_GearItem.CurrentHP == 0)
                {
                    LitharsMendingMain.repairPanel.SetActive(true);
                    LitharsMendingMain.repairPanelButtons.SetActive(true);

                    float chanceSuccessfullRepair = __instance.GetChanceSuccessfullRepair(__instance.m_GearItem);
                    float chanceActionSuccess = __instance.GetChanceActionSuccess(chanceSuccessfullRepair);
                    Repairable repairable = __instance.m_GearItem.m_Repairable;
                    string expandedDurationString = Utils.GetExpandedDurationString(__instance.GetModifiedRepairDuration(repairable, repairable.m_DurationMinutes));

                    __instance.m_Repair_AmountLabel.text = __instance.GetConditionIncreaseFromRepair(__instance.m_GearItem) + "%";
                    __instance.m_Repair_ChanceSuccessLabel.text = chanceActionSuccess.ToString("F0") + "%";
                    __instance.m_Repair_ConditionCapLabel.text = repairable.m_RepairConditionCap.ToString("F0") + "%";
                    __instance.m_Repair_TimeLabel.text = expandedDurationString;
                    __instance.UpdateWeightAndConditionLabels();
                }
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(Panel_Inventory_Examine), "GetChanceSuccessfullRepair", new Type[] { typeof(GearItem) })]
    public class GetChanceSuccessfullRepair_Patch
    {
        public static void Postfix(ref Panel_Inventory_Examine __instance, ref GearItem gi, ref float __result)
        {
            if (Settings.options.modEnabled)
            {
                if (__instance.m_GearItem && __instance.m_GearItem.m_ClothingItem && __instance.m_GearItem.CurrentHP == 0)
                {
                    if (Settings.options.difficultyPenaltyEnabled && __instance.m_GearItem.m_Repairable)
                    {
                        if (Settings.options.difficultyPenalty < 100)
                        {
                            int temp = 100 - Settings.options.difficultyPenalty;
                            __result = __result * (temp / 100);
                        }
                    }
                }
            }
        }
    }

    //int GetModifiedRepairDuration(Repairable r, int baseMinutes)
    [HarmonyLib.HarmonyPatch(typeof(Panel_Inventory_Examine), "GetModifiedRepairDuration", new Type[] { typeof(Repairable), typeof(int) })]
    public class GetModifiedRepairDuration_Patch
    {
        public static void Postfix(ref Panel_Inventory_Examine __instance, ref Repairable r, ref int baseMinutes, ref int __result)
        {
            if (Settings.options.modEnabled)
            {
                if (__instance.m_GearItem && __instance.m_GearItem.m_ClothingItem && __instance.m_GearItem.CurrentHP == 0)
                {
                    if (Settings.options.timePenaltyEnabled && __instance.m_GearItem.m_Repairable)
                    {
                        if(Settings.options.timePenalty > 100)
                        {
                            __result = (int)Math.Round(__result * (Settings.options.timePenalty / 100f));
                        }
                    }
                }
            }
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(Panel_Inventory_Examine), "GetConditionIncreaseFromRepair", new Type[] { typeof(GearItem) })]
    public class GetConditionIncreaseFromRepair_Patch
    {
        public static void Postfix(ref Panel_Inventory_Examine __instance, ref GearItem gi, ref float __result)
        {
            if (Settings.options.modEnabled)
            {
                if (__instance.m_GearItem && __instance.m_GearItem.m_ClothingItem && __instance.m_GearItem.CurrentHP == 0)
                {
                    if (Settings.options.conditionPenaltyEnabled && __instance.m_GearItem.m_Repairable)
                    {
                        if (Settings.options.conditionPenalty < 100)
                        {
                            int temp = 100 - Settings.options.conditionPenalty;
                            __result = __result * (temp / 100);
                        }
                    }
                }
            }
        }
    }
}

  
