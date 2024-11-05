import React from "react";

// Static data representing products in the selected category
const sampleProducts = [
  {
    id: 1,
    name: "Product 1",
    description: "Description of Product 1",
    price: 29.99,
    imageUrl: "https://via.placeholder.com/150",
  },
  {
    id: 2,
    name: "Product 2",
    description: "Description of Product 2",
    price: 19.99,
    imageUrl: "https://via.placeholder.com/150",
  },
  // Add more products as needed
];

// Define CategoryDetailPage component
const CategoryDetailPage: React.FC<{ categoryName: string; categoryDescription: string }> = ({
  categoryName,
  categoryDescription,
}) => {
  return (
    <div className="p-6">
      <h2 className="text-2xl font-bold mb-4">{categoryName}</h2>
      <p className="mb-6">{categoryDescription}</p>
      
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {sampleProducts.map((product) => (
          <div key={product.id} className="bg-white p-4 rounded shadow">
            <img src={product.imageUrl} alt={product.name} className="w-full h-32 object-cover mb-4 rounded" />
            <h3 className="text-lg font-semibold">{product.name}</h3>
            <p className="text-gray-500">{product.description}</p>
            <p className="text-gray-800 font-bold mb-4">${product.price.toFixed(2)}</p>
            <button
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
              onClick={() => handleAddToCart(product)}
            >
              Add to Cart
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

// Add to Cart handler (for now, just a console log)
const handleAddToCart = (product: any) => {
  console.log("Added to cart:", product);
};

export default CategoryDetailPage;
