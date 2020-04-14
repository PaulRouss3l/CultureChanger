using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using CultureChanger.Behaviours;

namespace CultureChanger
{
    public class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("TestMainMenuOption", new TextObject("Click on this", null), 9990, () =>
            {
                InformationManager.DisplayMessage(new InformationMessage("btn clicked"));
            }, false) );
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            Campaign campaign = game.GameType as Campaign;
            if (campaign != null)
            {
                CampaignGameStarter gameInitializer = (CampaignGameStarter)gameStarterObject;
                /*this.AddModels(gameStarterObject);*/
                this.AddBehaviors(gameInitializer);

            }
        }

        protected virtual void AddModels(IGameStarter gameStarterObject)
        {
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002692 File Offset: 0x00000892
        private void AddBehaviors(CampaignGameStarter gameInitializer)
        {
            gameInitializer.AddBehavior(CultureChangerCampaignBehaviour.Instance);
            InformationManager.DisplayMessage(new InformationMessage("Added behavior"));
        }
    }
}
