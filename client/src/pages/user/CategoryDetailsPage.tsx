import React, { useEffect, useState } from "react";
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
}

const CategoryDetailPage: React.FC = () => {
  const { id: categoryId } = useParams<{ id: string }>();
  const location = useLocation();
  const { categoryName, categoryDescription } = location.state as { categoryName: string; categoryDescription: string };

  const { addToCart } = useCart();
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [quantities, setQuantities] = useState<{ [key: number]: number }>({});

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Product/getproductbycategorie/${categoryId}`);
        if (!response.ok) {
          throw new Error("Failed to fetch products");
        }
        const data = await response.json();
        setProducts(data.$values);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [categoryId]);

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

        {loading ? (
          <p>Loading...</p>
        ) : error ? (
          <p className="text-red-500">Error: {error}</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {products.map((product) => (
              <div key={product.id} className="bg-white p-4 rounded shadow">
                <img src={product.imageUrl} alt={product.name} className="w-full h-24 object-cover mb-2 rounded" />
                <h3 className="text-lg font-semibold">{product.name}</h3>
                <p className="text-gray-500">{product.description}</p>
                <p className="text-gray-800 font-bold mb-4">${product.price.toFixed(2)}</p>
                <p className="text-sm text-gray-600">Available: {product.quantity}</p>

                <div className="flex items-center mb-4">
                  <label htmlFor={`quantity-${product.id}`} className="text-sm mr-2">Quantity:</label>
                  <input
                    id={`quantity-${product.id}`}
                    type="number"
                    min="1"
                    max={product.quantity}
                    value={quantities[product.id] || 1}
                    onChange={(e) =>
                      handleQuantityChange(product.id, Math.min(parseInt(e.target.value, 10), product.quantity))
                    }
                    className="border rounded w-16 p-1 text-center"
                  />
                </div>

                <button
                  className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                  onClick={() => handleAddToCart(product)}
                  disabled={quantities[product.id] > product.quantity}
                >
                  Add to Cart
                </button>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default CategoryDetailPage;
