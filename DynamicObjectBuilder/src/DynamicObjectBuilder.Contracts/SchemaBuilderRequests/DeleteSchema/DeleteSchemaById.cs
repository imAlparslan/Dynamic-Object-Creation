using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests.DeleteSchema;
public sealed class DeleteSchemaById
{
    public Guid Id { get; set; }
}
