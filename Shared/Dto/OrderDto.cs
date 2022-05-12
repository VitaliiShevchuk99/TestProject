using System.Collections.Generic;

namespace Shared.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public List<ProductDto> Products { get; set; }
        public int Sum { get; set; }
    }
}
