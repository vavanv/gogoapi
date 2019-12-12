using Services.CreateData;
using Services.Models.Common;

namespace GoGoApi.CreateData
{
    public interface ICreateDataFactory
    {
        ICreteDataResolver Create(MappingDataType type);
    }
}