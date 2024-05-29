using AutoFixture;
using AutoFixture.Dsl;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;
using System.Numerics;
using Xunit;

namespace SpaceshipCargoTransport.Domain.UnitTests.Services
{
    public class PlanetServiceTests
    {
        private readonly Mock<IPlanetRepository> planetRepositoryMock;
        private readonly PlanetService planetService;
        private readonly ICustomizationComposer<Planet> planetComposer;

        public PlanetServiceTests()
        {
            planetRepositoryMock = new Mock<IPlanetRepository>();
            planetService = new PlanetService(planetRepositoryMock.Object);

            var fixture = new Fixture();
            planetComposer = fixture.Build<Planet>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateAsync_ShouldReturnCorrectValue_WhenCreationIsFinished(bool returnValue)
        {
            // given
            var planet = planetComposer.Create();
            planetRepositoryMock.Setup(repo => repo.CreateAsync(planet)).ReturnsAsync(returnValue);

            // when
            var result = await planetService.CreateAsync(planet);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsync_ShouldReturnCorrectValue_WhenDeletionIsFinished(bool returnValue)
        {
            // given
            var planetId = Guid.NewGuid();
            planetRepositoryMock.Setup(repo => repo.DeleteAsync(planetId)).ReturnsAsync(returnValue);

            // when
            var result = await planetService.DeleteAsync(planetId);

            // then
            result.Should().Be(returnValue);
        }


        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsync_ShouldReturnCorrectValue_WhenUpdateIsFinished(bool returnValue)
        {
            // given
            var planet = planetComposer.Create();
            planetRepositoryMock.Setup(repo => repo.UpdateAsync(planet)).ReturnsAsync(returnValue);

            // when
            var result = await planetService.UpdateAsync(planet);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [ClassData(typeof(PlanetTestData))]
        public async Task GetAsync_ShouldReturnPlanetOrNull_WhenGuidIsProvided(Planet? planet, Planet? expectedResult)
        {
            // given
            var guid = Guid.NewGuid();
            planetRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(planet);

            // when
            var result = await planetService.GetAsync(guid);

            // then
            result.Should().Be(expectedResult);
        }

        public class PlanetTestData : TheoryData<Planet?, Planet?>
        {
            public PlanetTestData()
            {
                var fixture = new Fixture();
                var planet = fixture.Build<Planet>().Create();                

                Add(planet, planet);
                Add(null, null);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPlanets()
        {
            // given
            var planets = new List<Planet>
            {
                    planetComposer.Create(),
                    planetComposer.Create()
            };

            planetRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(planets);

            // when
            var result = await planetService.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(planets);
        }
    }
}