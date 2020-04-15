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
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void OnSessionLaunched(CampaignGameStarter obj)
        {
            this.createCultureChangerSettlementMenus(obj);
        }

        private void createCultureChangerSettlementMenus(CampaignGameStarter obj)
        {
            obj.AddGameMenuOption("town", "town_culture_change", "Change town culture",
                new GameMenuOption.OnConditionDelegate(CultureChangerCampaignBehaviour.game_menu_town_change_culture_on_condition),
                new GameMenuOption.OnConsequenceDelegate(CultureChangerCampaignBehaviour.game_menu_change_culture_on_consequence),
                false, 5, false);
            obj.AddGameMenuOption("castle", "castle_culture_change", "Change castle culture",
                new GameMenuOption.OnConditionDelegate(CultureChangerCampaignBehaviour.game_menu_castle_change_culture_on_condition),
                new GameMenuOption.OnConsequenceDelegate(CultureChangerCampaignBehaviour.game_menu_change_culture_on_consequence),
                false, 7, false);
        }

        public static bool game_menu_town_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            Settlement currentSettlement = Settlement.CurrentSettlement;
            bool disableOption;
            TextObject disabledText;
            return MenuHelper.SetOptionProperties(args, Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(currentSettlement, SettlementAccessModel.SettlementAction.ManageTown, out disableOption, out disabledText), disableOption, disabledText);
        }

        private static bool game_menu_castle_change_culture_on_condition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Manage;
            return Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan && Settlement.CurrentSettlement.IsCastle;
        }

        public static void game_menu_change_culture_on_consequence(MenuCallbackArgs args)
        {
            CultureChangerProperties properties = new CultureChangerProperties(Settlement.CurrentSettlement.StringId);
            ScreenManager.PushScreen(new CultureChangerScreen(ref properties));
        }
    }
}
