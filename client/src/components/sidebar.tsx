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
          <Link to="categories" className="hover:text-gray-300">
            Categories
          </Link>
        </li>
        <li className="mb-4">
          <Link to="products" className="hover:text-gray-300">
            Products
          </Link>
        </li>
        <li className="mb-4">
          <Link to="users" className="hover:text-gray-300">
            Users
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
