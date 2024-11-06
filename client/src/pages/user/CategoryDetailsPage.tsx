import React, { useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import { useCart } from "../../Context/CartContext";
import Navbar from "../../components/Navbar";

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  quantity: number;
  available: number; // Add this if available is used in the code
} 

const sampleProducts: Product[] = [
  {
    id: 1,
    name: "Product 1",
    description: "This is a description for Product 1",
    price: 10.0,
    imageUrl: "/images/product1.jpg",
    quantity: 1,
    available: 10, // Add this value based on the quantity available
  },
  {
    id: 2,
    name: "Product 2",
    description: "This is a description for Product 2",
    price: 15.0,
    imageUrl: "/images/product2.jpg",
    quantity: 1,
    available: 5,
  },
  // Add more products as needed
];

const CategoryDetailPage: React.FC = () => {
  const { id: categoryId } = useParams<{ id: string }>();
  const location = useLocation();
  const { categoryName, categoryDescription } = location.state as { categoryName: string; categoryDescription: string };

  const { addToCart } = useCart();
  const [quantities, setQuantities] = useState<{ [key: number]: number }>({});

  const handleQuantityChange = (productId: number, value: number) => {
    setQuantities((prevQuantities) => ({
      ...prevQuantities,
      [productId]: value,
    }));
  };

  const handleAddToCart = (product: Product) => {
    const quantity = quantities[product.id] || 1;
    addToCart(product, quantity);
  };

  return (
    <div>
      <Navbar />
      <div className="p-6">
      <h2 className="text-2xl font-bold mb-2">{categoryName}</h2>
      <p className="text-gray-600 mb-6">{categoryDescription}</p>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {sampleProducts.map((product) => (
          <div key={product.id} className="bg-white p-4 rounded shadow">
            <img src={product.imageUrl} alt={product.name} className="w-full h-24 object-cover mb-2 rounded" />
            <h3 className="text-lg font-semibold">{product.name}</h3>
            <p className="text-gray-500">{product.description}</p>
            <p className="text-gray-800 font-bold mb-4">${product.price.toFixed(2)}</p>
            <p className="text-sm text-gray-600">Available: {product.available}</p>

            <div className="flex items-center mb-4">
              <label htmlFor={`quantity-${product.id}`} className="text-sm mr-2">Quantity:</label>
              <input
                id={`quantity-${product.id}`}
                type="number"
                min="1"
                max={product.available}
                value={quantities[product.id] || 1}
                onChange={(e) => handleQuantityChange(product.id, Math.min(parseInt(e.target.value), product.available))}
                className="border rounded w-16 p-1 text-center"
              />
            </div>

            <button
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
              onClick={() => handleAddToCart(product)}
              disabled={quantities[product.id] > product.available}
            >
              Add to Cart
            </button>
          </div>
        ))}
      </div>
    </div>
    </div>

  );
};

export default CategoryDetailPage;
