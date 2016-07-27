using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kstar.sharp.domain.Entities;

namespace kstar.sharp.sqlite.Interfaces
{
    public interface IInverterDataRepository
    {
        List<InverterDataGranular> Get(DateTimeOffset start, DateTimeOffset end);
        InverterDataGranular GetLatest();
        void Add(InverterDataGranular inverterData);
    }
}
