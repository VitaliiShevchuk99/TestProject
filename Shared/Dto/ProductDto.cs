using System.Collections.Generic;

namespace Shared.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<OrderDto> Products { get; set; }
    }
}
