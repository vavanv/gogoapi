
using GoGoApi.Models;

using Services.StopDetail;

namespace GoGoApi.Mappers
{
    public interface IStopDetailMapper
    {
        StopModel MapFrom(Stop itemEntity);
        Stop MapFrom(StopModel model, Stop itemEntity);
    }
}