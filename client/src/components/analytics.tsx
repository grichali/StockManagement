import React from "react";
import orders from "../assets/img/orders.svg";
import profite from "../assets/img/profite.svg";
import products from "../assets/img/products.svg";
import { Bar, Doughnut, Line } from "react-chartjs-2";
import { Chart, registerables } from "chart.js";
Chart.register(...registerables);
const Analytics = () => {
  const totalOrders = 120;
  const totalProfit = 5000;
  const totalProducts = 200;
  const chiffreDAffaire = 15000;
  const topcategoriessell = [
    { category: "Electronics", orders: 30 },
    { category: "Books", orders: 20 },
    { category: "Clothing", orders: 50 },
  ];
  const topSellingProducts = [
    { product: "Smartphone", sales: 150 },
    { product: "Laptop", sales: 100 },
    { product: "Headphones", sales: 75 },
    { product: "Tablet", sales: 50 },
    { product: "Smartwatch", sales: 25 },
  ];

  const inventoryLevels = [
    { product: "Smartphone", stock: 30 },
    { product: "Laptop", stock: 15 },
    { product: "Headphones", stock: 50 },
    { product: "Tablet", stock: 25 },
    { product: "Smartwatch", stock: 10 },
  ];
  const categories = topcategoriessell.map((item) => item.category);
  const xx = topcategoriessell.map((item) => item.orders);

  const chartData = {
    labels: categories,
    datasets: [
      {
        label: "Orders",
        data: xx,
        backgroundColor: "rgba(75, 192, 192, 0.6)",
        borderWidth: 1,
      },
    ],
  };

  const doughnutChartData = {
    labels: topSellingProducts.map((item) => item.product),
    datasets: [
      {
        label: "Top Selling Products",
        data: topSellingProducts.map((item) => item.sales),
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4BC0C0",
          "#9966FF",
        ],
        hoverBackgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4BC0C0",
          "#9966FF",
        ],
      },
    ],
  };
  const horizontalBarChartData = {
    labels: inventoryLevels.map((item) => item.product),
    datasets: [
      {
        label: "Inventory Levels",
        data: inventoryLevels.map((item) => item.stock),
        backgroundColor: "rgba(153, 102, 255, 0.6)",
        borderColor: "rgba(153, 102, 255, 1)",
        borderWidth: 1,
      },
    ],
  };
  const profitsOverMonthsData = {
    labels: [
      "January",
      "February",
      "March",
      "April",
      "May",
      "June",
      "July",
      "August",
      "September",
      "October",
      "November",
      "December",
    ],
    datasets: [
      {
        label: "Monthly Profit",
        data: [400, 450, 500, 600, 700, 800, 750, 900, 1000, 1100, 1200, 1300], // Sample data
        fill: false,
        backgroundColor: "rgba(75, 192, 192, 1)",
        borderColor: "rgba(75, 192, 192, 1)",
        tension: 0.1,
      },
    ],
  };

  const horizontalBarChartOptions: {
    indexAxis: "y";
    scales: { x: { beginAtZero: boolean } };
  } = {
    indexAxis: "y",
    scales: {
      x: {
        beginAtZero: true,
      },
    },
  };
  return (
    <>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6 mb-6">
        <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
          <img
            src={orders}
            alt="Total Orders Icon"
            className="w-11 h-11 mr-2 rounded-full"
          />
          <div className="flex flex-col items-start">
            <p className="text-2xl font-bold">{totalOrders}</p>
            <h3 className="text-lg font-semibold">Total Orders</h3>
          </div>
        </div>

        <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
          <img
            src={profite}
            alt="Total Orders Icon"
            className="w-11 h-11 mr-2 rounded-full"
          />
          <div className="flex flex-col items-start">
            <p className="text-2xl font-bold">${totalProfit}</p>
            <h3 className="text-lg font-semibold ">Total Profit</h3>
          </div>
        </div>

        <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
          <img
            src={products}
            alt="Total Orders Icon"
            className="w-11 h-11 mr-2 rounded-full"
          />
          <div className="flex flex-col items-start">
            <p className="text-2xl font-bold">{totalProducts}</p>
            <h3 className="text-lg font-semibold">Total Products</h3>
          </div>
        </div>

        <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
          <img
            src={profite}
            alt="Total Orders Icon"
            className="w-11 h-11 mr-2 rounded-full"
          />
          <div className="flex flex-col items-start">
            <p className="text-2xl font-bold">${chiffreDAffaire}</p>
            <h3 className="text-lg font-semibold">Chiffre d'Affaire</h3>
          </div>
        </div>
      </div>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-2 gap-6">
        <div className="bg-white p-4 rounded shadow h-64">
          <h3 className="text-lg font-semibold mb-4">Categories Sales</h3>
          <div className="w-full h-full">
            <Bar data={chartData} options={{ maintainAspectRatio: false }} />
          </div>
        </div>

        <div className="bg-white p-4 rounded shadow h-64">
          <h3 className="text-lg font-semibold mb-4">Top Selling Products</h3>
          <div className="w-full h-48">
            <Doughnut
              data={doughnutChartData}
              options={{ maintainAspectRatio: false }}
            />
          </div>
        </div>

        <div className="bg-white p-4 rounded shadow h-64">
          <h3 className="text-lg font-semibold mb-4">Profits Over Months</h3>
          <div className="w-full h-full">
            <Line
              data={profitsOverMonthsData}
              options={{ maintainAspectRatio: false }}
            />
          </div>
        </div>

        <div className="bg-white p-4 rounded shadow h-64">
          {" "}
          {/* Inventory Levels by Product */}
          <h3 className="text-lg font-semibold mb-4">
            Inventory Levels by Product
          </h3>
          <div className="w-full h-full">
            <Bar
              data={horizontalBarChartData}
              options={horizontalBarChartOptions}
            />
          </div>
        </div>
      </div>
    </>
  );
};
export default Analytics;
