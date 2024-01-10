using CodeBase.Infrastructure;
using CodeBase.Services.Ads;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI MoneyText;
        public RewardedAdItem RewardedAdItem;

        public void Construct(IAdsService adsService)
        {
            RewardedAdItem.Construct(adsService, _persistentProgressService);
        }

        protected override void Initialize()
        {
            RewardedAdItem.Initialize();
            RefreshMoneyText();
        }

        protected override void SubscribeUpdates()
        {
            RewardedAdItem.Subscribe();
        }

        protected override void Cleanup()
        {
            RewardedAdItem.Cleanup();
        }

        public void RefreshMoneyText()
        {
            MoneyText.text = playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}