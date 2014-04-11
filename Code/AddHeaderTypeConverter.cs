using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Nop.Plugin.Misc.HtmlOptimiser.Code
{
    public class AddHeaderTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var result = Enumerable.Empty<AddHeader>().ToList();

                if (!string.IsNullOrEmpty((string)value))
                {
                    result = JsonConvert.DeserializeObject<List<AddHeader>>((string)value);
                }
                
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string result = string.Empty;

                if (((List<AddHeader>)value) != null)
                {
                    result = JsonConvert.SerializeObject(value);
                }

                return result;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
