using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private TextMeshProUGUI availableItemsText;
        [SerializeField] private Image icon;

        private ProductDescription _productDescription;

        private IIAPService _iapService;

        public void Construct(IIAPService iapService, IAssetProvider assetProvider,
            ProductDescription productDescription)
        {
            _iapService = iapService;
            _productDescription = productDescription;
            _assetProvider = assetProvider;
            
            Initialize();
        }

        private IAssetProvider _assetProvider;

        public async void Initialize()
        {
            buyButton.onClick.AddListener(OnBuyItemClick);

            priceText.text = _productDescription.ProductConfig.Price;
            quantityText.text = _productDescription.ProductConfig.Quantity.ToString();
            availableItemsText.text = _productDescription.AvaiblePurchaseLeft.ToString();

            if (_productDescription.ProductConfig.ItemType == ItemType.Resurrect)
                quantityText.enabled = false;

            Vector3 scale = icon.transform.localScale;
            icon.sprite = await _assetProvider.Load<Sprite>(_productDescription.ProductConfig.Icon);
            icon.transform.localScale = scale;
        }

        private void OnBuyItemClick() => 
            _iapService.StartPurchase(_productDescription.Id);
    }
}