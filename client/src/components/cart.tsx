import React from "react";
import { useCart } from "../Context/CartContext";

const Cart: React.FC = () => {
  const { cartItems, removeFromCart, calculateTotal, submitOrder } = useCart();

  return (
    <div className="cart">
      <h2>Shopping Cart</h2>
      {cartItems.map((item) => (
        <div key={item.id} className="cart-item">
          <span>{item.name}</span>
          <span>x{item.quantity}</span>
          <span>${(item.price * item.quantity).toFixed(2)}</span>
          <button onClick={() => removeFromCart(item.id)}>Remove</button>
        </div>
      ))}
      <div className="cart-total">
        <span>Total:</span>
        <span>${calculateTotal().toFixed(2)}</span>
      </div>
      <button onClick={submitOrder}>Submit Order</button>
    </div>
  );
};

export default Cart;
