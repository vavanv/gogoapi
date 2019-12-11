using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Services.Models.Common;

namespace Services.CreateData
{
    internal class ShapesCreteDataResolver: ICreteDataResolver
    {
        public List<IMappingData> BuildData(string file)
        {
            var list = new System.Collections.Generic.List<IMappingData>();
            var numRow = 0;
            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (numRow == 0)
                        {
                            numRow = 1;
                            continue;
                        }

                        var values = line.Split(',');
                        list.Add(new ShapesMappingData
                        {
                            ShapeId = values[0],
                            Lat = Convert.ToDecimal(values[1]),
                            Lon = Convert.ToDecimal(values[2]),
                            Sec = Convert.ToInt32(values[3])
                        });
                    }
                }
            }

            return list;
        }
    }
}
