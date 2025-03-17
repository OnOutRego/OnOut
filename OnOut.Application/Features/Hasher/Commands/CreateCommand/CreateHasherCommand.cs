using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.CreateCommand
{
    public class CreateHasherCommand: IRequest<Guid>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomeCountry { get; set; }
        public Guid DietaryChoiceId { get; set; }
    }
}
