import React from "react";

import Sidebar from "../../components/sidebar";
import { Route, Routes } from "react-router-dom";
import Analytics from "../../components/analytics";

const Dashboard: React.FC = () => {
  // Static data for now

  return (
    <div className="flex h-screen">
      {/* Sidebar */}
      <Sidebar />
      {/* Main Content */}
      <main className="flex-1 p-6 bg-gray-100">
        <Routes>
          <Route path="/" element={<Analytics />} />
          <Route path="/products" element={<Analytics />} />
        </Routes>
      </main>
    </div>
  );
};

export default Dashboard;
