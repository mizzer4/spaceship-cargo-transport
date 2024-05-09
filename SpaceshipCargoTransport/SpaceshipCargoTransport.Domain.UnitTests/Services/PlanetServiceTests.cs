using AutoFixture;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Domain.UnitTests.Services
{
    public class PlanetServiceTests
    {
        private readonly Mock<IPlanetRepository> planetRepositoryMock;
        private readonly PlanetService planetService;

        public PlanetServiceTests()
        {
            planetRepositoryMock = new Mock<IPlanetRepository>();
            planetService = new PlanetService(planetRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTrue_WhenCreationIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            planetRepositoryMock.Setup(repo => repo.CreateAsync(planet)).ReturnsAsync(true);

            // when
            var result = await planetService.CreateAsync(planet);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenCreationIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            planetRepositoryMock.Setup(repo => repo.CreateAsync(planet)).ReturnsAsync(false);

            // when
            var result = await planetService.CreateAsync(planet);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            planetRepositoryMock.Setup(repo => repo.DeleteAsync(planet)).ReturnsAsync(true);

            // when
            var result = await planetService.DeleteAsync(planet);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            planetRepositoryMock.Setup(repo => repo.DeleteAsync(planet)).ReturnsAsync(false);

            // when
            var result = await planetService.DeleteAsync(planet);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            var planetRepositoryMock = new Mock<IPlanetRepository>();
            planetRepositoryMock.Setup(repo => repo.UpdateAsync(planet)).ReturnsAsync(true);

            var planetService = new PlanetService(planetRepositoryMock.Object);

            // when
            var result = await planetService.UpdateAsync(planet);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenDeletionIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            var planetRepositoryMock = new Mock<IPlanetRepository>();
            planetRepositoryMock.Setup(repo => repo.UpdateAsync(planet)).ReturnsAsync(false);

            var planetService = new PlanetService(planetRepositoryMock.Object);

            // when
            var result = await planetService.UpdateAsync(planet);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnPlanet_WhenCorrectGuidIsProvided()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();
            var guid = Guid.NewGuid();

            planetRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(planet);

            // when
            var result = await planetService.GetAsync(guid);

            // then
            result.Should().Be(planet);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenIncorrectGuidIsProvided()
        {
            // given
            var fixture = new Fixture();
            Planet planet = null;
            var guid = Guid.NewGuid();           

            planetRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(planet);

            // when
            var result = await planetService.GetAsync(guid);

            // then
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPlanets()
        {
            // given
            var fixture = new Fixture();
            var planets = new List<Planet>
            {
                    fixture.Build<Planet>().Create(),
                    fixture.Build<Planet>().Create()
            };

            planetRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(planets);

            // when
            var result = await planetService.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(planets);
        }
    }
}