using System;

namespace Danske.MTM.Common.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string entityName, object key)
           : base($"Entity '{entityName}' with key '{key}' not found.")
        {
        }

        public NotFoundException(Type objectType, object key)
            : this(objectType.Name, key)
        {
        }
    }
}
