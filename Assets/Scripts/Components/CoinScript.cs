using UnityEngine;
using TMPro;
using CodeBase.Interfaces;

namespace CodeBase.Components
{
    public class CoinScript : MonoBehaviour, IStackable
    {
        public int CoinNumber;
        private TMP_Text _coinNumberText;
        private ObjectPool _objectPool;
        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _coinNumberText = GetComponentInChildren<TMP_Text>();
        }

        public void Initialize(int number)
        {
            CoinNumber = number;
            _coinNumberText.text = CoinNumber.ToString();
            transform.position = new Vector3(Random.Range(-7, 6), Random.Range(-5, 5), 0);
        }

        public void Stack(int lastNumber)
        {
            //TODO: We can add ParticleSystem for better visual
            _objectPool.DeleteFromList(this);
            transform.gameObject.SetActive(false);
        }
    }
}
