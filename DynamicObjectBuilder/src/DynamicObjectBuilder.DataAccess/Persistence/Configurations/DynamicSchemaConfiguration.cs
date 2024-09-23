using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicObjectBuilder.DataAccess.Persistence.Configurations;
internal class DynamicSchemaConfiguration : IEntityTypeConfiguration<DynamicSchema>
{
    public void Configure(EntityTypeBuilder<DynamicSchema> builder)
    {
        builder.ToTable("DynamicSchema");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("DynamicSchemaId")
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnName("DynamicSchemaName");

        builder.HasIndex(x => x.Name)
            .IsUnique();


        builder.OwnsMany(x => x.Fields, navigatingProp =>
        {
            navigatingProp.ToJson();
        });

        

    }
}
