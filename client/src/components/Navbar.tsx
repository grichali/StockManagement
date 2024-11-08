import React, { useState } from "react";
import { useCart } from "../Context/CartContext";

const Navbar: React.FC = () => {
  const { cartItems, removeFromCart, clearCart, calculateTotal } = useCart();
  const [isCartOpen, setIsCartOpen] = useState(false);

  const handleCheckout = () => {
    console.log("Checkout submitted:", cartItems);
    clearCart(); 
  };

  return (
    <nav className="p-4 bg-blue-500 text-white flex justify-between items-center">
      <h1 className="text-2xl font-bold">E-commerce</h1>

      <div className="relative">
        <button
          onClick={() => setIsCartOpen(!isCartOpen)}
          className="relative bg-gray-800 text-white rounded-full p-2"
        >
          🛒 {cartItems.length}
        </button>

        {isCartOpen && (
          <div className="absolute right-0 mt-2 w-64 bg-white text-black shadow-lg rounded p-4">
            <h3 className="text-lg font-bold mb-2">Cart</h3>
            {cartItems.length === 0 ? (
              <p>No items in cart</p>
            ) : (
              cartItems.map((item) => (
                <div key={item.id} className="flex justify-between items-center mb-2">
                  <span>{item.name}</span>
                  <span>{item.quantity} x ${item.price.toFixed(2)}</span>
                  <button
                    onClick={() => removeFromCart(item.id)}
                    className="text-red-500 text-sm"
                  >
                    Remove
                  </button>
                </div>
              ))
            )}

            <div className="mt-4">
              <div className="flex justify-between items-center mb-2">
                <span className="font-bold">Total: </span>
                <span>${calculateTotal().toFixed(2)}</span>
              </div>
              <button
                onClick={handleCheckout}
                className="w-full bg-blue-500 text-white py-2 rounded mt-2"
              >
                Submit Order
              </button>
            </div>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
