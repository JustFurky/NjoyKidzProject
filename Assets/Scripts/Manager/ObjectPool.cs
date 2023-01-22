using System.Collections.Generic;
using UnityEngine;
using CodeBase.Components;
using CodeBase.Managers;

namespace CodeBase.Components
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;
        [SerializeField] private CoinScript _coinPrefab;
        [SerializeField] private int _countOfCoins;
        private List<CoinScript> _coins = new List<CoinScript>();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            CreatePool();
        }
        private void CreatePool()
        {
            for (int i = 1; i < _countOfCoins + 1; i++)
            {
                CoinScript currentCoin = Instantiate(_coinPrefab);
                currentCoin.Initialize(i);
                _coins.Add(currentCoin);
            }
        }
        public void DeleteFromList(CoinScript coinScript)
        {
            _coins.Remove(coinScript);
            if (_coins.Count == 0)
            {
                Invoke("RestartScene", .5f);
                Debug.Log("Game End");
            }
        }
        private void RestartScene()
        {
            UIManager.Instance.RestartButton();
        }
    }
}
