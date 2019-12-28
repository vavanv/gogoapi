using System;

namespace Services.CreateData
{
    public interface ICreateDataFactory
    {
        ICreteDataResolver Create(MappingDataType type);
    }
}