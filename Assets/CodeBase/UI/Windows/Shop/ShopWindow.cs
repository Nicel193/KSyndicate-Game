using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.IAP;
using CodeBase.Services.Ads;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI MoneyText;
        public RewardedAdItem RewardedAdItem;
        public ShopItemContainer ShopItemContainer;

        public void Construct(IAdsService adsService, IIAPService iapService, IAssetProvider assetProvider)
        {
            RewardedAdItem.Construct(adsService, _persistentProgressService);
            ShopItemContainer.Construct(iapService, assetProvider);
        }

        protected override void Initialize()
        {
            RewardedAdItem.Initialize();
            ShopItemContainer.Initialize();
            RefreshMoneyText();
        }

        protected override void SubscribeUpdates()
        {
            RewardedAdItem.Subscribe();
            ShopItemContainer.Subscribe();
        }

        protected override void Cleanup()
        {
            RewardedAdItem.Cleanup();
            ShopItemContainer.Cleanup();
        }

        public void RefreshMoneyText()
        {
            MoneyText.text = playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}