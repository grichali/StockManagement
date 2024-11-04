import React from "react";
import { Link } from "react-router-dom";
const Sidebar = () => {
  return (
    <aside className="w-64 bg-gray-800 text-white p-6">
      <h2 className="text-2xl font-bold text-center">Admin Dashboard</h2>
      <ul className="mt-6">
        <li className="mb-4">
          <Link to="/dashboard/home" className="hover:text-gray-300">
            Home
          </Link>
        </li>
        <li className="mb-4">
          <Link to="/dashboard/orders" className="hover:text-gray-300">
            Orders
          </Link>
        </li>
        <li className="mb-4">
          <Link to="/dashboard/products" className="hover:text-gray-300">
            Products
          </Link>
        </li>
        <li className="mb-4">
          <Link to="/dashboard/Analytics" className="hover:text-gray-300">
            Analytics
          </Link>
        </li>
        <li className="mb-4">
          <Link to="/dashboard/settings" className="hover:text-gray-300">
            Settings
          </Link>
        </li>
      </ul>
    </aside>
  );
};
export default Sidebar;
