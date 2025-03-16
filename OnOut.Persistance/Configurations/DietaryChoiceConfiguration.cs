using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Persistance.Configurations
{
    public class DietaryChoiceConfiguration : IEntityTypeConfiguration<DietaryChoice>
    {
        public void Configure(EntityTypeBuilder<DietaryChoice> builder)
        {
            builder.HasData(
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "None",
                    Description = "No dietary restrictions."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Vegetarian",
                    Description = "Excludes meat, but may include dairy and eggs."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Vegan",
                    Description = "Excludes all animal products, including dairy, eggs, and honey."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Pescatarian",
                    Description = "Excludes meat but includes fish and seafood."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Gluten-Free",
                    Description = "Excludes foods containing gluten (e.g., wheat, barley, rye)."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Keto",
                    Description = "Low-carb, high-fat diet."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Paleo",
                    Description = "Focuses on whole foods, excludes processed foods and grains."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Halal",
                    Description = "Complies with Islamic dietary laws."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Kosher",
                    Description = "Complies with Jewish dietary laws."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Lacto-Vegetarian",
                    Description = "Vegetarian diet including dairy but excluding eggs."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Ovo-Vegetarian",
                    Description = "Vegetarian diet including eggs but excluding dairy."
                },
                new DietaryChoice
                {
                    Id = Guid.NewGuid(),
                    Name = "Lacto-Ovo Vegetarian",
                    Description = "Vegetarian diet including both dairy and eggs."
                }

                );
        }
    }
}
