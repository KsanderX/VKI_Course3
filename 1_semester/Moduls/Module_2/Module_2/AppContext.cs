using Module_2.Moduls;

namespace Module_2
{
    public class AppContext
    {
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            new Product()
            {
                Id=1,
                Count=1,
                Description = "Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый",
                Discount=20,
                Manufacturer="Производитель",
                Supplier = "Обувь для вас",
                Name="Ботинки",
                Сategory = "Мужская обувь",
                ImagePath = @"F:\ВКИ\VKI_Course3\1_semester\Moduls\Module_2\Module_2\Images\Boots\1.jpg",
                Price = 1000,
                Unit="шт.",
                SKU="F635R4"
            },
            new Product()
            {
                Id=2,
                Count=10,
                Description = "",
                Discount=0,
                Manufacturer="Производитель",
                Name="Ботинки",
                Сategory = "Мужская обувь",
                ImagePath = @"F:\ВКИ\VKI_Course3\1_semester\Moduls\Module_2\Module_2\Images\Boots\2.jpg",
                Price= 900,
                Unit="шт.",
                SKU="F635R4"
            },
            new Product()
            {
                Id=3,
                Count=0,
                Description = "",
                Discount=15,
                Manufacturer="Производитель",
                Name="Name",
                Сategory = "Мужская обувь",
                ImagePath = @"F:\ВКИ\VKI_Course3\1_semester\Moduls\Module_2\Module_2\Images\Boots\3.jpg",
                Price= 700,
                Unit="шт.",
                SKU="F635R4"
            }       
        };
    }
}
