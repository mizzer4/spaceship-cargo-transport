using AutoFixture;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Models;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using SpaceshipCargoTransport.Domain.Validators;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Services;
using SpaceshipCargoTransport.Application.DTOs.Transport;
using AutoMapper;

namespace transportCargoTransport.Domain.UnitTests.Services
{
    public class TransportServiceTests
    {
        private readonly Mock<ITransportRepository> transportRepositoryMock;
        private readonly Mock<ITransportValidator> transportValidatorMock;
        private readonly Mock<IMapper> mapper;
        private readonly TransportService sut;

        private readonly ICustomizationComposer<Transport> transportComposer;
        private readonly ICustomizationComposer<TransportCreateDTO> transportCreateDTOComposer;
        private readonly ICustomizationComposer<TransportReadDTO> transportReadDTOComposer;

        public TransportServiceTests()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            transportComposer = fixture.Build<Transport>();

            transportRepositoryMock = fixture.Freeze<Mock<ITransportRepository>>();
            transportValidatorMock = fixture.Freeze<Mock<ITransportValidator>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            transportCreateDTOComposer = fixture.Build<TransportCreateDTO>();
            transportReadDTOComposer = fixture.Build<TransportReadDTO>();

            sut = fixture.Build<TransportService>().Create();
        }


        [Fact]
        public async Task GetDetailsAsync_ShouldReturnTransport_WhenGuidProvided()
        {
            // given
            var guid = Guid.NewGuid();
            var transport = transportComposer.Create();
            var transportDTO = transportReadDTOComposer.Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(transport);
            mapper.Setup(mapper => mapper.Map<TransportReadDTO>(transport)).Returns(transportDTO);

            // when
            var result = await sut.GetDetailsAsync(guid);

            // then
            result.Should().Be(transportDTO);
        }

        [Theory]
        [ClassData(typeof(TransportCreateTestData))]
        public async Task RegisterNewAsync_ShouldReturnTransportReadDTO_WhenCreationAndValidationIsSuccessful(TransportReadDTO? transportReadDTO, bool creationResult, bool validationResult)
        {
            // given
            var transport = transportComposer.Create();
            var transportCreateDTO = transportCreateDTOComposer.Create();

            transportRepositoryMock.Setup(repo => repo.CreateAsync(transport)).ReturnsAsync(creationResult);
            transportValidatorMock.Setup(val => val.IsValid(transport)).Returns(validationResult);
            mapper.Setup(map => map.Map<Transport>(transportCreateDTO)).Returns(transport);
            mapper.Setup(map => map.Map<TransportReadDTO>(transport)).Returns(transportReadDTO);

            // when
            var result = await sut.RegisterNewAsync(transportCreateDTO);

            // then
            result.Should().Be(transportReadDTO);
        }

        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task SetToCargoLoadingAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = Guid.NewGuid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.SetToCargoLoadingAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }


        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task SetToInFlightAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = Guid.NewGuid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.SetToInFlightAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task SetToCargoUnloadingAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = Guid.NewGuid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.SetToCargoUnloadingAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task SetToFinishedAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = Guid.NewGuid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.SetToFinishedAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task SetToLostAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = Guid.NewGuid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.SetToLostAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }


        [Theory]
        [ClassData(typeof(TransportTestData))]
        public async Task CancelAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful(Transport? transport, bool updateResult, bool expectedResult)
        {
            // given
            var transportId = new Guid();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(updateResult);

            // when
            var result = await sut.CancelAsync(transportId);

            // then
            result.Should().Be(expectedResult);
        }

        public class TransportTestData : TheoryData<Transport?, bool, bool>
        {
            public TransportTestData()
            {
                var fixture = new Fixture();
                var Transport = fixture.Build<Transport>().Create();

                Add(Transport, false, false);
                Add(Transport, true, true);
                Add(null, false, false);
            }
        }

        public class TransportCreateTestData : TheoryData<TransportReadDTO?, bool, bool>
        {
            public TransportCreateTestData()
            {
                var fixture = new Fixture();
                var transportReadDTO = fixture.Build<TransportReadDTO>().Create();

                Add(transportReadDTO, true, true);
                Add(null, false, false);
                Add(null, true, false);
                Add(null, false, true);
            }
        }
    }
}