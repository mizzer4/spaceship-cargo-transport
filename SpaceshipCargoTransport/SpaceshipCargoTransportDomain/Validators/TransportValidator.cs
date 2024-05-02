using SpaceshipCargoTransport.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipCargoTransport.Domain.Validators
{
    internal class TransportValidator : ITransportValidator
    {
        public bool IsValid(Transport transport)
        {
            // TODO
            return true;
        }
    }
}
