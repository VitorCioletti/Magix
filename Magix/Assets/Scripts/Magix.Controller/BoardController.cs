namespace Magix.Controller
{
    using System;
    using UnityEngine;

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField]
        private GridController _gridController { get; set; } = default;

        private void Start()
        {
            _gridController.Init();
        }
    }
}
