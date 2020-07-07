namespace Project1.Business {

    public class ProductModel {

        public string Name { get; set; }
        public double Price { get; set; }

        public string DisplayPrice {
            get => $"${Price:0.00}";
        }
    }
}
