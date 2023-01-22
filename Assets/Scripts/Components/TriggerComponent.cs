using UnityEngine;
using CodeBase.Interfaces;

namespace CodeBase.Components
{
    public class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private int _lastStackedCoinNumber = 0;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<IStackable>() != null)
            {
                StackableTriggerFunc(collision.gameObject);
            }
        }
        private void StackableTriggerFunc(GameObject triggeredObject)
        {
            triggeredObject.GetComponent<IStackable>().Stack(_lastStackedCoinNumber);
            if (triggeredObject.GetComponent<CoinScript>().CoinNumber == _lastStackedCoinNumber + 1)
            {
                _lastStackedCoinNumber = triggeredObject.GetComponent<CoinScript>().CoinNumber;
                return;
            }
            else
                Debug.Log("Wrong Number Stacked");
        }
    }
}
