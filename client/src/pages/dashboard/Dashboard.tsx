import React from 'react';
import { Bar, Doughnut } from 'react-chartjs-2';
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
    const topcategoriessell = [
        { category: "Electronics", orders: 30 },
        { category: "Books", orders: 20 },
        { category: "Clothing", orders: 50 }
    ];
    const topSellingProducts = [
        { "product": "Smartphone", "sales": 150 },
        { "product": "Laptop", "sales": 100 },
        { "product": "Headphones", "sales": 75 },
        { "product": "Tablet", "sales": 50 },
        { "product": "Smartwatch", "sales": 25 }
    ];
    
    const categories = topcategoriessell.map(item => item.category);
    const xx = topcategoriessell.map(item => item.orders);

    const chartData = {
        labels: categories,
        datasets: [
            {
                label: 'Orders',
                data: xx,
                backgroundColor: 'rgba(75, 192, 192, 0.6)',
                borderWidth: 1,
            },
        ],
    };
    const doughnutChartData = {
        labels: topSellingProducts.map(item => item.product),
        datasets: [
            {
                label: 'Top Selling Products',
                data: topSellingProducts.map(item => item.sales),
                backgroundColor: [
                    '#FF6384',
                    '#36A2EB',
                    '#FFCE56',
                    '#4BC0C0',
                    '#9966FF'
                ],
                hoverBackgroundColor: [
                    '#FF6384',
                    '#36A2EB',
                    '#FFCE56',
                    '#4BC0C0',
                    '#9966FF'
                ]
            }
        ]
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
            <div className='flex items-start'>
                <div className="bg-white p-4 rounded shadow mb-6 max-w-lg mr-5">
                        <h3 className="text-lg font-semibold">Categories Sales</h3>
                        <div className='w-full'>
                        <Bar data={chartData} />
                        </div>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 max-w-lg">
                        <div className="bg-white p-4 rounded shadow">
                            <h3 className="text-lg font-semibold mb-4">Top Selling Products</h3>
                            <Doughnut data={doughnutChartData} />
                        </div>
                </div>
            </div>
            </main>
        </div>
    );
};

export default Dashboard;
