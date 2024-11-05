import React from "react";

import Sidebar from "../../components/sidebar";
import { Route, Routes } from "react-router-dom";
import Analytics from "../../components/analytics";
import Products from "../../components/products";
import Categories from "../../components/categories";

const Dashboard: React.FC = () => {


  return (
    <div className="flex h-screen">
      <Sidebar />
      <main className="flex-1 p-6 bg-gray-100">
        <Routes>
          <Route path="/" element={<Analytics />} />
          <Route path="/products" element={<Products />} />
          <Route path="/categories" element={<Categories />} />
        </Routes>
      </main>
    </div>
  );
};

export default Dashboard;
