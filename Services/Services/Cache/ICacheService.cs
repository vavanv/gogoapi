using System;
using System.Collections.Generic;
using Services.Models.Common;
using Services.Services.Common;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        void UpdateStopDetail(string code, string stop);
        void UpdateShapes(string shapes);
    }
}