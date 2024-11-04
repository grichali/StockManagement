import React from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart, registerables } from 'chart.js';
import orders from "../../assets/img/orders.svg"
import profite from "../../assets/img/profite.svg"
import products from "../../assets/img/products.svg"
Chart.register(...registerables);

const Dashboard: React.FC = () => {
    // Static data for now
    const totalOrders = 120;
    const totalProfit = 5000;
    const totalProducts = 200;
    const chiffreDAffaire = 15000;

    // Static chart data
    const chartData = {
        labels: ['Category 1', 'Category 2', 'Category 3', 'Category 4'],
        datasets: [
            {
                label: 'Sales',
                data: [120, 150, 80, 200], // Sample sales data
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                ],
                borderWidth: 1,
            },
        ],
    };

    return (
        <div className="flex h-screen">
            {/* Sidebar */}
            <aside className="w-64 bg-gray-800 text-white p-6">
                <h2 className="text-2xl font-bold text-center">Admin Dashboard</h2>
                <ul className="mt-6">
                    <li className="mb-4"><a href="#" className="hover:text-gray-300">Home</a></li>
                    <li className="mb-4"><a href="#" className="hover:text-gray-300">Orders</a></li>
                    <li className="mb-4"><a href="#" className="hover:text-gray-300">Products</a></li>
                    <li className="mb-4"><a href="#" className="hover:text-gray-300">Reports</a></li>
                    <li className="mb-4"><a href="#" className="hover:text-gray-300">Settings</a></li>
                </ul>
            </aside>

            {/* Main Content */}
            <main className="flex-1 p-6 bg-gray-100">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6 mb-6">
                <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
                    <img src={orders} alt="Total Orders Icon" className="w-11 h-11 mr-2 rounded-full" />
                    <div className="flex flex-col items-start">
                        <p className="text-2xl font-bold">{totalOrders}</p>
                        <h3 className="text-lg font-semibold">Total Orders</h3>
                    </div>
                </div>

                <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
                    <img src={profite} alt="Total Orders Icon" className="w-11 h-11 mr-2 rounded-full" />
                    <div className="flex flex-col items-start"> 
                        <p className="text-2xl font-bold">${totalProfit}</p>
                        <h3 className="text-lg font-semibold ">Total Profit</h3>
                    </div>
                </div>

                <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
                    <img src={products} alt="Total Orders Icon"className="w-11 h-11 mr-2 rounded-full" />
                    <div className="flex flex-col items-start">
                        <p className="text-2xl font-bold">{totalProducts}</p> 
                        <h3 className="text-lg font-semibold">Total Products</h3>
                    </div>
                </div>

                <div className="bg-white text-black p-4 rounded shadow flex-col items-start">
                    <img src={profite} alt="Total Orders Icon" className="w-11 h-11 mr-2 rounded-full" />
                    <div className="flex flex-col items-start"> 
                        <p className="text-2xl font-bold">${chiffreDAffaire}</p>
                        <h3 className="text-lg font-semibold">Chiffre d'Affaire</h3>
                    </div>
                </div>
            </div>

                {/* Chart */}
                {/* <div className="bg-white p-4 rounded shadow">
                    <h3 className="text-lg font-semibold mb-4">Sales by Category</h3>
                    <Bar data={chartData} options={{ maintainAspectRatio: false }} />
                </div> */}
            </main>
        </div>
    );
};

export default Dashboard;
