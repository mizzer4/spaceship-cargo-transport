using AutoFixture;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Services;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Validators;
using SpaceshipCargoTransport.Domain.Notifications;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;

namespace transportCargoTransport.Domain.UnitTests.Services
{
    public class TransportServiceTests
    {
        private readonly Mock<ITransportRepository> transportRepositoryMock;
        private readonly Mock<ITransportValidator> transportValidatorMock;
        private readonly TransportService sut;
        private readonly ICustomizationComposer<Transport> transportComposer; 

        public TransportServiceTests()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            transportComposer = fixture.Build<Transport>();

            transportRepositoryMock = fixture.Freeze<Mock<ITransportRepository>>();
            transportValidatorMock = fixture.Freeze<Mock<ITransportValidator>>();

            sut = fixture.Build<TransportService>().Create();
        }


        [Fact]
        public async Task GetDetailsAsync_ShouldReturnTransport_WhenGuidProvided()
        {
            // given
            var guid = Guid.NewGuid();
            var transport = transportComposer.Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(transport);

            // when
            var result = await sut.GetDetailsAsync(guid);

            // then
            result.Should().Be(transport);
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, true, true)]
        public async Task RegisterNewAsync_ShouldReturnTrue_WhenCreationAndValidationIsSuccessful(bool creationResult, bool validationResult, bool expectedResult)
        {
            // given
            var transport = transportComposer.Create();

            transportRepositoryMock.Setup(repo => repo.CreateAsync(transport)).ReturnsAsync(creationResult);
            transportValidatorMock.Setup(val => val.IsValid(transport)).Returns(validationResult);

            // when
            var result = await sut.RegisterNewAsync(transport);

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

    }
}