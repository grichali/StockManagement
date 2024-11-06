import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import SignUp from "./pages/auth/signup";
import LogIn from "./pages/auth/login";
import Dashboard from "./pages/dashboard/Dashboard";
import CategoriesPage from "./pages/user/CategoriesPages";

function App() {
  return (
    <div className="App ">
      <BrowserRouter>
        <Routes>
          <Route path="/signup" element={<SignUp />} />
          <Route path="/login" element={<LogIn />} />
          <Route path="/dashboard/*" element={<Dashboard />} />
          <Route path="/user/categories" element={<CategoriesPage />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
