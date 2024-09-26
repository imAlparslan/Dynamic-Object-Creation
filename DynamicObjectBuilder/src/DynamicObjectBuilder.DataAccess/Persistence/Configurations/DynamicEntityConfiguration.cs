using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicObjectBuilder.DataAccess.Persistence.Configurations;
internal class DynamicEntityConfiguration : IEntityTypeConfiguration<DynamicEntity>
{
    public void Configure(EntityTypeBuilder<DynamicEntity> builder)
    {
        builder.ToTable("DynamicEntity");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("DynamicEntityId")
            .ValueGeneratedNever();


        builder.OwnsMany(x => x.Fields, navigatingProp =>
        {
            navigatingProp.ToJson();
            navigatingProp.OwnsOne(x => x.Value);
        });

    }
}
