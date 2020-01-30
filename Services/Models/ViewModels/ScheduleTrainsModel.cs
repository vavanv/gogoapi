using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.ViewModels
{
    public class VariantModel
    {
        public string Code { get; set; }
        public string Display { get; set; }
        public string Direction { get; set; }
    }

    public class LineModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<VariantModel> Variant { get; set; }
    }
}
