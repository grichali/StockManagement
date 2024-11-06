import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import {CartProvider} from "./Context/CartContext"
import SignUp from "./pages/auth/signup";
import LogIn from "./pages/auth/login";
import Products from "./pages/dashboard/products";
import Dashboard from "./pages/dashboard/Dashboard";
import CategoriesPage from "./pages/user/CategoriesPages";
import CategoryDetailPage from "./pages/user/CategoryDetailsPage";
import Cart from "./components/cart";

function App() {
  return (
    <CartProvider>
    <div className="App ">
      <BrowserRouter>
        <Routes>
          <Route path="/signup" element={<SignUp />} />
          <Route path="/login" element={<LogIn />} />
          <Route path="/dashboard/*" element={<Dashboard />} />
          <Route path="/user/categories" element={<CategoriesPage />} />
          <Route path="/user/produits/:id" element={<CategoryDetailPage/>} />
        </Routes>
      </BrowserRouter>
    </div>
    </CartProvider>

  );
}

export default App;
