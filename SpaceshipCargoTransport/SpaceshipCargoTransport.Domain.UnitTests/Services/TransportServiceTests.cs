using AutoFixture;
using FluentAssertions;
using Moq;
using SpaceshipCargoTransport.Domain.Services;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Validators;
using SpaceshipCargoTransport.Domain.Notifications;

namespace transportCargoTransport.Domain.UnitTests.Services
{
    public class TransportServiceTests
    {
        private readonly Mock<ITransportRepository> transportRepositoryMock;
        private readonly Mock<ITransportValidator> transportValidatorMock;
        private readonly Mock<ITransportNotificationService> transportNotificationServiceMock;
        private readonly TransportService transportService;

        public TransportServiceTests()
        {
            transportRepositoryMock = new Mock<ITransportRepository>();
            transportValidatorMock = new Mock<ITransportValidator>();
            transportNotificationServiceMock = new Mock<ITransportNotificationService>();
            transportService = new TransportService(transportRepositoryMock.Object, transportValidatorMock.Object, transportNotificationServiceMock.Object);
        }


        [Fact]
        public async Task GetDetailsAsync_ShouldReturnTransport_WhenGuidProvided()
        {
            // given
            var fixture = new Fixture();
            var guid = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(guid)).ReturnsAsync(transport);

            // when
            var result = await transportService.GetDetailsAsync(guid);

            // then
            result.Should().Be(transport);
        }

        [Fact]
        public async Task RegisterNewAsync_ShouldReturnTrue_WhenCreationAndValidationIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.CreateAsync(transport)).ReturnsAsync(true);
            transportValidatorMock.Setup(val => val.IsValid(transport)).Returns(true);

            // when
            var result = await transportService.RegisterNewAsync(transport);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterNewAsync_ShouldReturnFalse_WhenCreationIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.CreateAsync(transport)).ReturnsAsync(false);
            transportValidatorMock.Setup(val => val.IsValid(transport)).Returns(true);

            // when
            var result = await transportService.RegisterNewAsync(transport);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task RegisterNewAsync_ShouldReturnFalse_WhenValidationIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.CreateAsync(transport)).ReturnsAsync(true);
            transportValidatorMock.Setup(val => val.IsValid(transport)).Returns(false);

            // when
            var result = await transportService.RegisterNewAsync(transport);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToCargoLoadingAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToCargoLoadingAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetToCargoLoadingAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToCargoLoadingAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToCargoLoadingAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.SetToCargoLoadingAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToInFlightAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToInFlightAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetToInFlightAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToInFlightAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToInFlightAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.SetToInFlightAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToCargoUnloadingAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToCargoUnloadingAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetToCargoUnloadingAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToCargoUnloadingAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToCargoUnloadingAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.SetToCargoUnloadingAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToFinishedAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToFinishedAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetToFinishedAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToFinishedAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToFinishedAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.SetToFinishedAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToLostAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToLostAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SetToLostAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.SetToLostAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SetToLostAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.SetToLostAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CancelAsync_ShouldReturnTrue_WhenStatusChangeIsSuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.CancelAsync(transportId);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public async Task CancelAsync_ShouldReturnFalse_WhenTransportIsNotFound()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            Transport transport = null;

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(true);

            // when
            var result = await transportService.CancelAsync(transportId);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CancelAsync_ShouldReturnFalse_WhenStatusChangeIsUnsuccessful()
        {
            // given
            var fixture = new Fixture();
            var transportId = new Guid();
            var transport = fixture.Build<Transport>().Create();

            transportRepositoryMock.Setup(repo => repo.GetAsync(transportId)).ReturnsAsync(transport);
            transportRepositoryMock.Setup(repo => repo.UpdateAsync(transport)).ReturnsAsync(false);

            // when
            var result = await transportService.CancelAsync(transportId);

            // then
            result.Should().BeFalse();
        }
    }
}