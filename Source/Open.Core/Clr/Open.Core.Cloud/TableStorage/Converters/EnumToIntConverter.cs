using System;

namespace Open.Core.Cloud.TableStorage
{
    public class EnumToIntConverter : IConverter
    {
        public object ToTarget(object source)
        {
            if (source == null) return source;
            return Convert.ToInt32(source);
        }

        public object ToSource(object target)
        {
            // NB: Integer treated as enum.
            return target;
        }
    }
}
