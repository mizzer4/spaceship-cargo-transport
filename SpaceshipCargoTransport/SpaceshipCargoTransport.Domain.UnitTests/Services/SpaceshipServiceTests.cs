using AutoFixture;
using AutoFixture.Dsl;
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
        private readonly SpaceshipService sut;
        private readonly ICustomizationComposer<Spaceship> spaceshipComposer;

        public SpaceshipServiceTests()
        {
            spaceshipRepositoryMock = new Mock<ISpaceshipRepository>();
            sut = new SpaceshipService(spaceshipRepositoryMock.Object);

            var fixture = new Fixture();
            spaceshipComposer = fixture.Build<Spaceship>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateAsync_ShouldReturnCorrectValue_WhenCreationIsFinished(bool returnValue)
        {
            // given
            var Spaceship = spaceshipComposer.Create();
            spaceshipRepositoryMock.Setup(repo => repo.CreateAsync(Spaceship)).ReturnsAsync(returnValue);

            // when
            var result = await sut.CreateAsync(Spaceship);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsync_ShouldReturnCorrectValue_WhenDeletionIsFinished(bool returnValue)
        {
            // given
            var spaceshipId = Guid.NewGuid();
            spaceshipRepositoryMock.Setup(repo => repo.DeleteAsync(spaceshipId)).ReturnsAsync(returnValue);

            // when
            var result = await sut.DeleteAsync(spaceshipId);

            // then
            result.Should().Be(returnValue);
        }


        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsync_ShouldReturnCorrectValue_WhenUpdateIsFinished(bool returnValue)
        {
            // given
            var Spaceship = spaceshipComposer.Create();
            spaceshipRepositoryMock.Setup(repo => repo.UpdateAsync(Spaceship)).ReturnsAsync(returnValue);

            // when
            var result = await sut.UpdateAsync(Spaceship);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [ClassData(typeof(SpaceshipTestData))]
        public async Task GetAsync_ShouldReturnSpaceshipOrNull_WhenGuidIsProvided(Spaceship? Spaceship, Spaceship? expectedResult)
        {
            // given
            var guid = Guid.NewGuid();
            spaceshipRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(Spaceship);

            // when
            var result = await sut.GetAsync(guid);

            // then
            result.Should().Be(expectedResult);
        }

        public class SpaceshipTestData : TheoryData<Spaceship?, Spaceship?>
        {
            public SpaceshipTestData()
            {
                var fixture = new Fixture();
                var Spaceship = fixture.Build<Spaceship>().Create();

                Add(Spaceship, Spaceship);
                Add(null, null);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSpaceships()
        {
            // given
            var Spaceships = new List<Spaceship>
            {
                    spaceshipComposer.Create(),
                    spaceshipComposer.Create()
            };

            spaceshipRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Spaceships);

            // when
            var result = await sut.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(Spaceships);
        }
    }
}