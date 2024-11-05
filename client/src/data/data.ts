interface CategoryDto {
  id: number;
  name: string;
}

interface Product {
  id: number;
  name: string;
  price: number;
  quantity: number;
  imageUrl: string;
  categoryDto?: CategoryDto | null;
}

const products: Product[] = [
  {
    id: 1,
    name: "Laptop",
    price: 999.99,
    quantity: 10,
    imageUrl: "https://example.com/images/laptop.jpg",
    categoryDto: { id: 1, name: "Electronics" },
  },
  {
    id: 2,
    name: "Smartphone",
    price: 699.99,
    quantity: 15,
    imageUrl: "https://example.com/images/smartphone.jpg",
    categoryDto: { id: 1, name: "Electronics" },
  },
  {
    id: 3,
    name: "Headphones",
    price: 199.99,
    quantity: 25,
    imageUrl: "https://example.com/images/headphones.jpg",
    categoryDto: { id: 2, name: "Accessories" },
  },
  {
    id: 4,
    name: "Coffee Maker",
    price: 89.99,
    quantity: 5,
    imageUrl: "https://example.com/images/coffeemaker.jpg",
    categoryDto: { id: 3, name: "Home Appliances" },
  },
  {
    id: 5,
    name: "Backpack",
    price: 49.99,
    quantity: 30,
    imageUrl: "https://example.com/images/backpack.jpg",
    categoryDto: { id: 4, name: "Outdoor" },
  },
];

export default products;
