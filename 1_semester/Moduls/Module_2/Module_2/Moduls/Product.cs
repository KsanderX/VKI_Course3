﻿namespace Module_2.Moduls
{
    public class Product
    {
        public int Id { get; set; }
        /// <summary>
        /// Артикул
        /// </summary>
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// единица измерения
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        public double Price { get; set; } = 0.0;
        /// <summary>
        /// поставщик
        /// </summary>
        public string Supplier { get; set; } = string.Empty;
        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; } = string.Empty;
        public string Сategory { get; set; } = string.Empty;
        public int Discount { get; set; } = 0;
        public int Count { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = @"F:\ВКИ\VKI_Course3\1_semester\Moduls\Module_2\Module_2\Images\picture.png ";
        public override string ToString()
        {
            return $"{Id} - {Name} - {Price}";
        }
    }
}
