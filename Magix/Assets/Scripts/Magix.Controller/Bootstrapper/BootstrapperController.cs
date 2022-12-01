namespace Magix.Controller.Boot
{
    using Bootstrapper;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class BootstrapperController : MonoBehaviour
    {
        private void Start()
        {
            _boot();
        }

        private void _boot()
        {
            Bootstrapper.Boot();

            SceneManager.LoadScene("Match");
        }
    }
}
