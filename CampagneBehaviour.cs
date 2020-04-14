using CultureChanger.Classes;
using CultureChanger.Screens;
using Helpers;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace CultureChanger.Behaviours
{
    internal class CultureChangerCampaignBehaviour : CampaignBehaviorBase
    {
        public static readonly CultureChangerCampaignBehaviour Instance = new CultureChangerCampaignBehaviour();

        public override void RegisterEvents()
        {
            // this.acrePropertiesMap = new Dictionary<string, AcreProperties>();
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
            // CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, MapEvent>(this.OnRaidCompleted));
        }

        public override void SyncData(IDataStore dataStore)
        {
/*            dataStore.SyncData<Dictionary<string, AcreProperties>>("acrePropertiesMap", ref this.acrePropertiesMap);
            throw new NotImplementedException();*/
        }

        private void OnSessionLaunched(CampaignGameStarter obj)
        {
            this.createCultureChangerSettlementMenus(obj);
        }

        private void createCultureChangerSettlementMenus(CampaignGameStarter obj)
        {
            obj.AddGameMenuOption("town", "town_culture_change", "Change Town Culture",
                new GameMenuOption.OnConditionDelegate(CultureChangerCampaignBehaviour.game_menu_town_change_culture_on_condition),
                new GameMenuOption.OnConsequenceDelegate(CultureChangerCampaignBehaviour.game_menu_town_change_culture_on_consequence),
                false, -1, false);
        }

        public static bool game_menu_town_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            Settlement currentSettlement = Settlement.CurrentSettlement;
            bool disableOption;
            TextObject disabledText;
            return MenuHelper.SetOptionProperties(args, Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(currentSettlement, SettlementAccessModel.SettlementAction.ManageTown, out disableOption, out disabledText), disableOption, disabledText);
        }

        public static void game_menu_town_change_culture_on_consequence(MenuCallbackArgs args)
        {
            CultureChangerProperties properties = new CultureChangerProperties(Settlement.CurrentSettlement.StringId);
            ScreenManager.PushScreen(new CultureChangerScreen(ref properties));
        }

        public class MySaveDefiner : SaveableTypeDefiner
        {
            public MySaveDefiner() : base(52357712)
            {
            }
        }
    }
}
