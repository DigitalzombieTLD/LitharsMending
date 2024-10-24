using UnityEngine;
using ModSettings;
using MelonLoader;

namespace LitharsMending
{
    internal class LitharsMendingSettings : JsonModSettings
    {        
        [Section("General")]
     
        [Name("Enabled")]
        [Description("Enable/disable complete mod functionality")]
        public bool modEnabled = true;


        [Section("Difficulty Penalty")]

        [Name("Enable")]
        [Description("Decreases chance of success to repair a ruined item")]
        public bool difficultyPenaltyEnabled = true;

        [Name("Percentage")]
        [Description("Decrease of success in percent (relative to default)")]
        [Slider(0, 99)]
        public int difficultyPenalty = 50;        

        [Section("Time Penalty")]

        [Name("Enable")]
        [Description("Increases the time it takes to repair a ruined item")]
        public bool timePenaltyEnabled = true;

        [Name("Percentage")]
        [Description("Duration of repair action on ruined item in percent (relative to default)")]
        [Slider(100, 300)]
        public int timePenalty = 150;

        [Section("Condition Penalty")]

        [Name("Enable")]
        [Description("Limits the condition thats regained on repairing a ruined item")]
        public bool conditionPenaltyEnabled = true;

        [Name("Condition Penalty")]
        [Description("Decrease condition on ruined item in percent (relative to default)")]
        [Slider(1, 100)]
        public int conditionPenalty = 50;

        protected override void OnConfirm()
        {
            base.OnConfirm();
        }
    }

    internal static class Settings
    {
        public static LitharsMendingSettings options;

        public static void OnLoad()
        {
            options = new LitharsMendingSettings();
            options.AddToModSettings("Lithars Mending");
        }
    }
}
