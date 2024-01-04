using System;
using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Loot
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Skull;
        public ParticleSystem PickupFx;
        public GameObject TextPopup;
        public TextMeshPro PickupText;

        private Loot _loot;
        private WorldData _worldData;
        private Action _onPicked;
        private bool _picked;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot, Action onPicked)
        {
            _onPicked = onPicked;
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => PickUp();

        private void PickUp()
        {
            if (_picked) return;
            
            _worldData.LootData.Collect(_loot);
            _onPicked?.Invoke();
            _picked = true;

            PlayFx();
            ShowText();
            HideSkull();
            StartCoroutine(StartDestroyTimer());
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1f);
            
            Destroy(this.gameObject);
        }

        private void ShowText()
        {
            PickupText.text = _loot.Value.ToString();
            TextPopup.SetActive(true);
        }

        private void HideSkull() => Skull.SetActive(false);
        private void PlayFx() => Instantiate(PickupFx, Skull.transform);
    }
}