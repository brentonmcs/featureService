using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNet.Mvc;
using NUnit.Framework;
using WH.FeatureService.Api.Controllers;
using WH.FeatureService.Api.Models;
using WH.FeatureService.Api.Services;

namespace ConsoleApplication
{

    public class FeatureSetControllerTest
    {
        private IFeatureSetRepository _featureSetRepository;

        [SetUp]
        public void Setup()
        {
            _featureSetRepository = A.Fake<IFeatureSetRepository>();
        }

        [Test]
        public async Task FeatureControllerReturns304IfLatestVersion()
        {
            var controller = new FeatureSetController(_featureSetRepository);

            var latestVersion = "blah";
            A.CallTo(() => _featureSetRepository.GetLatestVersion(A<int>.Ignored, A<string>.Ignored)).Returns(latestVersion);

            var result = await controller.Get(new FeatureSetRequest() { KnownVersion = latestVersion });

            var httpStatusCodeResult = result as HttpStatusCodeResult;
            Assert.NotNull(httpStatusCodeResult);
            Assert.AreEqual(304, httpStatusCodeResult.StatusCode);
        }

        [Test]
        public async Task ReturnsNewFeatureSetIfNotLatestVersion()
        {
            var controller = new FeatureSetController(_featureSetRepository);

            A.CallTo(() => _featureSetRepository.GetLatestVersion(A<int>.Ignored, A<string>.Ignored)).Returns("wrongVersion");

            var result = await controller.Get(new FeatureSetRequest() { KnownVersion = "newVersion" });

            var httpStatusCodeResult = result as HttpOkObjectResult;

            Assert.NotNull(httpStatusCodeResult);
            Assert.AreEqual(200, httpStatusCodeResult.StatusCode);

            var feature = httpStatusCodeResult.Value as FeatureSet;
            Assert.NotNull(feature);

            A.CallTo(() => _featureSetRepository.GetSet(A<int>.Ignored, A<string>.Ignored, A<int>.Ignored)).MustHaveHappened();

        }
    }
}