namespace Magix.Controller.Boot
{
    using Bootstrapper;
    using UnityEngine;

    public class BootstrapperController : MonoBehaviour
    {
        public void Boot()
        {
            Bootstrapper.Boot();
        }
    }
}
