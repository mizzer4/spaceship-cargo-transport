using AutoFixture;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Domain.UnitTests.Services
{
    public class SpaceshipServiceTests
    {
        private readonly Mock<ISpaceshipRepository> spaceshipRepositoryMock;
        private readonly SpaceshipService spaceshipService;

        public SpaceshipServiceTests()
        {
            spaceshipRepositoryMock = new Mock<ISpaceshipRepository>();
            spaceshipService = new SpaceshipService(spaceshipRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTrue_WhenCreationIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>().Create();

            spaceshipRepositoryMock.Setup(repo => repo.CreateAsync(spaceship)).ReturnsAsync(true);

            // when
            var result = await spaceshipService.CreateAsync(spaceship);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenCreationIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>().Create();

            spaceshipRepositoryMock.Setup(repo => repo.CreateAsync(spaceship)).ReturnsAsync(false);

            // when
            var result = await spaceshipService.CreateAsync(spaceship);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            // given
            var spaceshipId = new Guid();

            spaceshipRepositoryMock.Setup(repo => repo.DeleteAsync(spaceshipId)).ReturnsAsync(true);

            // when
            var result = await spaceshipService.DeleteAsync(spaceshipId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionIsUnsuccessful()
        {
            // given
            var spaceshipId = new Guid();

            spaceshipRepositoryMock.Setup(repo => repo.DeleteAsync(spaceshipId)).ReturnsAsync(false);

            // when
            var result = await spaceshipService.DeleteAsync(spaceshipId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>().Create();

            var spaceshipRepositoryMock = new Mock<ISpaceshipRepository>();
            spaceshipRepositoryMock.Setup(repo => repo.UpdateAsync(spaceship)).ReturnsAsync(true);

            var spaceshipService = new SpaceshipService(spaceshipRepositoryMock.Object);

            // when
            var result = await spaceshipService.UpdateAsync(spaceship);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenDeletionIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>().Create();

            var spaceshipRepositoryMock = new Mock<ISpaceshipRepository>();
            spaceshipRepositoryMock.Setup(repo => repo.UpdateAsync(spaceship)).ReturnsAsync(false);

            var spaceshipService = new SpaceshipService(spaceshipRepositoryMock.Object);

            // when
            var result = await spaceshipService.UpdateAsync(spaceship);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnspaceship_WhenCorrectGuidIsProvided()
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>().Create();
            var guid = Guid.NewGuid();

            spaceshipRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(spaceship);

            // when
            var result = await spaceshipService.GetAsync(guid);

            // then
            result.Should().Be(spaceship);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenIncorrectGuidIsProvided()
        {
            // given
            var fixture = new Fixture();
            Spaceship spaceship = null;
            var guid = Guid.NewGuid();           

            spaceshipRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(spaceship);

            // when
            var result = await spaceshipService.GetAsync(guid);

            // then
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSpaceships()
        {
            // given
            var fixture = new Fixture();
            var spaceships = new List<Spaceship>
            {
                    fixture.Build<Spaceship>().Create(),
                    fixture.Build<Spaceship>().Create()
            };

            spaceshipRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(spaceships);

            // when
            var result = await spaceshipService.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(spaceships);
        }
    }
}