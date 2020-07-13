﻿namespace Project1.DataAccess.Model {
    public partial class StoreStock : IModel {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
