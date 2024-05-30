using AutoFixture;
using AutoFixture.Dsl;
using FluentAssertions;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Validators;

namespace SpaceshipCargoTransport.Domain.UnitTests.Validators
{
    public class TransportValidatorTests
    {
        private readonly TransportValidator sut;
        private readonly ICustomizationComposer<Spaceship> spaceshipComposer;
        private readonly ICustomizationComposer<Transport> transportComposer;
        private readonly ICustomizationComposer<Planet> planetComposer;

        public TransportValidatorTests() 
        {
            var fixture = new Fixture();

            spaceshipComposer = fixture.Build<Spaceship>();
            transportComposer = fixture.Build<Transport>();
            planetComposer = fixture.Build<Planet>();

            sut = fixture.Build<TransportValidator>().Create();
        }

        [Theory]
        [InlineData(100, 80)]
        [InlineData(100, 100)]
        [InlineData(100, 0)]
        public void IsSpaceshipCargoSpaceEnough_ShouldReturnTrue_WhenCargoSpaceIsEnough(int cargoStorageSpace, int cargoSize)
        {
            // given
            var spaceship = spaceshipComposer
                .With(x => x.CargoStorageSpace, cargoStorageSpace)
                .Create();
            var transport = transportComposer
                .With(x => x.CargoSize, cargoSize)
                .With(x => x.Spaceship, spaceship)
                .Create();

            // when
            var result = sut.IsSpaceshipCargoSpaceEnough(transport);

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
            var spaceship = spaceshipComposer
                .With(x => x.CargoStorageSpace, cargoStorageSpace)
                .Create();
            var transport = transportComposer
                .With(x => x.CargoSize, cargoSize)
                .With(x => x.Spaceship, spaceship)
                .Create();

            // when
            var result = sut.IsSpaceshipCargoSpaceEnough(transport);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void IsDestinationDifferentPlanet_ShouldReturnTrue_WhenDifferentPlanet()
        {
            // given
            var planet1 = planetComposer.Create();
            var planet2 = planetComposer.Create();

            var transport = transportComposer
                .With(x => x.StartingLocation, planet1)
                .With(x => x.EndingLocation, planet2)
                .Create();

            // when
            var result = sut.IsDestinationDifferentPlanet(transport);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public void IsDestinationDifferentPlanet_ShouldReturnFalse_WhenSamePlanet()
        {
            // given
            var planet = planetComposer.Create();

            var transport = transportComposer
                .With(x => x.StartingLocation, planet)
                .With(x => x.EndingLocation, planet)
                .Create();

            // when
            var result = sut.IsDestinationDifferentPlanet(transport);

            // then
            result.Should().BeFalse();
        }

    }
}