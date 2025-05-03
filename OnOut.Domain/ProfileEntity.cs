using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public abstract class ProfileEntity: BaseEntity
    {
        public byte[] PrimaryImage { get; set; } = Array.Empty<byte>();
        public byte[] BannerImage { get; set; } = Array.Empty<byte>();
    }
}
