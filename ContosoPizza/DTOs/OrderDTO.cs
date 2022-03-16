﻿namespace ContosoPizza.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}