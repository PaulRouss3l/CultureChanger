using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using CultureChanger.Behaviours;

namespace CultureChanger
{
    public class Main : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            Campaign campaign = game.GameType as Campaign;
            if (campaign != null)
            {
                CampaignGameStarter gameInitializer = (CampaignGameStarter)gameStarterObject;
                this.AddBehaviors(gameInitializer);
            }
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002692 File Offset: 0x00000892
        private void AddBehaviors(CampaignGameStarter gameInitializer)
        {
            gameInitializer.AddBehavior(CultureChangerCampaignBehaviour.Instance);
        }
    }
}
