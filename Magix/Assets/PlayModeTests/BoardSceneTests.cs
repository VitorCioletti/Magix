namespace PlayModeTests
{
    using System.Collections;
    using Magix.Controller.Match.Board;
    using Magix.DependencyInjection;
    using Magix.Domain.Interface.Board;
    using Magix.Service;
    using Magix.Service.Interface;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.TestTools;

    public class BoardSceneTests
    {
        private BoardController _boardController;

        [SetUp]
        public void SetUp()
        {
            _setUpResolver();
            _setUpScene();
        }

        [UnityTest]
        public IEnumerator MustCreateAllTiles()
        {
            yield return new WaitForSeconds(3);

            IBoard board = Resolver.GetService<IMatchService>().Board;

            TileController[] createdTiles = _boardController.gameObject.GetComponentsInChildren<TileController>();

            Assert.AreEqual(board.Tiles.Length, createdTiles.Length);

            yield return null;
        }

        private void _setUpScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("PlayModeTests/Scenes/BoardTests");

            operation!.completed += OnCompleted;

            void OnCompleted(AsyncOperation obj)
            {
                GameObject board = GameObject.Find("Board");

                _boardController = board.GetComponent<BoardController>();
            }
        }

        private void _setUpResolver()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IMatchService, MatchService>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Resolver.SetServiceProvider(serviceProvider);
        }
    }
}
