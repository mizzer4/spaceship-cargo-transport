using AutoFixture;
using FluentAssertions;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Validators;

namespace SpaceshipCargoTransport.Domain.UnitTests.Validators
{
    public class TransportValidatorTests
    {
        [Theory]
        [InlineData(100, 80)]
        [InlineData(100, 100)]
        [InlineData(100, 0)]
        public void IsSpaceshipCargoSpaceEnough_ShouldReturnTrue_WhenCargoSpaceIsEnough(int cargoStorageSpace, int cargoSize)
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>()
                .With(x => x.cargoStorageSpace, cargoStorageSpace)
                .Create();
            var transport = fixture.Build<Transport>()
                .With(x => x.CargoSize, cargoSize)
                .With(x => x.Spaceship, spaceship)
                .Create();

            var validator = new TransportValidator();

            // when
            var result = validator.IsSpaceshipCargoSpaceEnough(transport);

            // then
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(100, 120)]
        [InlineData(100, 101)]
        [InlineData(100, 9999)]
        public void IsSpaceshipCargoSpaceEnough_ShouldReturnFalse_WhenCargoSpaceIsNotEnough(int cargoStorageSpace, int cargoSize)
        {
            // given
            var fixture = new Fixture();
            var spaceship = fixture.Build<Spaceship>()
                .With(x => x.cargoStorageSpace, cargoStorageSpace)
                .Create();
            var transport = fixture.Build<Transport>()
                .With(x => x.CargoSize, cargoSize)
                .With(x => x.Spaceship, spaceship)
                .Create();

            var validator = new TransportValidator();

            // when
            var result = validator.IsSpaceshipCargoSpaceEnough(transport);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void IsDestinationDifferentPlanet_ShouldReturnTrue_WhenDifferentPlanet()
        {
            // given
            var fixture = new Fixture();
            var planet1 = fixture.Build<Planet>().Create();
            var planet2 = fixture.Build<Planet>().Create();

            var transport = fixture.Build<Transport>()
                .With(x => x.StartingLocation, planet1)
                .With(x => x.EndingLocation, planet2)
                .Create();

            var validator = new TransportValidator();

            // when
            var result = validator.IsDestinationDifferentPlanet(transport);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public void IsDestinationDifferentPlanet_ShouldReturnFalse_WhenSamePlanet()
        {
            // given
            var fixture = new Fixture();
            var planet = fixture.Build<Planet>().Create();

            var transport = fixture.Build<Transport>()
                .With(x => x.StartingLocation, planet)
                .With(x => x.EndingLocation, planet)
                .Create();

            var validator = new TransportValidator();

            // when
            var result = validator.IsDestinationDifferentPlanet(transport);

            // then
            result.Should().BeFalse();
        }

    }
}