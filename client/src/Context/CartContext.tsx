import React, { createContext, useContext, useState } from "react";
import Cart from "../components/cart";

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  quantity: number;
}

interface CreateOrderItemDTO {
  ProductId: number;
  Quantity: number;
}

interface CreateOrderDto {
  Items: CreateOrderItemDTO[];
}

interface CartContextType {
  cartItems: Product[];
  addToCart: (product: Product, quantity: number) => void;
  removeFromCart: (productId: number) => void;
  clearCart: () => void;
  calculateTotal: () => number;
  submitOrder: () => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export const CartProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [cartItems, setCartItems] = useState<Product[]>([]);

  const addToCart = (product: Product, quantity: number) => {
    setCartItems((prevItems) => {
      const existingItem = prevItems.find((item) => item.id === product.id);
      if (existingItem) {
        return prevItems.map((item) =>
          item.id === product.id ? { ...item, quantity: item.quantity + quantity } : item
        );
      }
      return [...prevItems, { ...product, quantity }];
    });
  };

  const removeFromCart = (productId: number) => {
    setCartItems((prevItems) => prevItems.filter((item) => item.id !== productId));
  };

  const clearCart = () => {
    setCartItems([]);
  };

  const calculateTotal = () => {
    return cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  };

  const submitOrder = async () => {
    const orderItems: CreateOrderItemDTO[] = cartItems.map((item) => ({
      ProductId: item.id,
      Quantity: item.quantity,
    }));

    const orderDto: CreateOrderDto = {
      Items: orderItems,
    };

    try {
      const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Order/Create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(orderDto),
      });

      if (response.ok) {
        console.log("Order submitted successfully!", JSON.stringify(orderDto));
        clearCart(); // After submission, clear the cart
      } else {
        console.error("Failed to submit order:", response.statusText);
      }
    } catch (error) {
      console.error("Error submitting order:", error);
    }
  };

  return (
    <CartContext.Provider value={{ cartItems, addToCart, removeFromCart, clearCart, calculateTotal, submitOrder }}>
      {children}
      <Cart/>
    </CartContext.Provider>
  ); 
};

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) throw new Error("useCart must be used within a CartProvider");
  return context;
};
