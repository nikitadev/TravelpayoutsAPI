using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public interface INameDataInfo
    {
        string Name { get; set; }
    }

    public interface IDataInfo : INameDataInfo
    {
        string Code { get; set; }
    }
}
