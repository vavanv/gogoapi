using Services.Models.Common;

namespace GoGoApi.CreateData
{
    internal interface ICreateDataFactory
    {
        IMappingData Create();
    }
}