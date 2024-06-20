using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using AutoMapper;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;
using System.Numerics;

namespace SpaceshipCargoTransport.Domain.UnitTests.Services
{
    public class PlanetServiceTests
    {
        private readonly Mock<IPlanetRepository> planetRepositoryMock;
        private readonly Mock<IMapper> mapper;
        private readonly PlanetService sut;

        private readonly ICustomizationComposer<Planet> planetComposer;
        private readonly ICustomizationComposer<PlanetCreateDTO> planetCreateDTOComposer;
        private readonly ICustomizationComposer<PlanetUpdateDTO> planetUpdateDTOComposer;
        private readonly ICustomizationComposer<PlanetReadDTO> planetReadDTOComposer;

        public PlanetServiceTests()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            planetRepositoryMock = fixture.Freeze<Mock<IPlanetRepository>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            planetComposer = fixture.Build<Planet>();
            planetCreateDTOComposer = fixture.Build<PlanetCreateDTO>();
            planetUpdateDTOComposer = fixture.Build<PlanetUpdateDTO>();
            planetReadDTOComposer = fixture.Build<PlanetReadDTO>();

            sut = fixture.Build<PlanetService>().Create();         
        }

        [Theory]
        [ClassData(typeof(PlanetCreateTestData))]
        public async Task CreateAsync_ShouldReturnCorrectValue_WhenCreationIsFinished(PlanetReadDTO? planetReadDTO, bool returnValue)
        {
            // given
            var planet = planetComposer.Create();
            var planetCreateDTO = planetCreateDTOComposer.Create();

            planetRepositoryMock.Setup(repo => repo.CreateAsync(planet)).ReturnsAsync(returnValue);
            mapper.Setup(map => map.Map<Planet>(planetCreateDTO)).Returns(planet);
            mapper.Setup(map => map.Map<PlanetReadDTO>(planet)).Returns(planetReadDTO);

            // when
            var result = await sut.CreateAsync(planetCreateDTO);

            // then
            result.Should().Be(planetReadDTO);
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
            var result = await sut.DeleteAsync(planetId);

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
            var planetUpdateDTO = planetUpdateDTOComposer.Create();

            planetRepositoryMock.Setup(repo => repo.UpdateAsync(planet)).ReturnsAsync(returnValue);
            mapper.Setup(map => map.Map<Planet>(planetUpdateDTO)).Returns(planet);

            // when
            var result = await sut.UpdateAsync(planetUpdateDTO);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [ClassData(typeof(PlanetTestData))]
        public async Task GetAsync_ShouldReturnPlanetOrNull_WhenGuidIsProvided(Planet? planet, PlanetReadDTO? planetDTO)
        {
            // given
            var guid = Guid.NewGuid();
            planetRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(planet);
            mapper.Setup(map => map.Map<PlanetReadDTO>(planet)).Returns(planetDTO);

            // when
            var result = await sut.GetAsync(guid);

            // then
            result.Should().Be(planetDTO);
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

            var planetDTOs = new List<PlanetReadDTO>
            {
                    planetReadDTOComposer.Create(),
                    planetReadDTOComposer.Create()
            };

            planetRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(planets);
            mapper.Setup(map => map.Map<IEnumerable<PlanetReadDTO>>(planets)).Returns(planetDTOs);

            // when
            var result = await sut.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(planetDTOs);
        }

        public class PlanetTestData : TheoryData<Planet?, PlanetReadDTO?>
        {
            public PlanetTestData()
            {
                var fixture = new Fixture();
                var planet = fixture.Build<Planet>().Create();
                var planetDTO = fixture.Build<PlanetReadDTO>().Create();

                Add(planet, planetDTO);
                Add(null, null);
            }
        }

        public class PlanetCreateTestData : TheoryData<PlanetReadDTO?, bool>
        {
            public PlanetCreateTestData()
            {
                var fixture = new Fixture();
                var planetReadDTO = fixture.Build<PlanetReadDTO>().Create();

                Add(planetReadDTO, true);
                Add(null, false);
            }
        }
    }
}