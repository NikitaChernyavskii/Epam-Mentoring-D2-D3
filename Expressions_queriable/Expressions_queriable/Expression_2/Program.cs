using Expression_2.Mapping;
using Expression_2.Models;

namespace Expression_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<CarEntity, CarModel>();

            var carnEntity = new CarEntity
            {
                Id = 56,
                Name = "Chevy Camaro 1968"
            };

            CarModel res = mapper.Map(carnEntity);
        }
    }
}
