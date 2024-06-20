using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using AutoMapper;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Domain.UnitTests.Services
{
    public class SpaceshipServiceTests
    {
        private readonly Mock<ISpaceshipRepository> spaceshipRepositoryMock;
        private readonly Mock<IMapper> mapper;
        private readonly SpaceshipService sut;

        private readonly ICustomizationComposer<Spaceship> spaceshipComposer;
        private readonly ICustomizationComposer<SpaceshipCreateDTO> spaceshipCreateDTOComposer;
        private readonly ICustomizationComposer<SpaceshipUpdateDTO> spaceshipUpdateDTOComposer;
        private readonly ICustomizationComposer<SpaceshipReadDTO> spaceshipReadDTOComposer;

        public SpaceshipServiceTests()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            spaceshipRepositoryMock = fixture.Freeze<Mock<ISpaceshipRepository>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            spaceshipComposer = fixture.Build<Spaceship>();
            spaceshipCreateDTOComposer = fixture.Build<SpaceshipCreateDTO>();
            spaceshipUpdateDTOComposer = fixture.Build<SpaceshipUpdateDTO>();
            spaceshipReadDTOComposer = fixture.Build<SpaceshipReadDTO>();

            sut = fixture.Build<SpaceshipService>().Create();
        }

        [Theory]
        [ClassData(typeof(SpaceshipCreateTestData))]
        public async Task CreateAsync_ShouldReturnCorrectValue_WhenCreationIsFinished(SpaceshipReadDTO? spaceshipReadDTO, bool returnValue)
        {
            // given
            var spaceship = spaceshipComposer.Create();
            var spaceshipCreateDTO = spaceshipCreateDTOComposer.Create();

            spaceshipRepositoryMock.Setup(repo => repo.CreateAsync(spaceship)).ReturnsAsync(returnValue);
            mapper.Setup(map => map.Map<Spaceship>(spaceshipCreateDTO)).Returns(spaceship);
            mapper.Setup(map => map.Map<SpaceshipReadDTO>(spaceship)).Returns(spaceshipReadDTO);

            // when
            var result = await sut.CreateAsync(spaceshipCreateDTO);

            // then
            result.Should().Be(spaceshipReadDTO);
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
            var spaceship = spaceshipComposer.Create();
            var spaceshipUpdateDTO = spaceshipUpdateDTOComposer.Create();

            spaceshipRepositoryMock.Setup(repo => repo.UpdateAsync(spaceship)).ReturnsAsync(returnValue);
            mapper.Setup(map => map.Map<Spaceship>(spaceshipUpdateDTO)).Returns(spaceship);

            // when
            var result = await sut.UpdateAsync(spaceshipUpdateDTO);

            // then
            result.Should().Be(returnValue);
        }

        [Theory]
        [ClassData(typeof(SpaceshipTestData))]
        public async Task GetAsync_ShouldReturnSpaceshipOrNull_WhenGuidIsProvided(Spaceship? spaceship, SpaceshipReadDTO? spaceshipDTO)
        {
            // given
            var guid = Guid.NewGuid();
            spaceshipRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(spaceship);
            mapper.Setup(mapper => mapper.Map<SpaceshipReadDTO>(spaceship)).Returns(spaceshipDTO);

            // when
            var result = await sut.GetAsync(guid);

            // then
            result.Should().Be(spaceshipDTO);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSpaceships()
        {
            // given
            var spaceships = new List<Spaceship>
            {
                    spaceshipComposer.Create(),
                    spaceshipComposer.Create()
            };

            var spaceshipDTOs = new List<SpaceshipReadDTO>
            {
                    spaceshipReadDTOComposer.Create(),
                    spaceshipReadDTOComposer.Create()
            };

            spaceshipRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(spaceships);
            mapper.Setup(map => map.Map<IEnumerable<SpaceshipReadDTO>>(spaceships)).Returns(spaceshipDTOs);

            // when
            var result = await sut.GetAllAsync();

            // then
            result.Should().BeEquivalentTo(spaceshipDTOs);
        }

        public class SpaceshipTestData : TheoryData<Spaceship?, SpaceshipReadDTO?>
        {
            public SpaceshipTestData()
            {
                var fixture = new Fixture();
                var spaceship = fixture.Build<Spaceship>().Create();
                var spaceshipDTO = fixture.Build<SpaceshipReadDTO>().Create(); 

                Add(spaceship, spaceshipDTO);
                Add(null, null);
            }
        }

        public class SpaceshipCreateTestData : TheoryData<SpaceshipReadDTO?, bool>
        {
            public SpaceshipCreateTestData()
            {
                var fixture = new Fixture();
                var spaceshipReadDTO = fixture.Build<SpaceshipReadDTO>().Create();

                Add(spaceshipReadDTO, true);
                Add(null, false);
            }
        }
    }
}