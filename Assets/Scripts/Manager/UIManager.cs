using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void RestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
