using TMPro;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI MoneyText;

        protected override void Initialize() =>
            RefreshMoneyText();

        protected override void SubscribeUpdates()
        {
            
        }

        protected override void Cleanup()
        {
            
        }

        public void RefreshMoneyText()
        {
            MoneyText.text = playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}