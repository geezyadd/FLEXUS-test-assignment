using System.Collections.Generic;

namespace Features.VfxModule.Scripts
{
    public interface IVfxPoolRegistry
    {
        IReadOnlyCollection<VfxPool> All { get; }

        VfxPool Get(VfxType type);
    }
}
