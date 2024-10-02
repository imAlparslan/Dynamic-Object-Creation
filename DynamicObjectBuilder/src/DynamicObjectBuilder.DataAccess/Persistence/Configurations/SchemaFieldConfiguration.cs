using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicObjectBuilder.DataAccess.Persistence.Configurations;
internal class SchemaFieldConfiguration : IEntityTypeConfiguration<SchemaField>
{
    public void Configure(EntityTypeBuilder<SchemaField> builder)
    {
        builder.ToTable("SchemaField");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.Owner);

        builder.HasOne(x => x.DynamicSchema);

        //builder.Property(x => x.DynamicSchema)
        //    .IsRequired(false);

    }
}
